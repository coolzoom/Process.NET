// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.Input
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System.Runtime.InteropServices;

namespace Process.NET.Native.Types
{
  [StructLayout(LayoutKind.Explicit)]
  public struct Input
  {
    [FieldOffset(0)]
    public InputTypes Type;
    [FieldOffset(4)]
    public MouseInput Mouse;
    [FieldOffset(4)]
    public KeyboardInput Keyboard;
    [FieldOffset(4)]
    public HardwareInput Hardware;

    public Input(InputTypes type)
      : this()
    {
      this.Type = type;
    }
  }
}
