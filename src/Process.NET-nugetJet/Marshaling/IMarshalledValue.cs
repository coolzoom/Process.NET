﻿// Decompiled with JetBrains decompiler
// Type: Process.NET.Marshaling.IMarshalledValue
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using System;

namespace Process.NET.Marshaling
{
  public interface IMarshalledValue : IDisposable
  {
    IAllocatedMemory Allocated { get; }

    IntPtr Reference { get; }
  }
}
