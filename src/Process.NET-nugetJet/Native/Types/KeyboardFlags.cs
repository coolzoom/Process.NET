﻿// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.KeyboardFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum KeyboardFlags
  {
    ExtendedKey = 1,
    KeyUp = 2,
    ScanCode = 8,
    Unicode = 4,
  }
}
