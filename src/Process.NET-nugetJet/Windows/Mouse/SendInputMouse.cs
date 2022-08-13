// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Mouse.SendInputMouse
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native;
using Process.NET.Native.Types;
using Process.NET.Utilities;
using System.Threading;

namespace Process.NET.Windows.Mouse
{
  public class SendInputMouse : IMouse
  {
    public SendInputMouse(IWindow window) => this.Window = window;

    protected IWindow Window { get; set; }

    public void ClickLeft()
    {
      this.PressLeft();
      this.ReleaseLeft();
    }

    public void ClickMiddle()
    {
      this.PressMiddle();
      this.ReleaseMiddle();
    }

    public void ClickRight()
    {
      this.PressRight();
      this.ReleaseRight();
    }

    public void DoubleClickLeft()
    {
      this.ClickLeft();
      Thread.Sleep(10);
      this.ClickLeft();
    }

    public void MoveTo(int x, int y) => this.MoveToAbsolute(this.Window.X + x, this.Window.Y + y);

    public void PressLeft()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.LeftDown;
      WindowHelper.SendInput(input);
    }

    public void PressMiddle()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.MiddleDown;
      WindowHelper.SendInput(input);
    }

    public void PressRight()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.RightDown;
      WindowHelper.SendInput(input);
    }

    public void ReleaseLeft()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.LeftUp;
      WindowHelper.SendInput(input);
    }

    public void ReleaseMiddle()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.MiddleUp;
      WindowHelper.SendInput(input);
    }

    public void ReleaseRight()
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.RightUp;
      WindowHelper.SendInput(input);
    }

    public void ScrollHorizontally(int delta = 120)
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.HWheel;
      input.Mouse.MouseData = delta;
      WindowHelper.SendInput(input);
    }

    public void ScrollVertically(int delta = 120)
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.Flags = MouseFlags.Wheel;
      input.Mouse.MouseData = delta;
      WindowHelper.SendInput(input);
    }

    protected void MoveToAbsolute(int x, int y)
    {
      Input input = SendInputMouse.CreateInput();
      input.Mouse.DeltaX = SendInputMouse.CalculateAbsoluteCoordinateX(x);
      input.Mouse.DeltaY = SendInputMouse.CalculateAbsoluteCoordinateY(y);
      input.Mouse.Flags = MouseFlags.Absolute | MouseFlags.Move;
      input.Mouse.MouseData = 0;
      WindowHelper.SendInput(input);
    }

    private static int CalculateAbsoluteCoordinateX(int x) => x * 65536 / User32.GetSystemMetrics(SystemMetrics.CxScreen);

    private static int CalculateAbsoluteCoordinateY(int y) => y * 65536 / User32.GetSystemMetrics(SystemMetrics.CyScreen);

    private static Input CreateInput() => new Input(InputTypes.Mouse);
  }
}
