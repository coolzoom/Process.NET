// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.ThreadHelper
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native;
using Process.NET.Native.Types;
using System;
using System.ComponentModel;

namespace Process.NET.Utilities
{
  public static class ThreadHelper
  {
    public static SafeMemoryHandle CreateRemoteThread(
      SafeMemoryHandle processHandle,
      IntPtr startAddress,
      IntPtr parameter,
      ThreadCreationFlags creationFlags = ThreadCreationFlags.Run)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      HandleManipulator.ValidateAsArgument(startAddress, nameof (startAddress));
      SafeMemoryHandle remoteThread = Kernel32.CreateRemoteThread(processHandle, IntPtr.Zero, 0, startAddress, parameter, creationFlags, out int _);
      if (!remoteThread.IsClosed && !remoteThread.IsInvalid)
        return remoteThread;
      throw new Win32Exception(string.Format("Couldn't create the thread at 0x{0}.", (object) startAddress.ToString("X")));
    }

    public static IntPtr? GetExitCodeThread(SafeMemoryHandle threadHandle)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      IntPtr lpExitCode;
      if (!Kernel32.GetExitCodeThread(threadHandle, out lpExitCode))
        throw new Win32Exception("Couldn't get the exit code of the thread.");
      return lpExitCode == new IntPtr(259) ? new IntPtr?() : new IntPtr?(lpExitCode);
    }

    public static ThreadContext GetThreadContext(
      SafeMemoryHandle threadHandle,
      ThreadContextFlags contextFlags = ThreadContextFlags.Full)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      ThreadContext lpContext = new ThreadContext()
      {
        ContextFlags = contextFlags
      };
      return Kernel32.GetThreadContext(threadHandle, ref lpContext) ? lpContext : throw new Win32Exception("Couldn't get the thread context.");
    }

    public static LdtEntry GetThreadSelectorEntry(
      SafeMemoryHandle threadHandle,
      int selector)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      LdtEntry lpSelectorEntry;
      if (Kernel32.GetThreadSelectorEntry(threadHandle, selector, out lpSelectorEntry))
        return lpSelectorEntry;
      throw new Win32Exception(string.Format("Couldn't get the selector entry for this selector: {0}.", (object) selector));
    }

    public static SafeMemoryHandle OpenThread(
      ThreadAccessFlags accessFlags,
      int threadId)
    {
      SafeMemoryHandle safeMemoryHandle = Kernel32.OpenThread(accessFlags, false, threadId);
      if (!safeMemoryHandle.IsClosed && !safeMemoryHandle.IsInvalid)
        return safeMemoryHandle;
      throw new Win32Exception(string.Format("Couldn't open the thread #{0}.", (object) threadId));
    }

    public static ThreadBasicInformation NtQueryInformationThread(
      SafeMemoryHandle threadHandle)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      ThreadBasicInformation threadinfo = new ThreadBasicInformation();
      int num = Nt.NtQueryInformationThread(threadHandle, 0, ref threadinfo, MarshalType<ThreadBasicInformation>.Size, IntPtr.Zero);
      if (num == 0)
        return threadinfo;
      throw new ApplicationException(string.Format("Couldn't get the information from the thread, error code '{0}'.", (object) num));
    }

    public static int ResumeThread(SafeMemoryHandle threadHandle)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      int num = Kernel32.ResumeThread(threadHandle);
      return num != int.MaxValue ? num : throw new Win32Exception("Couldn't resume the thread.");
    }

    public static void SetThreadContext(SafeMemoryHandle threadHandle, ThreadContext context)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      if (!Kernel32.SetThreadContext(threadHandle, ref context))
        throw new Win32Exception("Couldn't set the thread context.");
    }

    public static int SuspendThread(SafeMemoryHandle threadHandle)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      int num = Kernel32.SuspendThread(threadHandle);
      return num != int.MaxValue ? num : throw new Win32Exception("Couldn't suspend the thread.");
    }

    public static void TerminateThread(SafeMemoryHandle threadHandle, int exitCode)
    {
      HandleManipulator.ValidateAsArgument(threadHandle, nameof (threadHandle));
      if (!Kernel32.TerminateThread(threadHandle, exitCode))
        throw new Win32Exception("Couldn't terminate the thread.");
    }

    public static WaitValues WaitForSingleObject(
      SafeMemoryHandle handle,
      TimeSpan? timeout)
    {
      HandleManipulator.ValidateAsArgument(handle, nameof (handle));
      int num = (int) Kernel32.WaitForSingleObject(handle, timeout.HasValue ? Convert.ToUInt32(timeout.Value.TotalMilliseconds) : 0U);
      return num != -1 ? (WaitValues) num : throw new Win32Exception("The WaitForSingleObject function call failed.");
    }

    public static WaitValues WaitForSingleObject(SafeMemoryHandle handle)
    {
      HandleManipulator.ValidateAsArgument(handle, nameof (handle));
      int num = (int) Kernel32.WaitForSingleObject(handle, uint.MaxValue);
      return num != -1 ? (WaitValues) num : throw new Win32Exception("The WaitForSingleObject function call failed.");
    }
  }
}
