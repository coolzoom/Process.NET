// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Detours.DetourManager
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Memory;
using Process.NET.Native.Types;
using System;

namespace Process.NET.Applied.Detours
{
  public class DetourManager : ComplexAppliedManager<Detour>, IDisposable
  {
    public ProcessorArchitecture ProcessorArchitecture { get; set; }

    public DetourManager(IMemory processPlus)
    {
      this.ProcessPlus = processPlus;
      this.ProcessorArchitecture = IntPtr.Size == 8 ? ProcessorArchitecture.X64 : ProcessorArchitecture.X86;
    }

    protected IMemory ProcessPlus { get; }

    public void Dispose() => this.RemoveAll();

    public Detour Create(
      Delegate target,
      Delegate newTarget,
      string name,
      bool ignoreAntiCheatRules = false)
    {
      if ((object) target == null)
        throw new ArgumentNullException(nameof (target));
      if ((object) newTarget == null)
        throw new ArgumentNullException(nameof (newTarget));
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof (name));
      if (!target.IsUnmanagedFunctionPointer())
        throw new Exception("The target delegate does not have the proper UnmanagedFunctionPointer attribute!");
      if (!newTarget.IsUnmanagedFunctionPointer())
        throw new Exception("The new target delegate does not have the proper UnmanagedFunctionPointer attribute!");
      if (this.InternalItems.ContainsKey(name))
        throw new ArgumentException(string.Format("The {0} detour already exists!", (object) name), nameof (name));
      if (this.ProcessorArchitecture == ProcessorArchitecture.X86)
        this.InternalItems[name] = (Detour) new Detour32(target, newTarget, name, this.ProcessPlus, ignoreAntiCheatRules);
      else
        this.InternalItems[name] = (Detour) new Detour64(target, newTarget, name, this.ProcessPlus, ignoreAntiCheatRules);
      return this.InternalItems[name];
    }

    public Detour CreateAndApply(
      Delegate target,
      Delegate newTarget,
      string name,
      bool ignoreAntiCheatRules = false)
    {
      this.Create(target, newTarget, name, ignoreAntiCheatRules);
      this.InternalItems[name].Enable();
      return this.InternalItems[name];
    }
  }
}
