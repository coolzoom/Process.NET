// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Keyboard.MessageKeyboard
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Process.NET.Windows.Keyboard
{
  public class MessageKeyboard : IKeyboard
  {
    protected static readonly List<Tuple<IntPtr, Keys>> PressedKeys = new List<Tuple<IntPtr, Keys>>();

    public MessageKeyboard(IWindow window) => this.Window = window;

    protected IWindow Window { get; set; }

    public void Press(Keys key, TimeSpan interval)
    {
      Tuple<IntPtr, Keys> tuple = Tuple.Create<IntPtr, Keys>(this.Window.Handle, key);
      if (MessageKeyboard.PressedKeys.Contains(tuple))
        return;
      MessageKeyboard.PressedKeys.Add(tuple);
      Task.Run((Func<Task>) (async () =>
      {
        while (MessageKeyboard.PressedKeys.Contains(tuple))
        {
          this.Press(key);
          await Task.Delay(interval);
        }
      }));
    }

    public void PressRelease(Keys key)
    {
      this.Press(key);
      Thread.Sleep(10);
      this.Release(key);
    }

    public void Write(string text, params object[] args)
    {
      foreach (char character in string.Format(text, args))
        this.Write(character);
    }

    public void Press(Keys key) => this.Window.PostMessage(WindowsMessages.KeyFirst, new IntPtr((int) key), MessageKeyboard.MakeKeyParameter(key, false));

    public void Write(char character) => this.Window.PostMessage(WindowsMessages.Char, new IntPtr((int) character), IntPtr.Zero);

    public virtual void Release(Keys key)
    {
      Tuple<IntPtr, Keys> tuple = Tuple.Create<IntPtr, Keys>(this.Window.Handle, key);
      if (MessageKeyboard.PressedKeys.Contains(tuple))
        MessageKeyboard.PressedKeys.Remove(tuple);
      this.Window.PostMessage(WindowsMessages.KeyUp, new IntPtr((int) key), MessageKeyboard.MakeKeyParameter(key, true));
    }

    private static IntPtr MakeKeyParameter(
      Keys key,
      bool keyUp,
      bool fRepeat,
      int cRepeat,
      bool altDown,
      bool fExtended)
    {
      uint num = (uint) cRepeat | (uint) (WindowHelper.MapVirtualKey(key, TranslationTypes.VirtualKeyToScanCode) << 16);
      if (fExtended)
        num |= 16777216U;
      if (altDown)
        num |= 536870912U;
      if (fRepeat)
        num |= 1073741824U;
      if (keyUp)
        num |= 2147483648U;
      return new IntPtr((long) num);
    }

    private static IntPtr MakeKeyParameter(Keys key, bool keyUp) => MessageKeyboard.MakeKeyParameter(key, keyUp, keyUp, 1, false, false);
  }
}
