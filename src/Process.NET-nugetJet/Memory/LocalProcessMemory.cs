// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.LocalProcessMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Native.Types;
using System;
using System.Runtime.InteropServices;

namespace Process.NET.Memory
{
  public class LocalProcessMemory : ProcessMemory
  {
    public LocalProcessMemory(SafeMemoryHandle handle)
      : base(handle)
    {
    }

    public override unsafe byte[] Read(IntPtr intPtr, int length)
    {
      byte[] numArray = new byte[length];
      byte* numPtr = (byte*) (void*) intPtr;
      for (int index = 0; index < length; ++index)
        numArray[index] = numPtr[index];
      return numArray;
    }

    public override T Read<T>(IntPtr intPtr) => intPtr.Read<T>();

    public override unsafe int Write(IntPtr intPtr, byte[] bytesToWrite)
    {
      using (new MemoryProtectionOperation(intPtr, bytesToWrite.Length, 64))
      {
        byte* numPtr = (byte*) (void*) intPtr;
        for (int index = 0; index < bytesToWrite.Length; ++index)
          numPtr[index] = bytesToWrite[index];
      }
      return bytesToWrite.Length;
    }

    public void Write<T>(IntPtr intPtr, T value, bool replace) => Marshal.StructureToPtr<T>(value, intPtr, replace);

    public override void Write<T>(IntPtr intPtr, T value) => this.Write<T>(intPtr, value, false);
  }
}
