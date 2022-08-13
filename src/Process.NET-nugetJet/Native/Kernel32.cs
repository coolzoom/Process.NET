// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Kernel32
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Windows;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Process.NET.Native
{
  public static class Kernel32
  {
    static Kernel32()
    {
      IntPtr moduleHandle = Kernel32.GetModuleHandle("kernel32.dll");
      if (moduleHandle == IntPtr.Zero)
        throw new Exception("Failed to get kernel32.dll module handle");
      Kernel32.Is32BitSystem = Kernel32.GetProcAddress(moduleHandle, "IsWow64Process") == IntPtr.Zero;
    }

    public static bool Is32BitSystem { get; }

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    public static extern int GetPrivateProfileString(
      string section,
      string key,
      string defaultValue,
      StringBuilder value,
      int size,
      string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetPrivateProfileString(
      string section,
      string key,
      string defaultValue,
      [In, Out] char[] value,
      int size,
      string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern int GetPrivateProfileSection(
      string section,
      IntPtr keyValue,
      int size,
      string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool WritePrivateProfileString(
      string section,
      string key,
      string value,
      string filePath);

    [DllImport("user32.dll")]
    public static extern int UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    public static extern int CallNextHookEx(
      IntPtr hhk,
      int nCode,
      IntPtr wParam,
      ref KBDLLHOOKSTRUCT lParam);

    [DllImport("kernel32.dll")]
    public static extern int GetCurrentThreadId();

    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
    public static extern unsafe void MoveMemory(void* dest, void* src, int size);

    [DllImport("kernel32.dll")]
    public static extern void GetSystemInfo(out SystemInfo input);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWow64Process([In] IntPtr process, out bool wow64Process);

    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true)]
    public static extern IntPtr GetProcAddressOrdinal(int hModule, int procName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr LoadLibraryA(string lpFileName);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DebugActiveProcess(int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DebugActiveProcessStop(int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DebugSetProcessKillOnExit(bool KillOnExit);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern unsafe SafeLoadLibrary LoadLibraryExW(
      [In] string lpwLibFileName,
      [In] void* hFile,
      [In] int dwFlags);

    public static bool Is32BitProcess(IntPtr hProcess)
    {
      bool wow64Process;
      return Kernel32.Is32BitSystem || Kernel32.IsWow64Process(hProcess, out wow64Process) & wow64Process;
    }

    [DllImport("kernel32.dll")]
    public static extern bool FindClose(IntPtr hFindFile);

    [DllImport("Kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CheckRemoteDebuggerPresent(
      IntPtr hProcess,
      [MarshalAs(UnmanagedType.Bool)] ref bool isDebuggerPresent);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int SuspendThread(SafeMemoryHandle hThread);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool TerminateThread(SafeMemoryHandle hThread, int dwExitCode);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr VirtualAllocEx(
      SafeMemoryHandle hProcess,
      IntPtr lpAddress,
      int dwSize,
      MemoryAllocationFlags flAllocationType,
      MemoryProtectionFlags flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool VirtualFreeEx(
      SafeMemoryHandle hProcess,
      IntPtr lpAddress,
      int dwSize,
      MemoryReleaseFlags dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool VirtualProtectEx(
      SafeMemoryHandle hProcess,
      IntPtr lpAddress,
      int dwSize,
      MemoryProtectionFlags flNewProtect,
      out MemoryProtectionFlags lpflOldProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int VirtualQueryEx(
      SafeMemoryHandle hProcess,
      IntPtr lpAddress,
      out MemoryBasicInformation lpBuffer,
      int dwLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern WaitValues WaitForSingleObject(
      SafeMemoryHandle hHandle,
      uint dwMilliseconds);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool WriteProcessMemory(
      SafeMemoryHandle hProcess,
      IntPtr lpBaseAddress,
      byte[] lpBuffer,
      int nSize,
      out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetThreadContext(
      SafeMemoryHandle hThread,
      [MarshalAs(UnmanagedType.Struct)] ref ThreadContext lpContext);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ReadProcessMemory(
      SafeMemoryHandle hProcess,
      IntPtr lpBaseAddress,
      [Out] byte[] lpBuffer,
      int dwSize,
      out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int ResumeThread(SafeMemoryHandle hThread);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern SafeMemoryHandle OpenProcess(
      ProcessAccessFlags dwDesiredAccess,
      [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
      int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern SafeMemoryHandle OpenThread(
      ThreadAccessFlags dwDesiredAccess,
      [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
      int dwThreadId);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetExitCodeThread(SafeMemoryHandle hThread, out IntPtr lpExitCode);

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetProcessId(SafeMemoryHandle hProcess);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetSystemMetrics(SystemMetrics metric);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetThreadContext(
      SafeMemoryHandle hThread,
      ref ThreadContext lpContext);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetThreadSelectorEntry(
      SafeMemoryHandle hThread,
      int dwSelector,
      out LdtEntry lpSelectorEntry);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetThreadId(SafeMemoryHandle hThread);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern SafeMemoryHandle CreateRemoteThread(
      SafeMemoryHandle hProcess,
      IntPtr lpThreadAttributes,
      int dwStackSize,
      IntPtr lpStartAddress,
      IntPtr lpParameter,
      ThreadCreationFlags dwCreationFlags,
      out int lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [SecurityCritical]
    [SecuritySafeCritical]
    internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex) => IntPtr.Size != 4 ? Kernel32.GetWindowLongPtr64(hWnd, nIndex) : Kernel32.GetWindowLong32(hWnd, nIndex);

    [SecurityCritical]
    [SecuritySafeCritical]
    public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr newValue) => IntPtr.Size != 4 ? Kernel32.SetWindowLongPtr64(hWnd, nIndex, newValue) : Kernel32.SetWindowLong32(hWnd, nIndex, newValue);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(
      IntPtr hhk,
      int nCode,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowsHookEx(
      HookType code,
      HookProc func,
      IntPtr hInstance,
      int threadId);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    public static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr newValue);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr newValue);

    [DllImport("user32.dll")]
    public static extern IntPtr CallWindowProc(
      IntPtr lpPrevWndFunc,
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern int RegisterWindowMessage(string lpString);

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    public static IntPtr SendMessage(Message message) => Kernel32.SendMessage(message.HWnd, message.Msg, message.WParam, message.LParam);
  }
}
