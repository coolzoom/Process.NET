// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.MemoryProtection
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;

namespace Process.NET.Memory
{
  public class MemoryProtection : IDisposable
  {
    protected readonly SafeMemoryHandle Handle;

    public MemoryProtection(
      SafeMemoryHandle handle,
      IntPtr baseAddress,
      int size,
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true)
    {
      this.Handle = handle;
      this.BaseAddress = baseAddress;
      this.NewProtection = protection;
      this.Size = size;
      this.MustBeDisposed = mustBeDisposed;
      this.OldProtection = MemoryHelper.ChangeProtection(this.Handle, baseAddress, size, protection);
    }

    public IntPtr BaseAddress { get; }

    public bool MustBeDisposed { get; set; }

    public MemoryProtectionFlags NewProtection { get; }

    public MemoryProtectionFlags OldProtection { get; }

    public int Size { get; }

    public virtual void Dispose()
    {
      int num = (int) MemoryHelper.ChangeProtection(this.Handle, this.BaseAddress, this.Size, this.OldProtection);
      GC.SuppressFinalize((object) this);
    }

    ~MemoryProtection()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    public override string ToString() => string.Format("BaseAddress = 0x{0:X} NewProtection = {1} OldProtection = {2}", (object) this.BaseAddress.ToInt64(), (object) this.NewProtection, (object) this.OldProtection);
  }
}
