// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.MemoryRegion
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;

namespace Process.NET.Memory
{
  public class MemoryRegion : MemoryPointer, IEquatable<MemoryRegion>
  {
    public MemoryRegion(IProcess processPlus, IntPtr baseAddress)
      : base(processPlus, baseAddress)
    {
    }

    public MemoryBasicInformation Information => MemoryHelper.Query(this.Process.Handle, this.BaseAddress);

    public override bool IsValid => base.IsValid && this.Information.State != MemoryStateFlags.Free;

    public bool Equals(MemoryRegion other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return this.BaseAddress.Equals((object) other.BaseAddress) && this.Process.Equals((object) other.Process) && this.Information.RegionSize.Equals(other.Information.RegionSize);
    }

    public MemoryProtection ChangeProtection(
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true)
    {
      return new MemoryProtection(this.Process.Handle, this.BaseAddress, this.Information.RegionSize, protection, mustBeDisposed);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return obj.GetType() == this.GetType() && this.Equals((MemoryRegion) obj);
    }

    public override int GetHashCode() => this.BaseAddress.GetHashCode() ^ this.Process.GetHashCode() ^ this.Information.RegionSize.GetHashCode();

    public static bool operator ==(MemoryRegion left, MemoryRegion right) => object.Equals((object) left, (object) right);

    public static bool operator !=(MemoryRegion left, MemoryRegion right) => !object.Equals((object) left, (object) right);

    public void Release()
    {
      MemoryHelper.Free(this.Process.Handle, this.BaseAddress);
      this.BaseAddress = IntPtr.Zero;
    }

    public override string ToString() => string.Format("BaseAddress = 0x{0:X} Size = 0x{1:X} Protection = {2}", (object) this.BaseAddress.ToInt64(), (object) this.Information.RegionSize, (object) this.Information.Protect);
  }
}
