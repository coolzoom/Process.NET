// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.Rectangle
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public struct Rectangle
  {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public int Height
    {
      get => this.Bottom - this.Top;
      set => this.Bottom = this.Top + value;
    }

    public int Width
    {
      get => this.Right - this.Left;
      set => this.Right = this.Left + value;
    }

    public override string ToString() => string.Format("Left = {0} Top = {1} Height = {2} Width = {3}", (object) this.Left, (object) this.Top, (object) this.Height, (object) this.Width);
  }
}
