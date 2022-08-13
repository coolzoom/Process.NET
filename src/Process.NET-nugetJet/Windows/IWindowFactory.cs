// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.IWindowFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;

namespace Process.NET.Windows
{
  public interface IWindowFactory : IDisposable
  {
    IEnumerable<IWindow> this[string windowTitle] { get; }

    IEnumerable<IWindow> ChildWindows { get; }

    IWindow MainWindow { get; set; }

    IEnumerable<IWindow> Windows { get; }

    IEnumerable<IWindow> GetWindowsByClassName(string className);

    IEnumerable<IWindow> GetWindowsByTitle(string windowTitle);

    IEnumerable<IWindow> GetWindowsByTitleContains(string windowTitle);
  }
}
