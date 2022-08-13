// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Keyboard.KeyboardHookEventArgs
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Process.NET.Windows.Keyboard
{
  public class KeyboardHookEventArgs
  {
    private const int KEY_PRESSED = 32768;

    public KeyboardHookEventArgs(KBDLLHOOKSTRUCT lParam)
    {
      this.Key = (Keys) lParam.vkCode;
      this.IsLAltPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_LALT) & 32768) || this.Key == Keys.LMenu;
      this.IsRAltPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_RALT) & 32768) || this.Key == Keys.RMenu;
      this.IsLCtrlPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_LCONTROL) & 32768) || this.Key == Keys.LControlKey;
      this.IsRCtrlPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_RCONTROL) & 32768) || this.Key == Keys.RControlKey;
      this.IsLShiftPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_LSHIFT) & 32768) || this.Key == Keys.LShiftKey;
      this.IsRShiftPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_RSHIFT) & 32768) || this.Key == Keys.RShiftKey;
      this.IsLWinPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_LWIN) & 32768) || this.Key == Keys.LWin;
      this.IsRWinPressed = Convert.ToBoolean((int) KeyboardHookEventArgs.GetKeyState(KeyboardHookEventArgs.VirtualKeyStates.VK_RWIN) & 32768) || this.Key == Keys.RWin;
      if (!((IEnumerable<Keys>) new Keys[8]
      {
        Keys.LMenu,
        Keys.RMenu,
        Keys.LControlKey,
        Keys.RControlKey,
        Keys.LShiftKey,
        Keys.RShiftKey,
        Keys.LWin,
        Keys.RWin
      }).Contains<Keys>(this.Key))
        return;
      this.Key = Keys.None;
    }

    public Keys Key { get; }

    public bool IsAltPressed => this.IsLAltPressed || this.IsRAltPressed;

    public bool IsLAltPressed { get; }

    public bool IsRAltPressed { get; }

    public bool IsCtrlPressed => this.IsLCtrlPressed || this.IsRCtrlPressed;

    public bool IsLCtrlPressed { get; }

    public bool IsRCtrlPressed { get; }

    public bool IsShiftPressed => this.IsLShiftPressed || this.IsRShiftPressed;

    public bool IsLShiftPressed { get; }

    public bool IsRShiftPressed { get; }

    public bool IsWinPressed => this.IsLWinPressed || this.IsRWinPressed;

    public bool IsLWinPressed { get; }

    public bool IsRWinPressed { get; }

    public override string ToString() => string.Format("Key={0}; Win={1}; Alt={2}; Ctrl={3}; Shift={4}", (object) this.Key, (object) this.IsWinPressed, (object) this.IsAltPressed, (object) this.IsCtrlPressed, (object) this.IsShiftPressed);

    [DllImport("user32.dll")]
    private static extern short GetKeyState(KeyboardHookEventArgs.VirtualKeyStates nVirtKey);

    private enum VirtualKeyStates
    {
      VK_LWIN = 91, // 0x0000005B
      VK_RWIN = 92, // 0x0000005C
      VK_LSHIFT = 160, // 0x000000A0
      VK_RSHIFT = 161, // 0x000000A1
      VK_LCONTROL = 162, // 0x000000A2
      VK_RCONTROL = 163, // 0x000000A3
      VK_LALT = 164, // 0x000000A4
      VK_RALT = 165, // 0x000000A5
    }
  }
}
