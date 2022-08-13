// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.IAppliedManager`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System.Collections.Generic;

namespace Process.NET.Applied
{
  public interface IAppliedManager<T> where T : IApplied
  {
    T this[string key] { get; }

    IReadOnlyDictionary<string, T> Items { get; }

    void Disable(T item);

    void Disable(string name);

    void DisableAll();

    void EnableAll();

    void Remove(T item);

    void Remove(string name);

    void RemoveAll();

    void Add(T applicable);

    void Add(IEnumerable<T> applicableRange);
  }
}
