// Decompiled with JetBrains decompiler
// Type: Process.NET.Threads.IThreadFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Process.NET.Threads
{
  public interface IThreadFactory : IDisposable
  {
    IRemoteThread this[int threadId] { get; }

    IRemoteThread MainThread { get; }

    IEnumerable<ProcessThread> NativeThreads { get; }

    IEnumerable<IRemoteThread> RemoteThreads { get; }

    IRemoteThread Create(IntPtr address, bool isStarted = true);

    IRemoteThread Create(IntPtr address, object parameter, bool isStarted = true);

    IRemoteThread CreateAndJoin(IntPtr address);

    IRemoteThread CreateAndJoin(IntPtr address, object parameter);

    IRemoteThread GetThreadById(int id);

    void ResumeAll();

    void SuspendAll();
  }
}
