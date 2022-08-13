// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Patches.Patch
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Applied.Patches
{
  public class Patch : IComplexApplied, IApplied, IDisposableState, IDisposable
  {
    public Patch(
      IntPtr address,
      byte[] patchWith,
      string identifier,
      IMemory processPlus,
      bool ignoreRules = false)
    {
      this.Identifier = identifier;
      this.ProcessPlus = processPlus;
      this.Address = address;
      this.PatchBytes = patchWith;
      this.OriginalBytes = processPlus.Read(address, patchWith.Length);
      this.IgnoreRules = ignoreRules;
    }

    private IMemory ProcessPlus { get; }

    public IntPtr Address { get; }

    public byte[] OriginalBytes { get; }

    public byte[] PatchBytes { get; }

    public string Identifier { get; }

    public bool IsDisposed { get; private set; }

    public bool IsEnabled { get; set; }

    public bool DisabledDueToRules { get; set; }

    public bool IgnoreRules { get; }

    public void Enable() => this.Enable(false);

    public void Disable() => this.Disable(false);

    public void Disable(bool ignoreRules)
    {
      if (this.IgnoreRules & ignoreRules)
        return;
      this.DisabledDueToRules = ignoreRules;
      this.ProcessPlus.Write(this.Address, this.OriginalBytes);
      this.IsEnabled = false;
    }

    public void Enable(bool disableDueToRules)
    {
      if (disableDueToRules && this.DisabledDueToRules)
      {
        this.DisabledDueToRules = false;
        this.ProcessPlus.Write(this.Address, this.PatchBytes);
        this.IsEnabled = true;
      }
      else
      {
        if (this.DisabledDueToRules)
          return;
        this.ProcessPlus.Write(this.Address, this.PatchBytes);
        this.IsEnabled = true;
      }
    }

    public bool MustBeDisposed { get; set; }

    public void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      if (this.IsEnabled)
        this.Disable();
      GC.SuppressFinalize((object) this);
    }

    ~Patch()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }

    public bool CheckIfEnabled() => ((IEnumerable<byte>) this.ProcessPlus.Read(this.Address, this.PatchBytes.Length)).SequenceEqual<byte>((IEnumerable<byte>) this.PatchBytes);
  }
}
