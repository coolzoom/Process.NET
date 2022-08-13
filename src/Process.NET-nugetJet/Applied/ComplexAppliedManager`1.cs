// Decompiled with JetBrains decompiler
// Type: Process.NET.Applied.ComplexAppliedManager`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Applied
{
  public class ComplexAppliedManager<T> : 
    AppliedManager<T>,
    IComplexAppliedManager<T>,
    IAppliedManager<T>
    where T : IComplexApplied
  {
    public void Disable(T item, bool dueToRules) => this.Disable(item.Identifier, dueToRules);

    public void Disable(string name, bool dueToRules) => this.InternalItems[name].Disable(dueToRules);

    public void Enable(T item, bool dueToRules) => this.Enable(item.Identifier, dueToRules);

    public void Enable(string name, bool dueToRules) => this.InternalItems[name].Enable(dueToRules);

    public void DisableAll(bool dueToRules)
    {
      foreach (T obj in this.InternalItems.Values)
        obj.Disable(dueToRules);
    }

    public void EnableAll(bool dueToRules)
    {
      foreach (T obj in this.InternalItems.Values)
        obj.Enable(dueToRules);
    }
  }
}
