﻿// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.WindowPlacement
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public struct WindowPlacement
  {
    public int Length;
    public int Flags;
    public WindowStates ShowCmd;
    public Point MinPosition;
    public Point MaxPosition;
    public Rectangle NormalPosition;
  }
}
