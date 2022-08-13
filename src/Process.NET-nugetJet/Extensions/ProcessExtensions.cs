// Decompiled with JetBrains decompiler
// Type: Process.NET.Extensions.ProcessExtensions
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Utilities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Process.NET.Extensions
{
  public static class ProcessExtensions
  {
    public static IList<ProcessThread> GetThreads(this System.Diagnostics.Process process) => (IList<ProcessThread>) process.Threads.Cast<ProcessThread>().ToList<ProcessThread>();

    public static IList<ProcessModule> GetModules(this System.Diagnostics.Process process) => (IList<ProcessModule>) process.Modules.Cast<ProcessModule>().ToList<ProcessModule>();

    public static SafeMemoryHandle Open(
      this System.Diagnostics.Process process,
      ProcessAccessFlags processAccessFlags = ProcessAccessFlags.AllAccess)
    {
      return MemoryHelper.OpenProcess(processAccessFlags, process.Id);
    }

    public static string GetVersionInfo(this System.Diagnostics.Process process) => string.Format("{0} {1}.{2}.{3} {4}", (object) process.MainModule.FileVersionInfo.FileDescription, (object) process.MainModule.FileVersionInfo.FileMajorPart, (object) process.MainModule.FileVersionInfo.FileMinorPart, (object) process.MainModule.FileVersionInfo.FileBuildPart, (object) process.MainModule.FileVersionInfo.FilePrivatePart);
  }
}
