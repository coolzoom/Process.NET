// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.ProcessHelper
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Extensions;
using Process.NET.Native;
using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Utilities
{
  public static class ProcessHelper
  {
    private const string SE_DEBUG_NAME = "SeDebugPrivilege";

    public static IEnumerable<IntPtr> TopLevelWindows => WindowHelper.EnumTopLevelWindows();

    public static IEnumerable<IntPtr> Windows => WindowHelper.EnumAllWindows();

    public static IntPtr GetMainWindowHandle(string processName) => (((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcessesByName(processName)).FirstOrDefault<System.Diagnostics.Process>() ?? throw new ArgumentNullException("process")).MainWindowHandle;

    public static System.Diagnostics.Process FromProcessId(int processId) => System.Diagnostics.Process.GetProcessById(processId);

    public static IEnumerable<System.Diagnostics.Process> FromWindowClassName(
      string className)
    {
      return ProcessHelper.Windows.Where<IntPtr>((Func<IntPtr, bool>) (window => WindowHelper.GetClassName(window) == className)).Select<IntPtr, System.Diagnostics.Process>(new Func<IntPtr, System.Diagnostics.Process>(ProcessHelper.FromWindowHandle));
    }

    public static System.Diagnostics.Process FromWindowHandle(IntPtr windowHandle) => ProcessHelper.FromProcessId(WindowHelper.GetWindowProcessId(windowHandle));

    public static IEnumerable<System.Diagnostics.Process> FromWindowTitle(
      string windowTitle)
    {
      return ProcessHelper.Windows.Where<IntPtr>((Func<IntPtr, bool>) (window => WindowHelper.GetWindowText(window) == windowTitle)).Select<IntPtr, System.Diagnostics.Process>(new Func<IntPtr, System.Diagnostics.Process>(ProcessHelper.FromWindowHandle));
    }

    public static IEnumerable<System.Diagnostics.Process> FromWindowTitleContains(
      string windowTitle)
    {
      return ProcessHelper.Windows.Where<IntPtr>((Func<IntPtr, bool>) (window => WindowHelper.GetWindowText(window).Contains(windowTitle))).Select<IntPtr, System.Diagnostics.Process>(new Func<IntPtr, System.Diagnostics.Process>(ProcessHelper.FromWindowHandle));
    }

    public static IEnumerable<System.Diagnostics.Process> CollectFromInternalName(
      string name)
    {
      return ((IEnumerable<System.Diagnostics.Process>) ((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcesses()).ToArray<System.Diagnostics.Process>()).Where<System.Diagnostics.Process>((Func<System.Diagnostics.Process, bool>) (process => !string.IsNullOrEmpty(process.MainModule.FileVersionInfo.InternalName) && string.Equals(process.MainModule.FileVersionInfo.InternalName, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public static IEnumerable<System.Diagnostics.Process> FindProcessesByProductName(
      string name)
    {
      return new List<System.Diagnostics.Process>((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcesses()).Where<System.Diagnostics.Process>((Func<System.Diagnostics.Process, bool>) (process => !string.IsNullOrEmpty(process.MainModule.FileVersionInfo.ProductName) && string.Equals(process.MainModule.FileVersionInfo.ProductName, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public static System.Diagnostics.Process FromName(string name) => ((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcessesByName(name)).FirstOrDefault<System.Diagnostics.Process>() ?? throw new ArgumentNullException("process");

    public static System.Diagnostics.Process GetByInternalName(string name) => ProcessHelper.CollectFromInternalName(name).FirstOrDefault<System.Diagnostics.Process>();

    public static System.Diagnostics.Process GetByProductName(string name)
    {
      using (System.Diagnostics.Process process = ProcessHelper.FindProcessesByProductName(name).FirstOrDefault<System.Diagnostics.Process>())
        return process != null ? process : throw new NullReferenceException(string.Format("Process {0} not found.", (object) name));
    }

    public static System.Diagnostics.Process SelectFromName(string internalName)
    {
label_0:
      List<System.Diagnostics.Process> list = ProcessHelper.CollectFromInternalName(internalName).ToList<System.Diagnostics.Process>();
      if (list.Count == 0)
        throw new Exception(string.Format("No '{0}' processes found", (object) internalName));
      try
      {
        if (list.Count == 1)
          return list[0];
        Console.WriteLine("Select process:");
        for (int index = 0; index < list.Count; ++index)
        {
          bool isDebuggerPresent = false;
          Kernel32.CheckRemoteDebuggerPresent(list[index].Handle, ref isDebuggerPresent);
          Console.WriteLine(string.Format("[{0}] {1} PID: {2} {3}", (object) index, (object) list[index].GetVersionInfo(), (object) list[index].Id, isDebuggerPresent ? (object) "(Already debugging)" : (object) ""));
        }
        Console.WriteLine();
        Console.Write("> ");
        int int32 = Convert.ToInt32(Console.ReadLine());
        return list[int32];
      }
      catch (Exception ex)
      {
        if (list.Count == 1)
          throw new Exception(ex.Message);
        goto label_0;
      }
    }

    public static bool SetDebugPrivileges()
    {
      IntPtr tokenHandle;
      if (!Advapi32.OpenProcessToken(Kernel32.GetCurrentProcess(), TokenObject.TOKEN_QUERY | TokenObject.TOKEN_ADJUST_PRIVILEGES, out tokenHandle))
        return false;
      LUID lpLuid;
      if (!Advapi32.LookupPrivilegeValue((string) null, "SeDebugPrivilege", out lpLuid))
      {
        Kernel32.CloseHandle(tokenHandle);
        return false;
      }
      TOKEN_PRIVILEGES newState;
      newState.PrivilegeCount = 1;
      newState.Luid = lpLuid;
      newState.Attributes = PrivilegeAttributes.SE_PRIVILEGE_ENABLED;
      if (Advapi32.AdjustTokenPrivileges(tokenHandle, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero))
        return Kernel32.CloseHandle(tokenHandle);
      Kernel32.CloseHandle(tokenHandle);
      return false;
    }
  }
}
