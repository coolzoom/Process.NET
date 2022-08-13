// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.IWindow
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Threads;
using Process.NET.Windows.Keyboard;
using Process.NET.Windows.Mouse;
using System;
using System.Collections.Generic;

namespace Process.NET.Windows
{
  public interface IWindow : IDisposable
  {
    IEnumerable<IWindow> Children { get; }

    string ClassName { get; }

    IntPtr Handle { get; }

    int Height { get; set; }

    bool IsActivated { get; }

    bool IsMainWindow { get; }

    IKeyboard Keyboard { get; set; }

    IMouse Mouse { get; set; }

    WindowPlacement Placement { get; set; }

    WindowStates State { get; set; }

    IRemoteThread Thread { get; }

    string Title { get; set; }

    int Width { get; set; }

    int X { get; set; }

    int Y { get; set; }

    void Activate();

    void Close();

    void Flash();

    void Flash(int count, TimeSpan timeout, FlashWindowFlags flags = FlashWindowFlags.All);

    void PostMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam);

    void PostMessage(int message, IntPtr wParam, IntPtr lParam);

    IntPtr SendMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam);

    IntPtr SendMessage(int message, IntPtr wParam, IntPtr lParam);
  }
}
