// Decompiled with JetBrains decompiler
// Type: Process.NET.Extensions.ProcessModuleExtensions
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Utilities;
using System;
using System.Diagnostics;

namespace Process.NET.Extensions
{
  public static class ProcessModuleExtensions
  {
    public static IntPtr GetProcAddress(this ProcessModule module, string functionName) => ModuleHelper.GetProcAddress(module.ModuleName, functionName);

    public static void FreeLibrary(this ProcessModule module) => ModuleHelper.FreeLibrary(module.ModuleName);
  }
}
