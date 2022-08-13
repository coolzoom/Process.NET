// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.MouseFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum MouseFlags
  {
    Absolute = 32768, // 0x00008000
    HWheel = 4096, // 0x00001000
    Move = 1,
    MoveNoCoalesce = 8192, // 0x00002000
    LeftDown = 2,
    LeftUp = 4,
    RightDown = 8,
    RightUp = 16, // 0x00000010
    MiddleDown = 32, // 0x00000020
    MiddleUp = 64, // 0x00000040
    VirtualDesk = 16384, // 0x00004000
    Wheel = 2048, // 0x00000800
    XDown = 128, // 0x00000080
    XUp = 256, // 0x00000100
  }
}
