// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.WindowFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Windows
{
  public class WindowFactory : IWindowFactory, IDisposable
  {
    private readonly IProcess _process;
    private IWindow _mainWindow;

    public WindowFactory(IProcess process) => this._process = process;

    internal IEnumerable<IntPtr> ChildWindowHandles => WindowHelper.EnumChildWindows(this._process.Native.MainWindowHandle);

    internal IEnumerable<IntPtr> WindowHandles => (IEnumerable<IntPtr>) new List<IntPtr>(this.ChildWindowHandles)
    {
      this.MainWindowHandle
    };

    public IntPtr MainWindowHandle => this._process.Native.MainWindowHandle;

    public IEnumerable<IWindow> ChildWindows => (IEnumerable<IWindow>) this.ChildWindowHandles.Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this._process, handle)));

    public IWindow MainWindow
    {
      get => this._mainWindow ?? (IWindow) new RemoteWindow(this._process, this.MainWindowHandle);
      set => this._mainWindow = value;
    }

    public IEnumerable<IWindow> this[string windowTitle] => this.GetWindowsByTitle(windowTitle);

    public IEnumerable<IWindow> Windows => (IEnumerable<IWindow>) this.WindowHandles.Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this._process, handle)));

    public IEnumerable<IWindow> GetWindowsByClassName(string className) => (IEnumerable<IWindow>) this.WindowHandles.Where<IntPtr>((Func<IntPtr, bool>) (handle => WindowHelper.GetClassName(handle) == className)).Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this._process, handle)));

    public IEnumerable<IWindow> GetWindowsByTitle(string windowTitle) => (IEnumerable<IWindow>) this.WindowHandles.Where<IntPtr>((Func<IntPtr, bool>) (handle => WindowHelper.GetWindowText(handle) == windowTitle)).Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this._process, handle)));

    public IEnumerable<IWindow> GetWindowsByTitleContains(string windowTitle) => (IEnumerable<IWindow>) this.WindowHandles.Where<IntPtr>((Func<IntPtr, bool>) (handle => WindowHelper.GetWindowText(handle).Contains(windowTitle))).Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this._process, handle)));

    public void Dispose()
    {
    }
  }
}
