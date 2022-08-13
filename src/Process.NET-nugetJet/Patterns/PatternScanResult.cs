// Decompiled with JetBrains decompiler
// Type: Process.NET.Patterns.PatternScanResult
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Patterns
{
  public struct PatternScanResult
  {
    public IntPtr ReadAddress { get; set; }

    public IntPtr BaseAddress { get; set; }

    public int Offset { get; set; }

    public bool Found { get; set; }
  }
}
