// Decompiled with JetBrains decompiler
// Type: Process.NET.Patterns.PatternScanner
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Patterns
{
  public class PatternScanner : IPatternScanner
  {
    private readonly IProcessModule _module;

    public PatternScanner(IProcessModule module)
    {
      this._module = module;
      this.Data = module.Read(0, this._module.Size);
    }

    public byte[] Data { get; }

    public PatternScanResult Find(IMemoryPattern pattern) => pattern.PatternType != MemoryPatternType.Function ? this.FindDataPattern(pattern) : this.FindFunctionPattern(pattern);

    private PatternScanResult FindFunctionPattern(IMemoryPattern pattern)
    {
      byte[] patternData = this.Data;
      int length = patternData.Length;
      for (int offset = 0; offset < length; offset++)
      {
        if (!pattern.GetMask().Where<char>((Func<char, int, bool>) ((m, b) => m == 'x' && (int) pattern.GetBytes()[b] != (int) patternData[b + offset])).Any<char>())
          return new PatternScanResult()
          {
            BaseAddress = this._module.BaseAddress + offset,
            ReadAddress = this._module.BaseAddress + offset,
            Offset = offset,
            Found = true
          };
      }
      return new PatternScanResult()
      {
        BaseAddress = IntPtr.Zero,
        ReadAddress = IntPtr.Zero,
        Offset = 0,
        Found = false
      };
    }

    private PatternScanResult FindDataPattern(IMemoryPattern pattern)
    {
      byte[] patternData = this.Data;
      IList<byte> patternBytes = pattern.GetBytes();
      string mask = pattern.GetMask();
      PatternScanResult patternScanResult = new PatternScanResult();
      for (int offset = 0; offset < patternData.Length; offset++)
      {
        if (!mask.Where<char>((Func<char, int, bool>) ((m, b) => m == 'x' && (int) patternBytes[b] != (int) patternData[b + offset])).Any<char>())
        {
          patternScanResult.Found = true;
          patternScanResult.ReadAddress = this._module.Read<IntPtr>(offset + pattern.Offset);
          patternScanResult.BaseAddress = new IntPtr(patternScanResult.ReadAddress.ToInt64() - this._module.BaseAddress.ToInt64());
          patternScanResult.Offset = offset;
          return patternScanResult;
        }
      }
      patternScanResult.Found = false;
      patternScanResult.Offset = 0;
      patternScanResult.ReadAddress = IntPtr.Zero;
      patternScanResult.BaseAddress = IntPtr.Zero;
      return patternScanResult;
    }
  }
}
