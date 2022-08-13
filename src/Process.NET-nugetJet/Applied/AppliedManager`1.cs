// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.AppliedManager`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;

namespace Process.NET.Applied
{
  public class AppliedManager<T> : IAppliedManager<T> where T : IApplied
  {
    protected readonly Dictionary<string, T> InternalItems = new Dictionary<string, T>();

    public IReadOnlyDictionary<string, T> Items => (IReadOnlyDictionary<string, T>) this.InternalItems;

    public void Disable(T item) => throw new NotImplementedException();

    public void Disable(string name) => throw new NotImplementedException();

    public T this[string key] => this.InternalItems[key];

    public void EnableAll()
    {
      foreach (KeyValuePair<string, T> internalItem in this.InternalItems)
      {
        T obj = internalItem.Value;
        if (!obj.IsEnabled)
        {
          obj = internalItem.Value;
          obj.Disable();
        }
      }
    }

    public void DisableAll()
    {
      foreach (KeyValuePair<string, T> internalItem in this.InternalItems)
      {
        T obj = internalItem.Value;
        if (obj.IsEnabled)
        {
          obj = internalItem.Value;
          obj.Disable();
        }
      }
    }

    public void Remove(string name)
    {
      if (!this.InternalItems.ContainsKey(name))
        return;
      try
      {
        this.InternalItems[name].Dispose();
      }
      finally
      {
        this.InternalItems.Remove(name);
      }
    }

    public void Remove(T item) => this.Remove(item.Identifier);

    public void RemoveAll()
    {
      foreach (KeyValuePair<string, T> internalItem in this.InternalItems)
        internalItem.Value.Dispose();
      this.InternalItems.Clear();
    }

    public void Add(T applicable) => this.InternalItems.Add(applicable.Identifier, applicable);

    public void Add(IEnumerable<T> applicableRange)
    {
      foreach (T applicable in applicableRange)
        this.Add(applicable);
    }
  }
}
