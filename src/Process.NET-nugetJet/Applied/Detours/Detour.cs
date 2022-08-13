// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Detours.Detour
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Memory;
using System;
using System.Collections.Generic;

namespace Process.NET.Applied.Detours
{
  public abstract class Detour : IComplexApplied, IApplied, IDisposableState, IDisposable
  {
    protected Delegate HookDelegate { get; set; }

    public IMemory ProcessMemory { get; protected set; }

    public IntPtr HookPointer { get; protected set; }

    public List<byte> New { get; protected set; }

    public List<byte> Original { get; protected set; }

    public IntPtr Target { get; protected set; }

    public Delegate TargetDelegate { get; protected set; }

    public bool DisabledDueToRules { get; set; }

    public bool IgnoreRules { get; protected set; }

    public bool IsEnabled { get; set; }

    public string Identifier { get; protected set; }

    public bool IsDisposed { get; internal set; }

    public bool MustBeDisposed { get; set; } = true;

    public void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      if (this.IsEnabled)
        this.Disable();
      GC.SuppressFinalize((object) this);
    }

    public void Enable() => this.Enable(false);

    public void Disable() => this.Disable(false);

    public void Disable(bool disableDueToRules)
    {
      if (this.IgnoreRules & disableDueToRules)
        return;
      this.DisabledDueToRules = disableDueToRules;
      this.ProcessMemory.Write(this.Target, this.Original.ToArray());
      this.IsEnabled = false;
    }

    public void Enable(bool disableDueToRules)
    {
      if (disableDueToRules && this.DisabledDueToRules)
      {
        this.DisabledDueToRules = false;
        this.ProcessMemory.Write(this.Target, this.New.ToArray());
        this.IsEnabled = true;
      }
      else
      {
        if (this.DisabledDueToRules || this.IsEnabled)
          return;
        this.ProcessMemory.Write(this.Target, this.New.ToArray());
        this.IsEnabled = true;
      }
    }

    ~Detour()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    public object CallOriginal(params object[] args)
    {
      this.Disable();
      object obj = this.TargetDelegate.DynamicInvoke(args);
      this.Enable();
      return obj;
    }
  }
}
