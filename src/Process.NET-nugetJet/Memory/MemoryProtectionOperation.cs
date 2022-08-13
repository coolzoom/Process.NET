// Decompiled with JetBrains decompiler
// Type: Process.NET.Memory.MemoryProtectionOperation
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Runtime.InteropServices;

namespace Process.NET.Memory
{
  public class MemoryProtectionOperation : IDisposable
  {
    private readonly IntPtr _hProcess;
    private readonly int _oldProtect;
    private readonly int _size;
    private readonly MemoryProtectionType _type;
    public readonly IntPtr Address;

    public MemoryProtectionOperation(IntPtr hProcess, IntPtr address, int size, int flNewProtect)
    {
      this._hProcess = hProcess;
      this.Address = address;
      this._size = size;
      this._type = MemoryProtectionType.External;
      MemoryProtectionOperation.VirtualProtectEx(hProcess, this.Address, size, flNewProtect, out this._oldProtect);
    }

    public MemoryProtectionOperation(IntPtr address, int size, int flNewProtect)
    {
      this.Address = address;
      this._size = size;
      this._type = MemoryProtectionType.Local;
      MemoryProtectionOperation.VirtualProtect(this.Address, size, flNewProtect, out this._oldProtect);
    }

    public void Dispose()
    {
      int lpflOldProtect;
      if (this._type == MemoryProtectionType.Local)
        MemoryProtectionOperation.VirtualProtectEx(this._hProcess, this.Address, this._size, this._oldProtect, out lpflOldProtect);
      else
        MemoryProtectionOperation.VirtualProtect(this.Address, this._size, this._oldProtect, out lpflOldProtect);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool VirtualProtect(
      IntPtr lpAddress,
      int dwSize,
      int flNewProtect,
      out int lpflOldProtect);

    [DllImport("kernel32.dll")]
    private static extern bool VirtualProtectEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      int dwSize,
      int flNewProtect,
      out int lpflOldProtect);
  }
}
