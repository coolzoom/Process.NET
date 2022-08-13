// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.Detours.Detour64
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Memory;
using System;
using System.Collections.Generic;

namespace Process.NET.Applied.Detours
{
  public class Detour64 : Detour
  {
    private readonly byte[] jmp_inst = new byte[20]
    {
      (byte) 80,
      (byte) 72,
      (byte) 184,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 144,
      (byte) 80,
      (byte) 72,
      (byte) 139,
      (byte) 68,
      (byte) 36,
      (byte) 8,
      (byte) 194,
      (byte) 8,
      (byte) 0
    };

    public Detour64(
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
      this.HookDelegate = hook;
      this.HookPointer = hook.ToFuncPtr();
      this.Original = new List<byte>();
      this.Original.AddRange((IEnumerable<byte>) memory.Read(this.Target, this.jmp_inst.Length));
      byte[] bytes = BitConverter.GetBytes(this.HookPointer.ToInt64());
      this.New = new List<byte>()
      {
        (byte) 80,
        (byte) 72,
        (byte) 184
      };
      this.New.AddRange((IEnumerable<byte>) bytes);
      this.New.AddRange((IEnumerable<byte>) new List<byte>()
      {
        (byte) 80,
        (byte) 72,
        (byte) 139,
        (byte) 68,
        (byte) 36,
        (byte) 8,
        (byte) 194,
        (byte) 8,
        (byte) 0
      });
    }
  }
}
