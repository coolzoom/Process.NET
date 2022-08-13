// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Detours.Detour32
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Memory;
using System;
using System.Collections.Generic;

namespace Process.NET.Applied.Detours
{
  public class Detour32 : Detour
  {
    private readonly Delegate _hookDelegate;

    public Detour32(
      Delegate target,
      Delegate hook,
      string identifier,
      IMemory memory,
      bool ignoreRules = false)
    {
      this.ProcessMemory = memory;
      this.Identifier = identifier;
      this.IgnoreRules = ignoreRules;
      this.TargetDelegate = target;
      this.Target = target.ToFuncPtr();
      this._hookDelegate = hook;
      this.HookPointer = hook.ToFuncPtr();
      this.Original = new List<byte>();
      this.Original.AddRange((IEnumerable<byte>) memory.Read(this.Target, 6));
      this.New = new List<byte>() { (byte) 104 };
      this.New.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.HookPointer.ToInt32()));
      this.New.Add((byte) 195);
    }
  }
}
