// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.MemoryAllocationFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum MemoryAllocationFlags
  {
    Commit = 4096, // 0x00001000
    Reserve = 8192, // 0x00002000
    Reset = 524288, // 0x00080000
    ResetUndo = 16777216, // 0x01000000
    LargePages = 536870912, // 0x20000000
    Physical = 4194304, // 0x00400000
    TopDown = 1048576, // 0x00100000
  }
}
