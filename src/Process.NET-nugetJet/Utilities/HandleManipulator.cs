// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.HandleManipulator
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native;
using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Process.NET.Utilities
{
  public static class HandleManipulator
  {
    public static void CloseHandle(IntPtr handle)
    {
      HandleManipulator.ValidateAsArgument(handle, nameof (handle));
      if (!Kernel32.CloseHandle(handle))
        throw new Win32Exception("Couldn't close the handle correctly.");
    }

    public static System.Diagnostics.Process HandleToProcess(SafeMemoryHandle processHandle) => ((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcesses()).First<System.Diagnostics.Process>((Func<System.Diagnostics.Process, bool>) (p => p.Id == HandleManipulator.HandleToProcessId(processHandle)));

    public static int HandleToProcessId(SafeMemoryHandle processHandle)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      int processId = Kernel32.GetProcessId(processHandle);
      return processId != 0 ? processId : throw new Win32Exception("Couldn't find the process id of the specified handle.");
    }

    public static ProcessThread HandleToThread(SafeMemoryHandle threadHandle)
    {
      foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
      {
        ProcessThread processThread = process.Threads.Cast<ProcessThread>().FirstOrDefault<ProcessThread>((Func<ProcessThread, bool>) (t => t.Id == HandleManipulator.HandleToThreadId(threadHandle)));
        if (processThread != null)
          return processThread;
      }
      throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static int HandleToThreadId(SafeMemoryHandle threadHandle)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      int threadId = Kernel32.GetThreadId(threadHandle);
      return threadId != 0 ? threadId : throw new Win32Exception("Couldn't find the thread id of the specified handle.");
    }

    public static void ValidateAsArgument(IntPtr handle, string argumentName)
    {
      if (handle == IntPtr.Zero)
        throw new ArgumentException("The handle is not valid.", argumentName);
    }

    public static void ValidateAsArgument(SafeMemoryHandle handle, string argumentName)
    {
      if (handle == null)
        throw new ArgumentNullException(argumentName);
      if (handle.IsClosed || handle.IsInvalid)
        throw new ArgumentException("The handle is not valid or closed.", argumentName);
    }
  }
}
