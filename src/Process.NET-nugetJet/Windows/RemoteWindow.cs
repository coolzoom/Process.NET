// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.RemoteWindow
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Threads;
using Process.NET.Utilities;
using Process.NET.Windows.Keyboard;
using Process.NET.Windows.Mouse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Windows
{
  public class RemoteWindow : IEquatable<RemoteWindow>, IWindow, IDisposable
  {
    protected readonly IProcess ProcessPlus;

    public RemoteWindow(IProcess processPlus, IntPtr handle)
    {
      this.ProcessPlus = processPlus;
      this.Handle = handle;
      this.Keyboard = (IKeyboard) new MessageKeyboard((IWindow) this);
      this.Mouse = (IMouse) new SendInputMouse((IWindow) this);
    }

    protected IEnumerable<IntPtr> ChildrenHandles => WindowHelper.EnumChildWindows(this.Handle);

    public bool Equals(RemoteWindow other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return object.Equals((object) this.ProcessPlus, (object) other.ProcessPlus) && this.Handle.Equals((object) other.Handle);
    }

    public IEnumerable<IWindow> Children => (IEnumerable<IWindow>) this.ChildrenHandles.Select<IntPtr, RemoteWindow>((Func<IntPtr, RemoteWindow>) (handle => new RemoteWindow(this.ProcessPlus, handle)));

    public string ClassName => WindowHelper.GetClassName(this.Handle);

    public IntPtr Handle { get; }

    public int Height
    {
      get => this.Placement.NormalPosition.Height;
      set
      {
        WindowPlacement placement = this.Placement;
        placement.NormalPosition.Height = value;
        this.Placement = placement;
      }
    }

    public bool IsActivated => WindowHelper.GetForegroundWindow() == this.Handle;

    public bool IsMainWindow => this.ProcessPlus.Native.MainWindowHandle == this.Handle;

    public IKeyboard Keyboard { get; set; }

    public IMouse Mouse { get; set; }

    public WindowPlacement Placement
    {
      get => WindowHelper.GetWindowPlacement(this.Handle);
      set => WindowHelper.SetWindowPlacement(this.Handle, value);
    }

    public WindowStates State
    {
      get => this.Placement.ShowCmd;
      set => WindowHelper.ShowWindow(this.Handle, value);
    }

    public string Title
    {
      get => WindowHelper.GetWindowText(this.Handle);
      set => WindowHelper.SetWindowText(this.Handle, value);
    }

    public IRemoteThread Thread => this.ProcessPlus.ThreadFactory.GetThreadById(WindowHelper.GetWindowThreadId(this.Handle));

    public int Width
    {
      get => this.Placement.NormalPosition.Width;
      set
      {
        WindowPlacement placement = this.Placement;
        placement.NormalPosition.Width = value;
        this.Placement = placement;
      }
    }

    public int X
    {
      get => this.Placement.NormalPosition.Left;
      set
      {
        WindowPlacement placement = this.Placement;
        placement.NormalPosition.Right = value + placement.NormalPosition.Width;
        placement.NormalPosition.Left = value;
        this.Placement = placement;
      }
    }

    public int Y
    {
      get => this.Placement.NormalPosition.Top;
      set
      {
        WindowPlacement placement = this.Placement;
        placement.NormalPosition.Bottom = value + placement.NormalPosition.Height;
        placement.NormalPosition.Top = value;
        this.Placement = placement;
      }
    }

    public void Activate() => WindowHelper.SetForegroundWindow(this.Handle);

    public void Close() => this.PostMessage(WindowsMessages.Close, IntPtr.Zero, IntPtr.Zero);

    public void Flash() => WindowHelper.FlashWindow(this.Handle);

    public void Flash(int count, TimeSpan timeout, FlashWindowFlags flags = FlashWindowFlags.All) => WindowHelper.FlashWindowEx(this.Handle, flags, count, timeout);

    public virtual void Dispose()
    {
    }

    public void PostMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam) => WindowHelper.PostMessage(this.Handle, message, wParam, lParam);

    public void PostMessage(int message, IntPtr wParam, IntPtr lParam) => WindowHelper.PostMessage(this.Handle, message, wParam, lParam);

    public IntPtr SendMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam) => WindowHelper.SendMessage(this.Handle, message, wParam, lParam);

    public IntPtr SendMessage(int message, IntPtr wParam, IntPtr lParam) => WindowHelper.SendMessage(this.Handle, message, wParam, lParam);

    public override int GetHashCode()
    {
      IProcess processPlus = this.ProcessPlus;
      return (processPlus != null ? processPlus.GetHashCode() : 0) * 397 ^ this.Handle.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return !(obj.GetType() != this.GetType()) && this.Equals((RemoteWindow) obj);
    }

    public static bool operator ==(RemoteWindow left, RemoteWindow right) => object.Equals((object) left, (object) right);

    public static bool operator !=(RemoteWindow left, RemoteWindow right) => !object.Equals((object) left, (object) right);

    public override string ToString() => string.Format("Title = {0} ClassName = {1}", (object) this.Title, (object) this.ClassName);
  }
}
