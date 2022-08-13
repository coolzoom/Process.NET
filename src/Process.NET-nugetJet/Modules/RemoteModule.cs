// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.RemoteModule
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Memory;
using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Process.NET.Modules
{
  public class RemoteModule : MemoryRegion, IProcessModule, IPointer
  {
    internal static readonly IDictionary<Tuple<string, SafeMemoryHandle>, IProcessFunction> CachedFunctions = (IDictionary<Tuple<string, SafeMemoryHandle>, IProcessFunction>) new Dictionary<Tuple<string, SafeMemoryHandle>, IProcessFunction>();

    public RemoteModule(IProcess processPlus, ProcessModule module)
      : base(processPlus, module.BaseAddress)
    {
      this.Native = module;
    }

    public bool IsMainModule => this.Process.Native.MainModule.BaseAddress == this.BaseAddress;

    public override bool IsValid => base.IsValid && this.Process.Native.Modules.Cast<ProcessModule>().Any<ProcessModule>((Func<ProcessModule, bool>) (m => m.BaseAddress == this.BaseAddress && m.ModuleName == this.Name));

    public string Name => this.Native.ModuleName;

    public ProcessModule Native { get; }

    public string Path => this.Native.FileName;

    public int Size => this.Native.ModuleMemorySize;

    public IProcessFunction this[string functionName]
    {
      get => this.FindFunction(functionName);
      set => RemoteModule.CachedFunctions[Tuple.Create<string, SafeMemoryHandle>(functionName, this.Process.Handle)] = value;
    }

    public void Eject()
    {
      this.Process.ModuleFactory.Eject((IProcessModule) this);
      this.BaseAddress = IntPtr.Zero;
    }

    public IProcessFunction FindFunction(string functionName)
    {
      Tuple<string, SafeMemoryHandle> key = Tuple.Create<string, SafeMemoryHandle>(functionName, this.Process.Handle);
      if (RemoteModule.CachedFunctions.ContainsKey(key))
        return RemoteModule.CachedFunctions[key];
      ProcessModule module = System.Diagnostics.Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault<ProcessModule>((Func<ProcessModule, bool>) (m => m.FileName.ToLower() == this.Path.ToLower()));
      bool flag = false;
      try
      {
        if (module == null)
        {
          flag = true;
          module = ModuleHelper.LoadLibrary(this.Native.FileName);
        }
        long int64_1 = module.GetProcAddress(functionName).ToInt64();
        IntPtr baseAddress = module.BaseAddress;
        long int64_2 = baseAddress.ToInt64();
        long num = int64_1 - int64_2;
        IProcess process = this.Process;
        baseAddress = this.Native.BaseAddress;
        IntPtr address = new IntPtr(baseAddress.ToInt64() + num);
        string functionName1 = functionName;
        RemoteFunction remoteFunction = new RemoteFunction(process, address, functionName1);
        RemoteModule.CachedFunctions.Add(key, (IProcessFunction) remoteFunction);
        return (IProcessFunction) remoteFunction;
      }
      finally
      {
        if (flag)
          module.FreeLibrary();
      }
    }

    internal static void InternalEject(IProcess memorySharp, IProcessModule module) => memorySharp.ThreadFactory.CreateAndJoin(memorySharp["kernel32"]["FreeLibrary"].BaseAddress, (object) module.BaseAddress);

    public override string ToString() => string.Format("BaseAddress = 0x{0:X} Name = {1}", (object) this.BaseAddress.ToInt64(), (object) this.Name);
  }
}
