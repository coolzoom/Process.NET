// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.Randomizer
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Text;

namespace Process.NET.Utilities
{
  public static class Randomizer
  {
    private static readonly Random Random = new Random();
    private static readonly char[] AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public static string GenerateString(int minSize = 40, int maxSize = 40)
    {
      StringBuilder stringBuilder = new StringBuilder(Randomizer.GenerateNumber(minSize, maxSize));
      for (int index = 0; index < stringBuilder.Capacity; ++index)
        stringBuilder.Append(Randomizer.AllowedChars[Randomizer.GenerateNumber(Randomizer.AllowedChars.Length - 1)]);
      return stringBuilder.ToString();
    }

    public static Guid GenerateGuid() => Guid.NewGuid();

    public static int GenerateNumber(int minValue, int maxValue) => Randomizer.Random.Next(minValue, maxValue);

    public static int GenerateNumber(int maxValue) => Randomizer.Random.Next(maxValue);

    public static int GenerateNumber() => Randomizer.Random.Next();
  }
}
