// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.AllocatedMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;

namespace Process.NET.Memory
{
  public class AllocatedMemory : 
    MemoryRegion,
    IAllocatedMemory,
    IPointer,
    IDisposableState,
    IDisposable
  {
    public AllocatedMemory(
      IProcess processPlus,
      string name,
      int size,
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true)
      : base(processPlus, MemoryHelper.Allocate(processPlus.Handle, size, protection))
    {
      this.Identifier = name;
      this.MustBeDisposed = mustBeDisposed;
      this.IsDisposed = false;
      this.Size = size;
    }

    public bool IsDisposed { get; private set; }

    public bool MustBeDisposed { get; set; }

    public virtual void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      this.Release();
      this.Process.MemoryFactory.Deallocate((IAllocatedMemory) this);
      this.BaseAddress = IntPtr.Zero;
      GC.SuppressFinalize((object) this);
    }

    public bool IsAllocated => this.IsDisposed;

    public int Size { get; }

    public string Identifier { get; }

    ~AllocatedMemory()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }
  }
}
