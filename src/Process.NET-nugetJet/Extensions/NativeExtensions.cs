// Decompiled with JetBrains decompiler
// Type: Process.NET.Extensions.NativeExtensions
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Extensions
{
  public static class NativeExtensions
  {
    public static int LowWord(this int i) => i & (int) ushort.MaxValue;

    public static int HighWord(this int i) => i >> 16 & (int) ushort.MaxValue;
  }
}
