﻿// Decompiled with JetBrains decompiler
// Type: Process.NET.Patterns.IMemoryPattern
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System.Collections.Generic;

namespace Process.NET.Patterns
{
  public interface IMemoryPattern
  {
    int Offset { get; }

    MemoryPatternType PatternType { get; }

    IList<byte> GetBytes();

    string GetMask();
  }
}
