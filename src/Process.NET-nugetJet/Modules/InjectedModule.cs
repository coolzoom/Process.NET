// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.InjectedModule
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Threads;
using System;
using System.Diagnostics;
using System.Linq;

namespace Process.NET.Modules
{
  public class InjectedModule : RemoteModule, IDisposableState, IDisposable
  {
    public InjectedModule(IProcess processPlus, ProcessModule module, bool mustBeDisposed = true)
      : base(processPlus, module)
    {
      this.MustBeDisposed = mustBeDisposed;
    }

    public bool IsDisposed { get; private set; }

    public bool MustBeDisposed { get; set; }

    public virtual void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      this.Process.ModuleFactory.Eject((IProcessModule) this);
      GC.SuppressFinalize((object) this);
    }

    ~InjectedModule()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    internal static InjectedModule InternalInject(IProcess memorySharp, string path)
    {
      IRemoteThread thread = memorySharp.ThreadFactory.CreateAndJoin(memorySharp["kernel32"]["LoadLibraryA"].BaseAddress, (object) path);
      return thread.GetExitCode<IntPtr>() != IntPtr.Zero ? new InjectedModule(memorySharp, memorySharp.ModuleFactory.NativeModules.First<ProcessModule>((Func<ProcessModule, bool>) (m => m.BaseAddress == thread.GetExitCode<IntPtr>()))) : (InjectedModule) null;
    }
  }
}
