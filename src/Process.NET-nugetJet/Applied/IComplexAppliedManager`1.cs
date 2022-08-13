// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.IComplexAppliedManager`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Applied
{
  public interface IComplexAppliedManager<T> : IAppliedManager<T> where T : IApplied
  {
    void Disable(T item, bool dueToRules);

    void Disable(string name, bool dueToRules);

    void Enable(T item, bool dueToRules);

    void Enable(string name, bool dueToRules);

    void DisableAll(bool dueToRules);

    void EnableAll(bool dueToRules);
  }
}
