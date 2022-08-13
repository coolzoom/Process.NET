// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.ProcessAccessFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum ProcessAccessFlags
  {
    AllAccess = 2035711, // 0x001F0FFF
    CreateProcess = 128, // 0x00000080
    CreateThread = 2,
    DupHandle = 64, // 0x00000040
    QueryInformation = 1024, // 0x00000400
    QueryLimitedInformation = 4096, // 0x00001000
    SetInformation = 512, // 0x00000200
    SetQuota = 256, // 0x00000100
    SuspendResume = 2048, // 0x00000800
    Terminate = 1,
    VmOperation = 8,
    VmRead = 16, // 0x00000010
    VmWrite = 32, // 0x00000020
    Synchronize = 1048576, // 0x00100000
  }
}
