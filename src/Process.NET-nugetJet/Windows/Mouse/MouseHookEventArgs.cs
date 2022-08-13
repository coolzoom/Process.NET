// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Mouse.MouseHookEventArgs
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;

namespace Process.NET.Windows.Mouse
{
  public class MouseHookEventArgs : HookEventArgs
  {
    public MouseHookEventArgs(MSLLHOOKSTRUCT lparam)
    {
      this.EventType = HookEventType.Mouse;
      this.LParam = lparam;
    }

    private MSLLHOOKSTRUCT LParam { get; }

    public Point Position => this.LParam.Point;

    public MouseScrollDirection ScrollDirection
    {
      get
      {
        if (this.MouseEventName != MouseEventNames.MouseWheel)
          return MouseScrollDirection.None;
        return this.LParam.MouseData >> 16 <= 0 ? MouseScrollDirection.Down : MouseScrollDirection.Up;
      }
    }

    public MouseEventNames MouseEventName { get; internal set; }
  }
}
