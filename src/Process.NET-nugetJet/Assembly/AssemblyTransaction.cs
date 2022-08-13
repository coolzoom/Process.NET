// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.AssemblyTransaction
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using System;
using System.Text;

namespace Process.NET.Assembly
{
  public class AssemblyTransaction : IDisposable, IAssemblyTransaction
  {
    private readonly IAssemblyFactory _assemblyFactory;
    protected IntPtr ExitCode;
    protected StringBuilder Mnemonics;

    public AssemblyTransaction(IAssemblyFactory assemblyFactory, IntPtr address, bool autoExecute)
    {
      this._assemblyFactory = assemblyFactory;
      this.IsAutoExecuted = autoExecute;
      this.Address = address;
      this.Mnemonics = new StringBuilder();
    }

    public AssemblyTransaction(IAssemblyFactory assemblyFactory, bool autoExecute)
      : this(assemblyFactory, IntPtr.Zero, autoExecute)
    {
    }

    public IntPtr Address { get; }

    public bool IsAutoExecuted { get; set; }

    public void AddLine(string asm, params object[] args) => this.Mnemonics.AppendLine(string.Format(asm, args));

    public byte[] Assemble() => this._assemblyFactory.Assembler.Assemble(this.Mnemonics.ToString());

    public void Clear() => this.Mnemonics.Clear();

    public T GetExitCode<T>() => MarshalType<T>.PtrToObject(this._assemblyFactory.Process, this.ExitCode);

    public void InsertLine(int index, string asm, params object[] args) => this.Mnemonics.Insert(index, string.Format(asm, args));

    public virtual void Dispose()
    {
      if (this.Address != IntPtr.Zero)
      {
        if (this.IsAutoExecuted)
          this.ExitCode = this._assemblyFactory.InjectAndExecute<IntPtr>(this.Mnemonics.ToString(), this.Address);
        else
          this._assemblyFactory.Inject(this.Mnemonics.ToString(), this.Address);
      }
      if (!(this.Address == IntPtr.Zero) || !this.IsAutoExecuted)
        return;
      this.ExitCode = this._assemblyFactory.InjectAndExecute<IntPtr>(this.Mnemonics.ToString());
    }
  }
}
