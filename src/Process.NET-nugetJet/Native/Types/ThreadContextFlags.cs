// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.ThreadContextFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum ThreadContextFlags
  {
    Intel386 = 65536, // 0x00010000
    Intel486 = Intel386, // 0x00010000
    Control = 65537, // 0x00010001
    Integer = 65538, // 0x00010002
    Segments = 65540, // 0x00010004
    FloatingPoint = 65544, // 0x00010008
    DebugRegisters = 65552, // 0x00010010
    ExtendedRegisters = 65568, // 0x00010020
    Full = 65543, // 0x00010007
    All = 65599, // 0x0001003F
  }
}
