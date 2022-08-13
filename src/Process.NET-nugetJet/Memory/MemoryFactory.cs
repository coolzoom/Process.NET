// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.MemoryFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Memory
{
  public class MemoryFactory : IMemoryFactory, IDisposable
  {
    protected readonly List<IAllocatedMemory> InternalRemoteAllocations;
    protected readonly IProcess Process;

    public MemoryFactory(IProcess process)
    {
      this.Process = process;
      this.InternalRemoteAllocations = new List<IAllocatedMemory>();
    }

    public IEnumerable<IAllocatedMemory> Allocations => (IEnumerable<IAllocatedMemory>) this.InternalRemoteAllocations.AsReadOnly();

    public IAllocatedMemory this[string name] => this.InternalRemoteAllocations.FirstOrDefault<IAllocatedMemory>((Func<IAllocatedMemory, bool>) (am => am.Identifier == name));

    public IEnumerable<MemoryRegion> Regions
    {
      get
      {
        IntPtr addressTo = IntPtr.Size == 8 ? new IntPtr(long.MaxValue) : new IntPtr(int.MaxValue);
        return MemoryHelper.Query(this.Process.Handle, IntPtr.Zero, addressTo).Select<MemoryBasicInformation, MemoryRegion>((Func<MemoryBasicInformation, MemoryRegion>) (page => new MemoryRegion(this.Process, page.BaseAddress)));
      }
    }

    public IAllocatedMemory Allocate(
      string name,
      int size,
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true)
    {
      AllocatedMemory allocatedMemory = new AllocatedMemory(this.Process, name, size, protection, mustBeDisposed);
      this.InternalRemoteAllocations.Add((IAllocatedMemory) allocatedMemory);
      return (IAllocatedMemory) allocatedMemory;
    }

    public void Deallocate(IAllocatedMemory allocation)
    {
      if (!allocation.IsDisposed)
        allocation.Dispose();
      if (!this.InternalRemoteAllocations.Contains(allocation))
        return;
      this.InternalRemoteAllocations.Remove(allocation);
    }

    public virtual void Dispose()
    {
      foreach (IDisposable disposable in this.InternalRemoteAllocations.Where<IAllocatedMemory>((Func<IAllocatedMemory, bool>) (m => m.MustBeDisposed)).ToArray<IAllocatedMemory>())
        disposable.Dispose();
      GC.SuppressFinalize((object) this);
    }

    ~MemoryFactory() => this.Dispose();
  }
}
