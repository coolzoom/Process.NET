// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.IMemory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Text;

namespace Process.NET.Memory
{
  public interface IMemory
  {
    byte[] Read(IntPtr intPtr, int length);

    string Read(IntPtr intPtr, Encoding encoding, int maxLength);

    T Read<T>(IntPtr intPtr);

    T[] Read<T>(IntPtr intPtr, int length);

    int Write(IntPtr intPtr, byte[] bytesToWrite);

    void Write(IntPtr intPtr, string stringToWrite, Encoding encoding);

    void Write<T>(IntPtr intPtr, T[] values);

    void Write<T>(IntPtr intPtr, T value);
  }
}
