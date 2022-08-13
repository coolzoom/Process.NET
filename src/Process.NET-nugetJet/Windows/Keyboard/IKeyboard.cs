// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Keyboard.IKeyboard
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;

namespace Process.NET.Windows.Keyboard
{
  public interface IKeyboard
  {
    void Press(Keys key, TimeSpan interval);

    void PressRelease(Keys key);

    void Write(string text, params object[] args);

    void Press(Keys key);

    void Write(char character);
  }
}
