// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.IAssemblyTransaction
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;

namespace Process.NET.Assembly
{
  public interface IAssemblyTransaction
  {
    IntPtr Address { get; }

    bool IsAutoExecuted { get; set; }

    void AddLine(string asm, params object[] args);

    byte[] Assemble();

    void Clear();

    void Dispose();

    T GetExitCode<T>();

    void InsertLine(int index, string asm, params object[] args);
  }
}
