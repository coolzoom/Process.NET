// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.ModuleFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Process.NET.Modules
{
  public class ModuleFactory : IModuleFactory, IDisposable
  {
    protected readonly List<InjectedModule> InternalInjectedModules;
    protected readonly IProcess ProcessPlus;

    public ModuleFactory(IProcess processPlus)
    {
      this.ProcessPlus = processPlus;
      this.InternalInjectedModules = new List<InjectedModule>();
    }

    public IPointer this[IntPtr address] => (IPointer) new MemoryPointer(this.ProcessPlus, address);

    public IProcessModule MainModule => this.FetchModule(this.ProcessPlus.Native.MainModule);

    public IEnumerable<IProcessModule> RemoteModules => this.NativeModules.Select<ProcessModule, IProcessModule>(new Func<ProcessModule, IProcessModule>(this.FetchModule));

    public IEnumerable<ProcessModule> NativeModules => this.ProcessPlus.Native.Modules.Cast<ProcessModule>();

    public IProcessModule this[string moduleName] => this.FetchModule(moduleName);

    public virtual void Dispose()
    {
      foreach (InjectedModule injectedModule in this.InternalInjectedModules.Where<InjectedModule>((Func<InjectedModule, bool>) (m => m.MustBeDisposed)))
        injectedModule.Dispose();
      foreach (KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction> keyValuePair in ((IEnumerable<KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction>>) RemoteModule.CachedFunctions.ToArray<KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction>>()).Where<KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction>>((Func<KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction>, bool>) (cachedFunction => cachedFunction.Key.Item2 == this.ProcessPlus.Handle)))
        ((ICollection<KeyValuePair<Tuple<string, SafeMemoryHandle>, IProcessFunction>>) RemoteModule.CachedFunctions).Remove(keyValuePair);
      GC.SuppressFinalize((object) this);
    }

    public IEnumerable<InjectedModule> InjectedModules => (IEnumerable<InjectedModule>) this.InternalInjectedModules.AsReadOnly();

    public void Eject(string moduleName)
    {
      IProcessModule module = this.RemoteModules.FirstOrDefault<IProcessModule>((Func<IProcessModule, bool>) (m => m.Name == moduleName));
      if (module == null)
        return;
      RemoteModule.InternalEject(this.ProcessPlus, module);
    }

    public InjectedModule Inject(string path, bool mustBeDisposed = true)
    {
      InjectedModule injectedModule = InjectedModule.InternalInject(this.ProcessPlus, path);
      this.InternalInjectedModules.Add(injectedModule);
      return injectedModule;
    }

    public void Eject(IProcessModule module)
    {
      if (!module.IsValid)
        return;
      InjectedModule injectedModule = this.InternalInjectedModules.FirstOrDefault<InjectedModule>((Func<InjectedModule, bool>) (m => m.Equals((object) module)));
      if ((MemoryRegion) injectedModule != (MemoryRegion) null)
        this.InternalInjectedModules.Remove(injectedModule);
      RemoteModule.InternalEject(this.ProcessPlus, module);
    }

    ~ModuleFactory() => this.Dispose();

    public IProcessModule FetchModule(string moduleName)
    {
      moduleName = moduleName.ToLower();
      if (!Path.HasExtension(moduleName))
        moduleName += ".dll";
      return (IProcessModule) new RemoteModule(this.ProcessPlus, this.NativeModules.First<ProcessModule>((Func<ProcessModule, bool>) (m => m.ModuleName.ToLower() == moduleName)));
    }

    public IProcessModule FetchModule(ProcessModule module) => this.FetchModule(module.ModuleName);
  }
}
