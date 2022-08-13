// Decompiled with JetBrains decompiler
// Type: Process.NET.Utilities.WindowHelper
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native;
using Process.NET.Native.Types;
using Process.NET.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Process.NET.Utilities
{
  public static class WindowHelper
  {
    public static string GetClassName(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      StringBuilder lpClassName = new StringBuilder((int) ushort.MaxValue);
      if (User32.GetClassName(windowHandle, lpClassName, lpClassName.Capacity) == 0)
        throw new Win32Exception("Couldn't get the class name of the window or the window has no class name.");
      return lpClassName.ToString();
    }

    public static IntPtr GetForegroundWindow() => User32.GetForegroundWindow();

    public static int GetSystemMetrics(SystemMetrics metric)
    {
      int systemMetrics = User32.GetSystemMetrics(metric);
      return systemMetrics != 0 ? systemMetrics : throw new Win32Exception("The call of GetSystemMetrics failed. Unfortunately, GetLastError code doesn't provide more information.");
    }

    public static string GetWindowText(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      int windowTextLength = User32.GetWindowTextLength(windowHandle);
      if (windowTextLength == 0)
        return string.Empty;
      StringBuilder lpString = new StringBuilder(windowTextLength + 1);
      if (User32.GetWindowText(windowHandle, lpString, lpString.Capacity) == 0)
        throw new Win32Exception("Couldn't get the text of the window's title bar or the window has no title.");
      return lpString.ToString();
    }

    public static WindowPlacement GetWindowPlacement(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      WindowPlacement lpwndpl;
      lpwndpl.Length = Marshal.SizeOf(typeof (WindowPlacement));
      if (!User32.GetWindowPlacement(windowHandle, out lpwndpl))
        throw new Win32Exception("Couldn't get the window placement.");
      return lpwndpl;
    }

    public static int GetWindowProcessId(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      int lpdwProcessId;
      User32.GetWindowThreadProcessId(windowHandle, out lpdwProcessId);
      return lpdwProcessId;
    }

    public static int GetWindowThreadId(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      return User32.GetWindowThreadProcessId(windowHandle, out int _);
    }

    public static IEnumerable<IntPtr> EnumAllWindows()
    {
      List<IntPtr> numList = new List<IntPtr>();
      foreach (IntPtr enumTopLevelWindow in WindowHelper.EnumTopLevelWindows())
      {
        numList.Add(enumTopLevelWindow);
        numList.AddRange(WindowHelper.EnumChildWindows(enumTopLevelWindow));
      }
      return (IEnumerable<IntPtr>) numList;
    }

    public static IEnumerable<IntPtr> EnumChildWindows(IntPtr parentHandle)
    {
      List<IntPtr> list = new List<IntPtr>();
      EnumWindowsProc lpEnumFunc = (EnumWindowsProc) ((windowHandle, lParam) =>
      {
        list.Add(windowHandle);
        return true;
      });
      User32.EnumChildWindows(parentHandle, lpEnumFunc, IntPtr.Zero);
      return (IEnumerable<IntPtr>) list.ToArray();
    }

    public static IEnumerable<IntPtr> EnumTopLevelWindows() => WindowHelper.EnumChildWindows(IntPtr.Zero);

    public static bool FlashWindow(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      return User32.FlashWindow(windowHandle, true);
    }

    public static void FlashWindowEx(
      IntPtr windowHandle,
      FlashWindowFlags flags,
      int count,
      TimeSpan timeout)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      FlashInfo pwfi = new FlashInfo()
      {
        Size = Marshal.SizeOf(typeof (FlashInfo)),
        Hwnd = windowHandle,
        Flags = flags,
        Count = count,
        Timeout = Convert.ToInt32(timeout.TotalMilliseconds)
      };
      User32.FlashWindowEx(ref pwfi);
    }

    public static void FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags, int count) => WindowHelper.FlashWindowEx(windowHandle, flags, count, TimeSpan.FromMilliseconds(0.0));

    public static void FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags) => WindowHelper.FlashWindowEx(windowHandle, flags, 0);

    public static int MapVirtualKey(int key, TranslationTypes translation) => User32.MapVirtualKey(key, translation);

    public static int MapVirtualKey(Keys key, TranslationTypes translation) => WindowHelper.MapVirtualKey((int) key, translation);

    public static void PostMessage(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      if (!User32.PostMessage(windowHandle, message, wParam, lParam))
        throw new Win32Exception(string.Format("Couldn't post the message '{0}'.", (object) message));
    }

    public static void PostMessage(
      IntPtr windowHandle,
      WindowsMessages message,
      IntPtr wParam,
      IntPtr lParam)
    {
      WindowHelper.PostMessage(windowHandle, (int) message, wParam, lParam);
    }

    public static void SendInput(Input[] inputs)
    {
      if (inputs == null || inputs.Length == 0)
        throw new ArgumentException("The parameter cannot be null or empty.", nameof (inputs));
      if (User32.SendInput(inputs.Length, inputs, MarshalType<Input>.Size) == 0)
        throw new Win32Exception("Couldn't send the inputs.");
    }

    public static void SendInput(Input input) => WindowHelper.SendInput(new Input[1]
    {
      input
    });

    public static IntPtr SendMessage(
      IntPtr windowHandle,
      int message,
      IntPtr wParam,
      IntPtr lParam)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      return User32.SendMessage(windowHandle, message, wParam, lParam);
    }

    public static IntPtr SendMessage(
      IntPtr windowHandle,
      WindowsMessages message,
      IntPtr wParam,
      IntPtr lParam)
    {
      return WindowHelper.SendMessage(windowHandle, (int) message, wParam, lParam);
    }

    public static IntPtr SendMessage(Message message) => WindowHelper.SendMessage(message.HWnd, message.Msg, message.WParam, message.LParam);

    public static void SetForegroundWindow(IntPtr windowHandle)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      if (WindowHelper.GetForegroundWindow() == windowHandle)
        return;
      WindowHelper.ShowWindow(windowHandle, WindowStates.Restore);
      if (!User32.SetForegroundWindow(windowHandle))
        throw new ApplicationException("Couldn't set the window to foreground.");
    }

    public static void SetWindowPlacement(
      IntPtr windowHandle,
      int left,
      int top,
      int height,
      int width)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      WindowPlacement windowPlacement = WindowHelper.GetWindowPlacement(windowHandle);
      windowPlacement.NormalPosition.Left = left;
      windowPlacement.NormalPosition.Top = top;
      windowPlacement.NormalPosition.Height = height;
      windowPlacement.NormalPosition.Width = width;
      WindowHelper.SetWindowPlacement(windowHandle, windowPlacement);
    }

    public static void SetWindowPlacement(IntPtr windowHandle, WindowPlacement placement)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      if (Debugger.IsAttached && placement.ShowCmd == WindowStates.ShowNormal)
        placement.ShowCmd = WindowStates.Restore;
      if (!User32.SetWindowPlacement(windowHandle, ref placement))
        throw new Win32Exception("Couldn't set the window placement.");
    }

    public static void SetWindowText(IntPtr windowHandle, string title)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      if (!User32.SetWindowText(windowHandle, title))
        throw new Win32Exception("Couldn't set the text of the window's title bar.");
    }

    public static bool ShowWindow(IntPtr windowHandle, WindowStates state)
    {
      HandleManipulator.ValidateAsArgument(windowHandle, nameof (windowHandle));
      return User32.ShowWindow(windowHandle, state);
    }

    public static IntPtr GetMainWindowHandle(string processName) => (((IEnumerable<System.Diagnostics.Process>) System.Diagnostics.Process.GetProcessesByName(processName)).FirstOrDefault<System.Diagnostics.Process>() ?? throw new ArgumentNullException("process")).MainWindowHandle;
  }
}
