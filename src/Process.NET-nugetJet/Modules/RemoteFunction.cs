// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.RemoteFunction
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using System;
using System.Runtime.InteropServices;

namespace Process.NET.Modules
{
  public class RemoteFunction : MemoryPointer, IProcessFunction
  {
    public RemoteFunction(IProcess processPlus, IntPtr address, string functionName)
      : base(processPlus, address)
    {
      this.Name = functionName;
    }

    public string Name { get; }

    public T GetDelegate<T>() => Marshal.GetDelegateForFunctionPointer<T>(this.BaseAddress);

    public override string ToString() => string.Format("BaseAddress = 0x{0:X} Name = {1}", (object) this.BaseAddress.ToInt64(), (object) this.Name);
  }
}
