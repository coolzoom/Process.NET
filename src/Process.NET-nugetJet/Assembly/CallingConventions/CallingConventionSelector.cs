// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.CallingConventions.CallingConventionSelector
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Utilities;
using System;

namespace Process.NET.Assembly.CallingConventions
{
  public static class CallingConventionSelector
  {
    public static ICallingConvention Get(Process.NET.Native.Types.CallingConventions callingConvention)
    {
      switch (callingConvention)
      {
        case Process.NET.Native.Types.CallingConventions.Cdecl:
          return (ICallingConvention) Singleton<CdeclCallingConvention>.Instance;
        case Process.NET.Native.Types.CallingConventions.Stdcall:
          return (ICallingConvention) Singleton<StdcallCallingConvention>.Instance;
        case Process.NET.Native.Types.CallingConventions.Fastcall:
          return (ICallingConvention) Singleton<FastcallCallingConvention>.Instance;
        case Process.NET.Native.Types.CallingConventions.Thiscall:
          return (ICallingConvention) Singleton<ThiscallCallingConvention>.Instance;
        default:
          throw new ApplicationException("Unsupported calling convention.");
      }
    }
  }
}
