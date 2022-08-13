// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.WindowStates
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public enum WindowStates
  {
    Hide = 0,
    ShowNormal = 1,
    ShowMinimized = 2,
    Maximize = 3,
    ShowMaximized = 3,
    ShowNormalNoActivate = 4,
    Show = 5,
    Minimize = 6,
    ShowMinNoActive = 7,
    ShowNoActivate = 8,
    Restore = 9,
    ShowDefault = 10, // 0x0000000A
    ForceMinimized = 11, // 0x0000000B
  }
}
