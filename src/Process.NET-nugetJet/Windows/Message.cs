// Decompiled with JetBrains decompiler
// Type: Process.NET.Windows.Message
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Native.Types;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Process.NET.Windows
{
  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  public struct Message
  {
    private IntPtr hWnd;
    private int msg;
    private IntPtr wparam;
    private IntPtr lparam;
    private IntPtr result;
    private static readonly CodeAccessPermission UnmanagedCode = (CodeAccessPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);

    public IntPtr HWnd
    {
      get => this.hWnd;
      set => this.hWnd = value;
    }

    public int Msg
    {
      get => this.msg;
      set => this.msg = value;
    }

    public IntPtr WParam
    {
      get => this.wparam;
      set => this.wparam = value;
    }

    public IntPtr LParam
    {
      get => this.lparam;
      set => this.lparam = value;
    }

    public IntPtr Result
    {
      get => this.result;
      set => this.result = value;
    }

    public object GetLParam(Type cls) => Marshal.PtrToStructure(this.lparam, cls);

    public static Message Create(
      IntPtr hWnd,
      WindowsMessages msg,
      IntPtr wparam,
      IntPtr lparam)
    {
      return Message.Create(hWnd, (int) msg, wparam, lparam);
    }

    public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam) => new Message()
    {
      hWnd = hWnd,
      msg = msg,
      wparam = wparam,
      lparam = lparam,
      result = IntPtr.Zero
    };

    public override bool Equals(object o) => o is Message message && this.hWnd == message.hWnd && this.msg == message.msg && this.wparam == message.wparam && this.lparam == message.lparam && this.result == message.result;

    public static bool operator !=(Message a, Message b) => !a.Equals((object) b);

    public static bool operator ==(Message a, Message b) => a.Equals((object) b);

    public override int GetHashCode() => (int) this.hWnd << 4 | this.msg;

    public override string ToString()
    {
      bool flag = false;
      try
      {
        Message.UnmanagedCode.Demand();
        flag = true;
      }
      catch (SecurityException ex)
      {
      }
      return flag ? Message.GetProperName(((WindowsMessages) this.Msg).ToString()) : base.ToString();
    }

    private static string GetProperName(string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < name.Length; ++index)
      {
        char c = name[index];
        if (index > 0 && char.IsUpper(c))
        {
          stringBuilder.Append(' ');
          stringBuilder.Append(char.ToLowerInvariant(c));
        }
        else
          stringBuilder.Append(c);
      }
      return stringBuilder.ToString();
    }
  }
}
