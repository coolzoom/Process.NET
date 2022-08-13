// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.WndProcEventArgs
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Windows
{
  public class WndProcEventArgs : EventArgs
  {
    public WndProcEventArgs(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
    {
      this.Hwnd = hwnd;
      this.Msg = msg;
      this.WParam = wParam;
      this.LParam = lParam;
    }

    public IntPtr Hwnd { get; }

    public int Msg { get; }

    public IntPtr WParam { get; }

    public IntPtr LParam { get; }
  }
}
