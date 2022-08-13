// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Advapi32
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Runtime.InteropServices;

namespace Process.NET.Native
{
  public static class Advapi32
  {
    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool OpenProcessToken(
      IntPtr processHandle,
      TokenObject desiredAccess,
      out IntPtr tokenHandle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool LookupPrivilegeValue(
      string lpSystemName,
      string lpName,
      out LUID lpLuid);

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AdjustTokenPrivileges(
      IntPtr tokenHandle,
      [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges,
      ref TOKEN_PRIVILEGES newState,
      int zero,
      IntPtr null1,
      IntPtr null2);
  }
}
