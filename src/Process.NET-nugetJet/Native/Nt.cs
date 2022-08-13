// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Nt
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Runtime.InteropServices;

namespace Process.NET.Native
{
  public static class Nt
  {
    [DllImport("ntdll.dll")]
    public static extern int NtQueryInformationProcess(
      SafeMemoryHandle processHandle,
      ProcessInformationClass infoclass,
      ref ProcessBasicInformation processinfo,
      int length,
      IntPtr bytesread);

    [DllImport("ntdll.dll")]
    public static extern int NtQueryInformationThread(
      SafeMemoryHandle hwnd,
      int infoclass,
      ref ThreadBasicInformation threadinfo,
      int length,
      IntPtr bytesread);
  }
}
