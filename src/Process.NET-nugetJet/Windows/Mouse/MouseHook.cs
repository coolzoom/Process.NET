// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Mouse.MouseHook
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Applied;
using Process.NET.Marshaling;
using Process.NET.Native;
using Process.NET.Native.Types;
using System;
using System.Runtime.InteropServices;

namespace Process.NET.Windows.Mouse
{
  public sealed class MouseHook : IApplied, IDisposableState, IDisposable
  {
    private readonly LowLevelProc _callback;
    private IntPtr _hookId;

    public MouseHook(string identifier)
    {
      this.Identifier = identifier;
      this._callback = new LowLevelProc(this.MouseHookCallback);
    }

    public bool IsEnabled { get; private set; }

    public bool IsDisposed { get; set; }

    public bool MustBeDisposed { get; set; } = true;

    public string Identifier { get; }

    public void Enable()
    {
      this._hookId = User32.SetWindowsHook(HookType.WH_MOUSE_LL, this._callback);
      this.IsEnabled = true;
    }

    public void Disable()
    {
      if (!this.IsEnabled)
        return;
      User32.UnhookWindowsHookEx(this._hookId);
      this.IsEnabled = false;
    }

    private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
      if (nCode >= 0)
      {
        MouseHookEventArgs e = new MouseHookEventArgs((MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof (MSLLHOOKSTRUCT)));
        switch ((int) wParam)
        {
          case 512:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.MouseMove, new Action<MouseHookEventArgs>(this.OnMove));
            break;
          case 513:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.LeftButtonDown, new Action<MouseHookEventArgs>(this.OnLeftButtonDown));
            break;
          case 514:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.LeftButtonUp, new Action<MouseHookEventArgs>(this.OnLeftButtonUp));
            break;
          case 516:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.RightButtonDown, new Action<MouseHookEventArgs>(this.OnRightButtonDown));
            break;
          case 517:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.RightButtonUp, new Action<MouseHookEventArgs>(this.OnRightButtonUp));
            break;
          case 519:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.MiddleButtonDown, new Action<MouseHookEventArgs>(this.OnMiddleButtonDown));
            break;
          case 520:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.MouseMove, new Action<MouseHookEventArgs>(this.OnMove));
            e.MouseEventName = MouseEventNames.MiddleButtonUp;
            this.OnMiddleButtonUp(e);
            break;
          case 522:
            MouseHook.TriggerMouseEvent(e, MouseEventNames.MouseMove, new Action<MouseHookEventArgs>(this.OnMove));
            e.MouseEventName = MouseEventNames.MouseWheel;
            this.OnWheel(e);
            break;
        }
      }
      return (IntPtr) User32.CallNextHookEx(this._hookId, nCode, wParam, lParam);
    }

    private static void TriggerMouseEvent(
      MouseHookEventArgs e,
      MouseEventNames name,
      Action<MouseHookEventArgs> method)
    {
      e.MouseEventName = name;
      method(e);
    }

    public event EventHandler<MouseHookEventArgs> Move;

    private void OnMove(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> move = this.Move;
      if (move != null)
        move((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> LeftButtonDown;

    private void OnLeftButtonDown(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> leftButtonDown = this.LeftButtonDown;
      if (leftButtonDown != null)
        leftButtonDown((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> LeftButtonUp;

    private void OnLeftButtonUp(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> leftButtonUp = this.LeftButtonUp;
      if (leftButtonUp != null)
        leftButtonUp((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> RightButtonDown;

    private void OnRightButtonDown(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> rightButtonDown = this.RightButtonDown;
      if (rightButtonDown != null)
        rightButtonDown((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> RightButtonUp;

    private void OnRightButtonUp(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> rightButtonUp = this.RightButtonUp;
      if (rightButtonUp != null)
        rightButtonUp((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> MiddleButtonDown;

    private void OnMiddleButtonDown(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> middleButtonDown = this.MiddleButtonDown;
      if (middleButtonDown != null)
        middleButtonDown((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> MiddleButtonUp;

    private void OnMiddleButtonUp(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> middleButtonUp = this.MiddleButtonUp;
      if (middleButtonUp != null)
        middleButtonUp((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> Wheel;

    private void OnWheel(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> wheel = this.Wheel;
      if (wheel != null)
        wheel((object) this, e);
      this.OnMouseEvent(e);
    }

    public event EventHandler<MouseHookEventArgs> MouseEvent;

    private void OnMouseEvent(MouseHookEventArgs e)
    {
      EventHandler<MouseHookEventArgs> mouseEvent = this.MouseEvent;
      if (mouseEvent == null)
        return;
      mouseEvent((object) this, e);
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

    ~MouseHook()
    {
      if (!this.MustBeDisposed)
        return;
      this.Disable();
    }
  }
}
