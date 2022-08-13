// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.WindowProcHook
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Applied;
using Process.NET.Marshaling;
using Process.NET.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Process.NET.Windows
{
  public abstract class WindowProcHook : IApplied, IDisposableState, IDisposable
  {
    private const int GWL_WNDPROC = -4;
    private WindowProc _newCallback;
    private IntPtr _oldCallback;

    protected WindowProcHook(IntPtr handle, string identifier = "")
    {
      if (string.IsNullOrEmpty(identifier))
        identifier = "WindowProcHook - " + handle.ToString("X");
      this.Identifier = identifier;
      this.Handle = handle;
    }

    protected WindowProcHook(string procName, int index = 0)
    {
      List<System.Diagnostics.Process> list = ((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcessesByName(procName)).ToList<System.Diagnostics.Process>();
      if (list == null)
        throw new NullReferenceException(string.Format("Could not find a process by the name of {0}", (object) procName));
      System.Diagnostics.Process process = index == 0 ? list.First<System.Diagnostics.Process>() : list[index];
      this.Identifier = string.Format("WndProc hook - {0}", (object) process.MainWindowTitle);
      this.Handle = process.MainWindowHandle;
    }

    protected IntPtr Handle { get; set; }

    public void Enable()
    {
      this._newCallback = new WindowProc(this.OnWndProc);
      this._oldCallback = Kernel32.SetWindowLongPtr(this.Handle, -4, Marshal.GetFunctionPointerForDelegate<WindowProc>(this._newCallback));
      if (this._oldCallback == IntPtr.Zero)
        throw new Win32Exception(Marshal.GetLastWin32Error());
      this.IsEnabled = true;
    }

    public string Identifier { get; }

    public bool IsDisposed { get; protected set; }

    public bool IsEnabled { get; protected set; }

    public bool MustBeDisposed { get; set; } = true;

    public void Disable()
    {
      if (this._newCallback == null)
        return;
      Kernel32.SetWindowLongPtr(this.Handle, -4, this._oldCallback);
      this._newCallback = (WindowProc) null;
      this.IsEnabled = false;
    }

    public void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      if (this.IsEnabled)
        this.Disable();
      GC.SuppressFinalize((object) this);
    }

    protected virtual IntPtr OnWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam) => Kernel32.CallWindowProc(this._oldCallback, hWnd, msg, wParam, lParam);

    ~WindowProcHook()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    public override string ToString() => this.Identifier;
  }
}
