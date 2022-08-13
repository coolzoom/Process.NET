// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.CallingConventions.ICallingConvention
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;

namespace Process.NET.Assembly.CallingConventions
{
  public interface ICallingConvention
  {
    string Name { get; }

    CleanupTypes Cleanup { get; }

    string FormatParameters(IntPtr[] parameters);

    string FormatCalling(IntPtr function);

    string FormatCleaning(int nbParameters);
  }
}
