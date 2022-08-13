// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.ModuleHelper
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Process.NET.Utilities
{
  public static class ModuleHelper
  {
    public static IntPtr GetProcAddress(string moduleName, string functionName)
    {
      IntPtr procAddress = Kernel32.GetProcAddress((System.Diagnostics.Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault<ProcessModule>((Func<ProcessModule, bool>) (m => string.Equals(m.ModuleName, moduleName, StringComparison.CurrentCultureIgnoreCase))) ?? throw new ArgumentException(string.Format("Couldn't get the module {0} because it doesn't exist in the current process.", (object) moduleName))).BaseAddress, functionName);
      return procAddress != IntPtr.Zero ? procAddress : throw new Win32Exception(string.Format("Couldn't get the function address of {0}.", (object) functionName));
    }

    public static void FreeLibrary(string libraryName)
    {
      ProcessModule processModule = System.Diagnostics.Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault<ProcessModule>((Func<ProcessModule, bool>) (m => m.ModuleName.ToLower() == libraryName.ToLower()));
      if (processModule == null)
        throw new ArgumentException(string.Format("Couldn't free the library {0} because it doesn't exist in the current process.", (object) libraryName));
      if (!Kernel32.FreeLibrary(processModule.BaseAddress))
        throw new Win32Exception(string.Format("Couldn't free the library {0}.", (object) libraryName));
    }

    public static ProcessModule LoadLibrary(string libraryPath)
    {
      if (!File.Exists(libraryPath))
        throw new FileNotFoundException(string.Format("Couldn't load the library {0} because the file doesn't exist.", (object) libraryPath));
      if (Kernel32.LoadLibrary(libraryPath) == IntPtr.Zero)
        throw new Win32Exception(string.Format("Couldn't load the library {0}.", (object) libraryPath));
      return System.Diagnostics.Process.GetCurrentProcess().Modules.Cast<ProcessModule>().First<ProcessModule>((Func<ProcessModule, bool>) (m => m.FileName == libraryPath));
    }
  }
}
