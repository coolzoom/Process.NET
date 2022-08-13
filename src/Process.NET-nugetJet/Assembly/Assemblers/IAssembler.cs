// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.Assemblers.IAssembler
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Assembly.Assemblers
{
  public interface IAssembler
  {
    byte[] Assemble(string asm);

    byte[] Assemble(string asm, IntPtr baseAddress);
  }
}
