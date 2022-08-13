// Decompiled with JetBrains decompiler
// Type: Process.NET.Patterns.DwordPattern
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Process.NET.Patterns
{
  public class DwordPattern : IMemoryPattern
  {
    private readonly byte[] _bytes;
    private readonly string _mask;
    public readonly string PatternText;

    public DwordPattern(string dwordPattern)
    {
      this.PatternText = dwordPattern;
      this.PatternType = MemoryPatternType.Function;
      this.Offset = 0;
      this._bytes = DwordPattern.GetBytesFromDwordPattern(dwordPattern);
      this._mask = DwordPattern.GetMaskFromDwordPattern(dwordPattern);
    }

    public DwordPattern(string pattern, int offset)
    {
      this.PatternText = pattern;
      this.PatternType = MemoryPatternType.Data;
      this.Offset = offset;
      this._bytes = DwordPattern.GetBytesFromDwordPattern(pattern);
      this._mask = DwordPattern.GetMaskFromDwordPattern(pattern);
    }

    public IList<byte> GetBytes() => (IList<byte>) this._bytes;

    public string GetMask() => this._mask;

    public int Offset { get; }

    public MemoryPatternType PatternType { get; }

    private static string GetMaskFromDwordPattern(string pattern) => string.Concat(((IEnumerable<string>) pattern.Split(' ')).Select<string, string>((Func<string, string>) (s => !s.Contains<char>('?') ? "x" : "?")));

    private static byte[] GetBytesFromDwordPattern(string pattern) => ((IEnumerable<string>) pattern.Split(' ')).Select<string, byte>((Func<string, byte>) (s => !s.Contains<char>('?') ? byte.Parse(s, NumberStyles.HexNumber) : (byte) 0)).ToArray<byte>();

    public override string ToString() => this.PatternText;
  }
}
