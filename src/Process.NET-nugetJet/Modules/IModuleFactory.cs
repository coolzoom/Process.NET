// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.IModuleFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Process.NET.Modules
{
  public interface IModuleFactory : IDisposable
  {
    IProcessModule this[string moduleName] { get; }

    IEnumerable<InjectedModule> InjectedModules { get; }

    IProcessModule MainModule { get; }

    IEnumerable<IProcessModule> RemoteModules { get; }

    IEnumerable<ProcessModule> NativeModules { get; }

    void Eject(string moduleName);

    void Eject(IProcessModule module);

    InjectedModule Inject(string path, bool mustBeDisposed = true);
  }
}
