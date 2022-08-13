// Decompiled with JetBrains decompiler
// Type: Process.NET.Threads.FrozenThread
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Threads
{
  public class FrozenThread : IFrozenThread, IDisposable
  {
    public FrozenThread(IRemoteThread thread) => this.Thread = thread;

    public IRemoteThread Thread { get; }

    public virtual void Dispose() => this.Thread.Resume();

    public override string ToString() => string.Format("Id = {0}", (object) this.Thread.Id);
  }
}
