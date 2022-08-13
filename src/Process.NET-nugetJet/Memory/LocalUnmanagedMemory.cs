// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.LocalUnmanagedMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Runtime.InteropServices;

namespace Process.NET.Memory
{
  public class LocalUnmanagedMemory : IDisposable
  {
    public LocalUnmanagedMemory(int size)
    {
      this.Size = size;
      this.Address = Marshal.AllocHGlobal(this.Size);
    }

    public IntPtr Address { get; private set; }

    public int Size { get; }

    public virtual void Dispose()
    {
      Marshal.FreeHGlobal(this.Address);
      this.Address = IntPtr.Zero;
      GC.SuppressFinalize((object) this);
    }

    ~LocalUnmanagedMemory() => this.Dispose();

    public T Read<T>() => (T) Marshal.PtrToStructure(this.Address, typeof (T));

    public byte[] Read()
    {
      byte[] destination = new byte[this.Size];
      Marshal.Copy(this.Address, destination, 0, this.Size);
      return destination;
    }

    public override string ToString() => string.Format("Size = {0:X}", (object) this.Size);

    public void Write(byte[] byteArray) => Marshal.Copy(byteArray, 0, this.Address, this.Size);

    public void Write<T>(T data) => Marshal.StructureToPtr<T>(data, this.Address, false);
  }
}
