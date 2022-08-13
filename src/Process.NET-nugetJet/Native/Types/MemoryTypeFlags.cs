// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.MemoryTypeFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum MemoryTypeFlags
  {
    None = 0,
    Image = 16777216, // 0x01000000
    Mapped = 262144, // 0x00040000
    Private = 131072, // 0x00020000
  }
}
