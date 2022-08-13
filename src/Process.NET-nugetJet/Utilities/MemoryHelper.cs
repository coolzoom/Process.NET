// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.MemoryHelper
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native;
using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Process.NET.Utilities
{
  public static class MemoryHelper
  {
    public static IntPtr Allocate(
      SafeMemoryHandle processHandle,
      int size,
      MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
      MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      IntPtr num = Kernel32.VirtualAllocEx(processHandle, IntPtr.Zero, size, allocationFlags, protectionFlags);
      return num != IntPtr.Zero ? num : throw new Win32Exception(string.Format("Couldn't allocate memory of {0} byte(s).", (object) size));
    }

    public static void CloseHandle(IntPtr handle)
    {
      HandleManipulator.ValidateAsArgument(handle, nameof (handle));
      if (!Kernel32.CloseHandle(handle))
        throw new Win32Exception(string.Format("Couldn't close he handle 0x{0}.", (object) handle));
    }

    public static void Free(SafeMemoryHandle processHandle, IntPtr address)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      HandleManipulator.ValidateAsArgument(address, nameof (address));
      if (!Kernel32.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release))
        throw new Win32Exception(string.Format("The memory page 0x{0} cannot be freed.", (object) address.ToString("X")));
    }

    public static ProcessBasicInformation NtQueryInformationProcess(
      SafeMemoryHandle processHandle)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      ProcessBasicInformation processinfo = new ProcessBasicInformation();
      int num = Nt.NtQueryInformationProcess(processHandle, ProcessInformationClass.ProcessBasicInformation, ref processinfo, processinfo.Size, IntPtr.Zero);
      if (num == 0)
        return processinfo;
      throw new ApplicationException(string.Format("Couldn't get the information from the process, error code '{0}'.", (object) num));
    }

    public static SafeMemoryHandle OpenProcess(
      ProcessAccessFlags accessFlags,
      int processId)
    {
      SafeMemoryHandle safeMemoryHandle = Kernel32.OpenProcess(accessFlags, false, processId);
      if (!safeMemoryHandle.IsInvalid && !safeMemoryHandle.IsClosed)
        return safeMemoryHandle;
      throw new Win32Exception(string.Format("Couldn't open the process {0}.", (object) processId));
    }

    public static byte[] ReadBytes(SafeMemoryHandle processHandle, IntPtr address, int size)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      HandleManipulator.ValidateAsArgument(address, nameof (address));
      byte[] lpBuffer = new byte[size];
      int lpNumberOfBytesRead;
      if (Kernel32.ReadProcessMemory(processHandle, address, lpBuffer, size, out lpNumberOfBytesRead) && size == lpNumberOfBytesRead)
        return lpBuffer;
      throw new Win32Exception(string.Format("Couldn't read {0} byte(s) from 0x{1}.", (object) size, (object) address.ToString("X")));
    }

    public static MemoryProtectionFlags ChangeProtection(
      SafeMemoryHandle processHandle,
      IntPtr address,
      int size,
      MemoryProtectionFlags protection)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      HandleManipulator.ValidateAsArgument(address, nameof (address));
      MemoryProtectionFlags lpflOldProtect;
      if (Kernel32.VirtualProtectEx(processHandle, address, size, protection, out lpflOldProtect))
        return lpflOldProtect;
      throw new Win32Exception(string.Format("Couldn't change the protection of the memory at 0x{0} of {1} byte(s) to {2}.", (object) address.ToString("X"), (object) size, (object) protection));
    }

    public static MemoryBasicInformation Query(
      SafeMemoryHandle processHandle,
      IntPtr baseAddress)
    {
      MemoryBasicInformation lpBuffer;
      if (Kernel32.VirtualQueryEx(processHandle, baseAddress, out lpBuffer, MarshalType<MemoryBasicInformation>.Size) != 0)
        return lpBuffer;
      throw new Win32Exception(string.Format("Couldn't query information about the memory region 0x{0}", (object) baseAddress.ToString("X")));
    }

    public static IEnumerable<MemoryBasicInformation> Query(
      SafeMemoryHandle processHandle,
      IntPtr addressFrom,
      IntPtr addressTo)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      long numberFrom = addressFrom.ToInt64();
      long numberTo = addressTo.ToInt64();
      if (numberFrom >= numberTo)
        throw new ArgumentException("The starting address must be lower than the ending address.", nameof (addressFrom));
      int ret;
      do
      {
        MemoryBasicInformation lpBuffer;
        ret = Kernel32.VirtualQueryEx(processHandle, new IntPtr(numberFrom), out lpBuffer, MarshalType<MemoryBasicInformation>.Size);
        numberFrom += (long) lpBuffer.RegionSize;
        if (lpBuffer.State != MemoryStateFlags.Free)
          yield return lpBuffer;
      }
      while (numberFrom < numberTo && ret != 0);
    }

    public static int WriteBytes(SafeMemoryHandle processHandle, IntPtr address, byte[] byteArray)
    {
      HandleManipulator.ValidateAsArgument(processHandle, nameof (processHandle));
      HandleManipulator.ValidateAsArgument(address, nameof (address));
      int lpNumberOfBytesWritten;
      if (Kernel32.WriteProcessMemory(processHandle, address, byteArray, byteArray.Length, out lpNumberOfBytesWritten) && lpNumberOfBytesWritten == byteArray.Length)
        return lpNumberOfBytesWritten;
      throw new Win32Exception(string.Format("Couldn't write {0} bytes to 0x{1}", (object) byteArray.Length, (object) address.ToString("X")));
    }
  }
}
