// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.FloatingSaveArea
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System.Runtime.InteropServices;

namespace Process.NET.Native.Types
{
  public struct FloatingSaveArea
  {
    public int ControlWord;
    public int StatusWord;
    public int TagWord;
    public int ErrorOffset;
    public int ErrorSelector;
    public int DataOffset;
    public int DataSelector;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
    public byte[] RegisterArea;
    public int Cr0NpxState;
  }
}
