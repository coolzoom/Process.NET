// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.SystemInfo
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  public struct SystemInfo
  {
    public ProcessorArchitecture ProcessorArchitecture;
    public int PageSize;
    public IntPtr MinimumApplicationAddress;
    public IntPtr MaximumApplicationAddress;
    public IntPtr ActiveProcessorMask;
    public int NumberOfProcessors;
    public int ProcessorType;
    public int AllocationGranularity;
    public ushort ProcessorLevel;
    public ushort ProcessorRevision;
  }
}
