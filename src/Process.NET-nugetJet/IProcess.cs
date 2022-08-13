// Decompiled with JetBrains decompiler
// Type: Process.NET.IProcess
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using Process.NET.Modules;
using Process.NET.Native.Types;
using Process.NET.Threads;
using Process.NET.Windows;
using System;

namespace Process.NET
{
  public interface IProcess : IDisposable
  {
    System.Diagnostics.Process Native { get; set; }

    SafeMemoryHandle Handle { get; set; }

    IMemory Memory { get; set; }

    IThreadFactory ThreadFactory { get; set; }

    IModuleFactory ModuleFactory { get; set; }

    IMemoryFactory MemoryFactory { get; set; }

    IWindowFactory WindowFactory { get; set; }

    IProcessModule this[string moduleName] { get; }

    IPointer this[IntPtr intPtr] { get; }
  }
}
