// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.CallingConventions.ThiscallCallingConvention
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Process.NET.Assembly.CallingConventions
{
  public class ThiscallCallingConvention : ICallingConvention
  {
    public string Name => "Stdcall";

    public CleanupTypes Cleanup => CleanupTypes.Callee;

    public string FormatParameters(IntPtr[] parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      List<IntPtr> numList = new List<IntPtr>((IEnumerable<IntPtr>) parameters);
      if (numList.Count > 0)
      {
        stringBuilder.AppendLine("mov ecx, " + numList[0].ToString());
        numList.RemoveAt(0);
      }
      numList.Reverse();
      foreach (IntPtr num in numList)
        stringBuilder.AppendLine("push " + num.ToString());
      return stringBuilder.ToString();
    }

    public string FormatCalling(IntPtr function) => "call " + function.ToString();

    public string FormatCleaning(int nbParameters) => string.Empty;
  }
}
