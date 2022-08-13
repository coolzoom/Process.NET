// Decompiled with JetBrains decompiler
// Type: Process.NET.Marshaling.MarshalledValue`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using Process.NET.Utilities;
using System;
using System.Text;

namespace Process.NET.Marshaling
{
  public class MarshalledValue<T> : IMarshalledValue, IDisposable
  {
    protected readonly IProcess Process;

    public MarshalledValue(IProcess process, T value)
    {
      this.Process = process;
      this.Value = value;
      this.Marshal();
    }

    public T Value { get; }

    public IAllocatedMemory Allocated { get; private set; }

    public IntPtr Reference { get; private set; }

    public void Dispose()
    {
      this.Allocated?.Dispose();
      this.Reference = IntPtr.Zero;
      GC.SuppressFinalize((object) this);
    }

    ~MarshalledValue() => this.Dispose();

    private void Marshal()
    {
      if (typeof (T) == typeof (string))
      {
        string stringToWrite = this.Value.ToString();
        this.Allocated = this.Process.MemoryFactory.Allocate(Randomizer.GenerateString(), stringToWrite.Length + 1);
        this.Allocated.Write(0, stringToWrite, Encoding.UTF8);
        this.Reference = this.Allocated.BaseAddress;
      }
      else
      {
        byte[] byteArray = MarshalType<T>.ObjectToByteArray(this.Value);
        if (MarshalType<T>.CanBeStoredInRegisters)
        {
          this.Reference = MarshalType<IntPtr>.ByteArrayToObject(byteArray);
        }
        else
        {
          this.Allocated = this.Process.MemoryFactory.Allocate(Randomizer.GenerateString(), MarshalType<T>.Size);
          this.Allocated.Write<T>(0, this.Value);
          this.Reference = this.Allocated.BaseAddress;
        }
      }
    }
  }
}
