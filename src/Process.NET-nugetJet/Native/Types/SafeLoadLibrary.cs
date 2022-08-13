// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.SafeLoadLibrary
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Microsoft.Win32.SafeHandles;

namespace Process.NET.Native.Types
{
  public class SafeLoadLibrary : SafeHandleMinusOneIsInvalid
  {
    public SafeLoadLibrary(bool ownsHandle)
      : base(ownsHandle)
    {
    }

    public static unsafe SafeLoadLibrary LoadLibraryEx(
      string library,
      int loadLibraryOptions = 0)
    {
      SafeLoadLibrary safeLoadLibrary = Kernel32.LoadLibraryExW(library, (void*) null, loadLibraryOptions);
      if (safeLoadLibrary.IsInvalid)
        safeLoadLibrary.SetHandleAsInvalid();
      return safeLoadLibrary;
    }

    protected override bool ReleaseHandle() => Kernel32.FreeLibrary(this.handle);
  }
}
