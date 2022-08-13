// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.ThreadAccessFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum ThreadAccessFlags
  {
    Synchronize = 1048576, // 0x00100000
    AllAccess = 2035711, // 0x001F0FFF
    DirectImpersonation = 512, // 0x00000200
    GetContext = 8,
    Impersonate = 256, // 0x00000100
    QueryInformation = 64, // 0x00000040
    QueryLimitedInformation = 2048, // 0x00000800
    SetContext = 16, // 0x00000010
    SetInformation = 32, // 0x00000020
    SetLimitedInformation = 1024, // 0x00000400
    SetThreadToken = 128, // 0x00000080
    SuspendResume = 2,
    Terminate = 1,
  }
}
