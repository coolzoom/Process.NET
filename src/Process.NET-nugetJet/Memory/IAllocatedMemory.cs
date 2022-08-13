// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.IAllocatedMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using System;

namespace Process.NET.Memory
{
  public interface IAllocatedMemory : IPointer, IDisposableState, IDisposable
  {
    bool IsAllocated { get; }

    int Size { get; }

    string Identifier { get; }
  }
}
