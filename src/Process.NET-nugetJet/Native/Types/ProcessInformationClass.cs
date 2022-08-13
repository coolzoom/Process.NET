// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.ProcessInformationClass
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public enum ProcessInformationClass
  {
    ProcessBasicInformation = 0,
    ProcessDebugPort = 7,
    ProcessWow64Information = 26, // 0x0000001A
    ProcessImageFileName = 27, // 0x0000001B
  }
}
