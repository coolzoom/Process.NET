// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Patches.PatchManager
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using System;

namespace Process.NET.Applied.Patches
{
  public class PatchManager : ComplexAppliedManager<Patch>, IDisposable
  {
    public PatchManager(IMemory processMemory) => this.MemoryBase = processMemory;

    protected IMemory MemoryBase { get; }

    public void Dispose() => this.RemoveAll();

    public Patch Create(IntPtr address, byte[] patchWith, string name)
    {
      if (this.InternalItems.ContainsKey(name))
        return this.InternalItems[name];
      this.InternalItems.Add(name, new Patch(address, patchWith, name, this.MemoryBase));
      return this.InternalItems[name];
    }

    public Patch CreateAndApply(IntPtr address, byte[] patchWith, string name)
    {
      this.Create(address, patchWith, name);
      this.InternalItems[name].Enable();
      return this.InternalItems[name];
    }
  }
}
