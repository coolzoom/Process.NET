// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.FlashWindowFlags
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Native.Types
{
  [Flags]
  public enum FlashWindowFlags
  {
    All = 3,
    Caption = 1,
    Stop = 0,
    Timer = 4,
    TimerNoForeground = 12, // 0x0000000C
    Tray = 2,
  }
}
