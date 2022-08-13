// Decompiled with JetBrains decompiler
// Type: Process.NET.Threads.IRemoteThread
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Diagnostics;

namespace Process.NET.Threads
{
  public interface IRemoteThread : IDisposable
  {
    ThreadContext Context { get; set; }

    SafeMemoryHandle Handle { get; }

    int Id { get; }

    bool IsAlive { get; }

    bool IsMainThread { get; }

    bool IsSuspended { get; }

    bool IsTerminated { get; }

    ProcessThread Native { get; }

    T GetExitCode<T>();

    int GetHashCode();

    IntPtr GetRealSegmentAddress(SegmentRegisters segment);

    void Join();

    WaitValues Join(TimeSpan time);

    void Refresh();

    void Resume();

    IFrozenThread Suspend();

    void Terminate(int exitCode = 0);
  }
}
