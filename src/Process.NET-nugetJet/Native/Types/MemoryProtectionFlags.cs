// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.MemoryProtectionFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum MemoryProtectionFlags
  {
    ZeroAccess = 0,
    Execute = 16, // 0x00000010
    ExecuteRead = 32, // 0x00000020
    ExecuteReadWrite = 64, // 0x00000040
    ExecuteWriteCopy = 128, // 0x00000080
    NoAccess = 1,
    ReadOnly = 2,
    ReadWrite = 4,
    WriteCopy = 8,
    Guard = 256, // 0x00000100
    NoCache = 512, // 0x00000200
    WriteCombine = 1024, // 0x00000400
  }
}
