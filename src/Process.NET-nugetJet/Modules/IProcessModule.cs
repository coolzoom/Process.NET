// Decompiled with JetBrains decompiler
// Type: Process.NET.Modules.IProcessModule
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using System.Diagnostics;

namespace Process.NET.Modules
{
  public interface IProcessModule : IPointer
  {
    IProcessFunction this[string functionName] { get; }

    bool IsMainModule { get; }

    string Name { get; }

    ProcessModule Native { get; }

    string Path { get; }

    int Size { get; }

    void Eject();

    IProcessFunction FindFunction(string functionName);
  }
}
