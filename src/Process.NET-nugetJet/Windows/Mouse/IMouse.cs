// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Mouse.IMouse
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Windows.Mouse
{
  public interface IMouse
  {
    void ClickLeft();

    void ClickMiddle();

    void ClickRight();

    void DoubleClickLeft();

    void MoveTo(int x, int y);

    void PressLeft();

    void PressMiddle();

    void PressRight();

    void ReleaseLeft();

    void ReleaseMiddle();

    void ReleaseRight();

    void ScrollHorizontally(int delta = 120);

    void ScrollVertically(int delta = 120);
  }
}
