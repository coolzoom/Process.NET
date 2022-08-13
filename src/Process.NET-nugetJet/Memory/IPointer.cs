// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.IPointer
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Text;

namespace Process.NET.Memory
{
  public interface IPointer
  {
    IntPtr BaseAddress { get; }

    bool IsValid { get; }

    byte[] Read(int offset, int length);

    string Read(int offset, Encoding encoding, int maxLength);

    T Read<T>(int offset);

    T[] Read<T>(int offset, int length);

    int Write(int offset, byte[] toWrite);

    void Write(int offset, string stringToWrite, Encoding encoding);

    void Write<T>(int offset, T[] values);

    void Write<T>(int offset, T value);
  }
}
