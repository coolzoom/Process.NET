// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.ExternalProcessMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;

namespace Process.NET.Memory
{
  public class ExternalProcessMemory : ProcessMemory
  {
    public ExternalProcessMemory(SafeMemoryHandle handle)
      : base(handle)
    {
    }

    public override byte[] Read(IntPtr intPtr, int length) => MemoryHelper.ReadBytes(this.Handle, intPtr, length);

    public override T Read<T>(IntPtr intPtr) => MarshalType<T>.ByteArrayToObject(this.Read(intPtr, MarshalType<T>.Size));

    public override void Write<T>(IntPtr intPtr, T value) => this.Write(intPtr, MarshalType<T>.ObjectToByteArray(value));

    public override int Write(IntPtr intPtr, byte[] bytesToWrite)
    {
      using (new MemoryProtection(this.Handle, intPtr, MarshalType<byte>.Size * bytesToWrite.Length))
        MemoryHelper.WriteBytes(this.Handle, intPtr, bytesToWrite);
      return bytesToWrite.Length;
    }
  }
}
