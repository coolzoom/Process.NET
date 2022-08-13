// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.ProcessMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native.Types;
using System;
using System.Text;

namespace Process.NET.Memory
{
  public abstract class ProcessMemory : IMemory
  {
    protected readonly SafeMemoryHandle Handle;

    protected ProcessMemory(SafeMemoryHandle handle) => this.Handle = handle;

    public abstract byte[] Read(IntPtr intPtr, int length);

    public string Read(IntPtr intPtr, Encoding encoding, int maxLength)
    {
      byte[] bytes = this.Read(intPtr, maxLength);
      string str = encoding.GetString(bytes);
      if (str.IndexOf(char.MinValue) != -1)
        str = str.Remove(str.IndexOf(char.MinValue));
      return str;
    }

    public abstract T Read<T>(IntPtr intPtr);

    public T[] Read<T>(IntPtr intPtr, int length)
    {
      T[] objArray = new T[length];
      for (int index = 0; index < length; ++index)
        objArray[index] = this.Read<T>(intPtr + index * MarshalCache<T>.Size);
      return objArray;
    }

    public abstract int Write(IntPtr intPtr, byte[] bytesToWrite);

    public virtual void Write(IntPtr intPtr, string stringToWrite, Encoding encoding)
    {
      if (stringToWrite[stringToWrite.Length - 1] != char.MinValue)
        stringToWrite += "\0";
      byte[] bytes = encoding.GetBytes(stringToWrite);
      this.Write(intPtr, bytes);
    }

    public void Write<T>(IntPtr intPtr, T[] values)
    {
      foreach (T obj in values)
        this.Write<T>(intPtr, obj);
    }

    public abstract void Write<T>(IntPtr intPtr, T value);
  }
}
