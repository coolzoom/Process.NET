// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.ThreadContext
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System.Runtime.InteropServices;

namespace Process.NET.Native.Types
{
  public struct ThreadContext
  {
    public ThreadContextFlags ContextFlags;
    public int Dr0;
    public int Dr1;
    public int Dr2;
    public int Dr3;
    public int Dr6;
    public int Dr7;
    [MarshalAs(UnmanagedType.Struct)]
    public FloatingSaveArea FloatingSave;
    public int SegGs;
    public int SegFs;
    public int SegEs;
    public int SegDs;
    public int Edi;
    public int Esi;
    public int Ebx;
    public int Edx;
    public int Ecx;
    public int Eax;
    public int Ebp;
    public int Eip;
    public int SegCs;
    public int EFlags;
    public int Esp;
    public int SegSs;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
    public byte[] ExtendedRegisters;
  }
}
