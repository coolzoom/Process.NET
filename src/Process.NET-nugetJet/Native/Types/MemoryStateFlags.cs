// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.MemoryStateFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum MemoryStateFlags
  {
    Commit = 4096, // 0x00001000
    Free = 65536, // 0x00010000
    Reserve = 8192, // 0x00002000
  }
}
