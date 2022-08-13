// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.IMemoryFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Collections.Generic;

namespace Process.NET.Memory
{
  public interface IMemoryFactory : IDisposable
  {
    IEnumerable<MemoryRegion> Regions { get; }

    IEnumerable<IAllocatedMemory> Allocations { get; }

    IAllocatedMemory this[string name] { get; }

    IAllocatedMemory Allocate(
      string name,
      int size,
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true);

    void Deallocate(IAllocatedMemory allocation);
  }
}
