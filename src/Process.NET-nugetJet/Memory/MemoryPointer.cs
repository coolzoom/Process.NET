// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.MemoryPointer
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Text;

namespace Process.NET.Memory
{
  public class MemoryPointer : IEquatable<MemoryPointer>, IPointer
  {
    public MemoryPointer(IProcess process, IntPtr address)
    {
      this.Process = process;
      this.BaseAddress = address;
    }

    public IProcess Process { get; protected set; }

    public bool Equals(MemoryPointer other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return this.BaseAddress.Equals((object) other.BaseAddress) && this.Process.Equals((object) other.Process);
    }

    public IntPtr BaseAddress { get; protected set; }

    public virtual bool IsValid => this.BaseAddress != IntPtr.Zero;

    public byte[] Read(int offset, int length) => this.Process.Memory.Read(this.BaseAddress + offset, length);

    public T Read<T>(int offset) => this.Process.Memory.Read<T>(this.BaseAddress + offset);

    public T[] Read<T>(int offset, int count) => this.Process.Memory.Read<T>(this.BaseAddress + offset, count);

    public int Write(int offset, byte[] toWrite) => this.Process.Memory.Write(this.BaseAddress + offset, toWrite);

    public string Read(int offset, Encoding encoding, int maxLength = 512) => this.Process.Memory.Read(this.BaseAddress + offset, encoding, maxLength);

    public void Write<T>(int offset, T value) => this.Process.Memory.Write<T>(this.BaseAddress + offset, value);

    public void Write<T>(int offset, T[] array) => this.Process.Memory.Write<T>(this.BaseAddress + offset, array);

    public void Write(int offset, string text, Encoding encoding) => this.Process.Memory.Write(this.BaseAddress + offset, text, encoding);

    public MemoryProtection ChangeProtection(
      SafeMemoryHandle handle,
      int size,
      MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
      bool mustBeDisposed = true)
    {
      return new MemoryProtection(handle, this.BaseAddress, size, protection, mustBeDisposed);
    }

    public T Read<T>(Enum offset) => this.Read<T>(Convert.ToInt32((object) offset));

    public T Read<T>() => this.Read<T>(0);

    public T[] Read<T>(Enum offset, int count) => this.Read<T>(Convert.ToInt32((object) offset), count);

    public string Read(Enum offset, Encoding encoding, int maxLength = 512) => this.Read(Convert.ToInt32((object) offset), encoding, maxLength);

    public string Read(Encoding encoding, int maxLength = 512) => this.Read(0, encoding, maxLength);

    public string Read(int offset, int maxLength, Encoding encoding) => this.Process.Memory.Read(this.BaseAddress + offset, encoding, maxLength);

    public string Read(Enum offset, int maxLength = 512) => this.Read(Convert.ToInt32((object) offset), maxLength, Encoding.UTF8);

    public override string ToString() => string.Format("BaseAddress = 0x{0:X}", (object) this.BaseAddress.ToInt64());

    public void Write<T>(Enum offset, T value) => this.Write<T>(Convert.ToInt32((object) offset), value);

    public void Write<T>(T value) => this.Write<T>(0, value);

    public void Write<T>(Enum offset, T[] array) => this.Write<T>(Convert.ToInt32((object) offset), array);

    public void Write<T>(T[] array) => this.Write<T>(0, array);

    public void Write(Enum offset, string text, Encoding encoding) => this.Write(Convert.ToInt32((object) offset), text, encoding);

    public void Write(string text, Encoding encoding) => this.Write(0, text, encoding);

    public void Write(int offset, string text) => this.Process.Memory.Write<string>(this.BaseAddress + offset, text);

    public void Write(Enum offset, string text) => this.Write(Convert.ToInt32((object) offset), text);

    public void Write(string text) => this.Write(0, text);

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return obj.GetType() == this.GetType() && this.Equals((MemoryPointer) obj);
    }

    public override int GetHashCode() => this.BaseAddress.GetHashCode() ^ this.Process.GetHashCode();

    public static bool operator ==(MemoryPointer left, MemoryPointer right) => object.Equals((object) left, (object) right);

    public static bool operator !=(MemoryPointer left, MemoryPointer right) => !object.Equals((object) left, (object) right);
  }
}
