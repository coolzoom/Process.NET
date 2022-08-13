// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.SafeMemoryHandle
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace Process.NET.Native.Types
{
  [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
  public sealed class SafeMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    public SafeMemoryHandle()
      : base(true)
    {
    }

    public SafeMemoryHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected override bool ReleaseHandle() => this.handle != IntPtr.Zero && Kernel32.CloseHandle(this.handle);
  }
}
