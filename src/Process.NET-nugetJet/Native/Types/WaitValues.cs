// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.WaitValues
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public enum WaitValues : uint
  {
    Signaled = 0,
    Abandoned = 128, // 0x00000080
    Timeout = 258, // 0x00000102
    Failed = 4294967295, // 0xFFFFFFFF
  }
}
