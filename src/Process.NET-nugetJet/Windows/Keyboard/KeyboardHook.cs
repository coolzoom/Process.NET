// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Keyboard.KeyboardHook
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Applied;
using Process.NET.Marshaling;
using Process.NET.Native.Types;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Process.NET.Windows.Keyboard
{
  public class KeyboardHook : IApplied, IDisposableState, IDisposable
  {
    private IntPtr _hhook;
    private KeyboardHook.HookProc _hookproc;
    private bool _ispaused;
    public KeyboardHook.KeyDownEventDelegate KeyDownEvent = (KeyboardHook.KeyDownEventDelegate) (_param1 => { });
    public KeyboardHook.KeyUpEventDelegate KeyUpEvent = (KeyboardHook.KeyUpEventDelegate) (_param1 => { });
    private const int WmKeydown = 256;
    private const int WmKeyup = 257;
    private const int WmSyskeydown = 260;
    public readonly int WmSyskeyup = 261;

    public KeyboardHook(string name) => this.Identifier = name;

    public bool IsPaused
    {
      get => this._ispaused;
      set
      {
        if (value != this._ispaused & value)
          this.Disable();
        if (value != this._ispaused && !value)
          this.Enable();
        this._ispaused = value;
      }
    }

    public void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      this.Disable();
      GC.SuppressFinalize((object) this);
    }

    public bool IsDisposed { get; set; }

    public bool MustBeDisposed { get; } = true;

    public string Identifier { get; }

    public bool IsEnabled { get; set; }

    public void Enable()
    {
      Trace.WriteLine(string.Format("Starting hook '{0}'...", (object) this.Identifier), string.Format("Hook.StartHook [{0}]", (object) Thread.CurrentThread.Name));
      this._hookproc = new KeyboardHook.HookProc(this.HookCallback);
      this._hhook = KeyboardHook.SetWindowsHookEx(KeyboardHook.HookType.WhKeyboardLl, this._hookproc, KeyboardHook.GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
      IntPtr hhook = this._hhook;
      if (this._hhook != IntPtr.Zero)
      {
        this.IsEnabled = true;
      }
      else
      {
        Trace.TraceError(new Win32Exception(Marshal.GetLastWin32Error()).Message);
        this.IsDisposed = false;
      }
    }

    public void Disable()
    {
      Trace.WriteLine(string.Format("Stopping hook '{0}'...", (object) this.Identifier), string.Format("Hook.StartHook [{0}]", (object) Thread.CurrentThread.Name));
      KeyboardHook.UnhookWindowsHookEx(this._hhook);
      this.IsEnabled = false;
    }

    private int HookCallback(int code, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
    {
      int num = 0;
      try
      {
        if (!this.IsPaused)
        {
          if (code >= 0)
          {
            if (wParam.ToInt32() == 260 || wParam.ToInt32() == 256)
              this.KeyDownEvent(new KeyboardHookEventArgs(lParam));
            if (wParam.ToInt32() != this.WmSyskeyup)
            {
              if (wParam.ToInt32() != 257)
                goto label_9;
            }
            this.KeyUpEvent(new KeyboardHookEventArgs(lParam));
          }
        }
      }
      finally
      {
        num = KeyboardHook.CallNextHookEx(IntPtr.Zero, code, wParam, ref lParam);
      }
label_9:
      return num;
    }

    ~KeyboardHook()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(
      KeyboardHook.HookType idHook,
      KeyboardHook.HookProc lpfn,
      IntPtr hMod,
      int dwThreadId);

    [DllImport("user32.dll")]
    private static extern int UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern int CallNextHookEx(
      IntPtr hhk,
      int nCode,
      IntPtr wParam,
      ref KBDLLHOOKSTRUCT lParam);

    public delegate void KeyDownEventDelegate(KeyboardHookEventArgs e);

    public delegate void KeyUpEventDelegate(KeyboardHookEventArgs e);

    private enum HookType
    {
      WhJournalrecord,
      WhJournalplayback,
      WhKeyboard,
      WhGetmessage,
      WhCallwndproc,
      WhCbt,
      WhSysmsgfilter,
      WhMouse,
      WhHardware,
      WhDebug,
      WhShell,
      WhForegroundidle,
      WhCallwndprocret,
      WhKeyboardLl,
      WhMouseLl,
    }

    private delegate int HookProc(int code, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
  }
}
