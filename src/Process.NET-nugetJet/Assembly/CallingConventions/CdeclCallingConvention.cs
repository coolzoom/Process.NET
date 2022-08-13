// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.CallingConventions.CdeclCallingConvention
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Process.NET.Assembly.CallingConventions
{
  public class CdeclCallingConvention : ICallingConvention
  {
    public string Name => "Cdecl";

    public CleanupTypes Cleanup => CleanupTypes.Caller;

    public string FormatParameters(IntPtr[] parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (IntPtr num in ((IEnumerable<IntPtr>) parameters).Reverse<IntPtr>())
        stringBuilder.AppendLine("push " + num.ToString());
      return stringBuilder.ToString();
    }

    public string FormatCalling(IntPtr function) => "call " + function.ToString();

    public string FormatCleaning(int nbParameters) => "add esp, " + (object) (nbParameters * 4);
  }
}
