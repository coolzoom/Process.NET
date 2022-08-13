// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.AssemblyFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Microsoft.CSharp.RuntimeBinder;
using Process.NET.Assembly.Assemblers;
using Process.NET.Assembly.CallingConventions;
using Process.NET.Marshaling;
using Process.NET.Memory;
using Process.NET.Native.Types;
using Process.NET.Threads;
using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Process.NET.Assembly
{
  public class AssemblyFactory : IAssemblyFactory, IDisposable
  {
    public AssemblyFactory(IProcess process, IAssembler assembler)
    {
      this.Process = process;
      this.Assembler = assembler;
    }

    public IAssembler Assembler { get; set; }

    public IProcess Process { get; }

    public AssemblyTransaction BeginTransaction(IntPtr address, bool autoExecute = true) => new AssemblyTransaction((IAssemblyFactory) this, address, autoExecute);

    public AssemblyTransaction BeginTransaction(bool autoExecute = true) => new AssemblyTransaction((IAssemblyFactory) this, autoExecute);

    public void Dispose()
    {
    }

    public T Execute<T>(IntPtr address) => this.Process.ThreadFactory.CreateAndJoin(address).GetExitCode<T>();

    public IntPtr Execute(IntPtr address) => this.Execute<IntPtr>(address);

    public T Execute<T>(IntPtr address, object parameter)
    {
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IRemoteThread>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IRemoteThread), typeof (AssemblyFactory)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IRemoteThread> target = AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IRemoteThread>> p1 = AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__0 = CallSite<Func<CallSite, IThreadFactory, IntPtr, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "CreateAndJoin", (IEnumerable<Type>) null, typeof (AssemblyFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__0.Target((CallSite) AssemblyFactory.\u003C\u003Eo__13<T>.\u003C\u003Ep__0, this.Process.ThreadFactory, address, parameter);
      return target((CallSite) p1, obj).GetExitCode<T>();
    }

    public IntPtr Execute(IntPtr address, object parameter)
    {
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IntPtr>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IntPtr), typeof (AssemblyFactory)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IntPtr> target = AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IntPtr>> p1 = AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, AssemblyFactory, IntPtr, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, nameof (Execute), (IEnumerable<Type>) new Type[1]
        {
          typeof (IntPtr)
        }, typeof (AssemblyFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) AssemblyFactory.\u003C\u003Eo__14.\u003C\u003Ep__0, this, address, parameter);
      return target((CallSite) p1, obj);
    }

    public T Execute<T>(
      IntPtr address,
      Process.NET.Native.Types.CallingConventions callingConvention,
      params object[] parameters)
    {
      IMarshalledValue[] array = ((IEnumerable<object>) parameters).Select<object, object>((Func<object, object>) (p =>
      {
        // ISSUE: reference to a compiler-generated field
        if (AssemblyFactory.\u003C\u003Eo__15<T>.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AssemblyFactory.\u003C\u003Eo__15<T>.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, IProcess, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Marshal", (IEnumerable<Type>) null, typeof (AssemblyFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        return AssemblyFactory.\u003C\u003Eo__15<T>.\u003C\u003Ep__0.Target((CallSite) AssemblyFactory.\u003C\u003Eo__15<T>.\u003C\u003Ep__0, typeof (MarshalValue), this.Process, p);
      })).Cast<IMarshalledValue>().ToArray<IMarshalledValue>();
      AssemblyTransaction assemblyTransaction;
      using (assemblyTransaction = this.BeginTransaction(true))
      {
        ICallingConvention callingConvention1 = CallingConventionSelector.Get(callingConvention);
        assemblyTransaction.AddLine(callingConvention1.FormatParameters(((IEnumerable<IMarshalledValue>) array).Select<IMarshalledValue, IntPtr>((Func<IMarshalledValue, IntPtr>) (p => p.Reference)).ToArray<IntPtr>()), Array.Empty<object>());
        assemblyTransaction.AddLine(callingConvention1.FormatCalling(address), Array.Empty<object>());
        if (callingConvention1.Cleanup == CleanupTypes.Caller)
          assemblyTransaction.AddLine(callingConvention1.FormatCleaning(array.Length), Array.Empty<object>());
        assemblyTransaction.AddLine("retn", Array.Empty<object>());
      }
      foreach (IDisposable disposable in array)
        disposable.Dispose();
      return assemblyTransaction.GetExitCode<T>();
    }

    public IntPtr Execute(
      IntPtr address,
      Process.NET.Native.Types.CallingConventions callingConvention,
      params object[] parameters)
    {
      return this.Execute<IntPtr>(address, callingConvention, parameters);
    }

    public Task<T> ExecuteAsync<T>(IntPtr address) => Task.Run<T>((Func<T>) (() => this.Execute<T>(address)));

    public Task<IntPtr> ExecuteAsync(IntPtr address) => this.ExecuteAsync<IntPtr>(address);

    public Task<T> ExecuteAsync<T>(IntPtr address, object parameter) => Task.Run<T>((Func<Task<T>>) (() =>
    {
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Task<T>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Task<T>), typeof (AssemblyFactory)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, Task<T>> target = AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, Task<T>>> p1 = AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__0 = CallSite<Func<CallSite, AssemblyFactory, IntPtr, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Execute", (IEnumerable<Type>) new Type[1]
        {
          typeof (T)
        }, typeof (AssemblyFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__0.Target((CallSite) AssemblyFactory.\u003C\u003Eo__19<T>.\u003C\u003Ep__0, this, address, parameter);
      return target((CallSite) p1, obj);
    }));

    public Task<IntPtr> ExecuteAsync(IntPtr address, object parameter)
    {
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Task<IntPtr>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Task<IntPtr>), typeof (AssemblyFactory)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, Task<IntPtr>> target = AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, Task<IntPtr>>> p1 = AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, AssemblyFactory, IntPtr, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, nameof (ExecuteAsync), (IEnumerable<Type>) new Type[1]
        {
          typeof (IntPtr)
        }, typeof (AssemblyFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) AssemblyFactory.\u003C\u003Eo__20.\u003C\u003Ep__0, this, address, parameter);
      return target((CallSite) p1, obj);
    }

    public Task<T> ExecuteAsync<T>(
      IntPtr address,
      Process.NET.Native.Types.CallingConventions callingConvention,
      params object[] parameters)
    {
      return Task.Run<T>((Func<T>) (() => this.Execute<T>(address, callingConvention, parameters)));
    }

    public Task<IntPtr> ExecuteAsync(
      IntPtr address,
      Process.NET.Native.Types.CallingConventions callingConvention,
      params object[] parameters)
    {
      return this.ExecuteAsync<IntPtr>(address, callingConvention, parameters);
    }

    public void Inject(string asm, IntPtr address) => this.Process.Memory.Write(address, this.Assembler.Assemble(asm, address));

    public void Inject(string[] asm, IntPtr address) => this.Inject(string.Join("\n", asm), address);

    public IAllocatedMemory Inject(string asm)
    {
      byte[] numArray = this.Assembler.Assemble(asm);
      IAllocatedMemory allocatedMemory = this.Process.MemoryFactory.Allocate(Randomizer.GenerateString(), numArray.Length);
      this.Inject(asm, allocatedMemory.BaseAddress);
      return allocatedMemory;
    }

    public IAllocatedMemory Inject(string[] asm) => this.Inject(string.Join("\n", asm));

    public T InjectAndExecute<T>(string asm, IntPtr address)
    {
      this.Inject(asm, address);
      return this.Execute<T>(address);
    }

    public IntPtr InjectAndExecute(string asm, IntPtr address) => this.InjectAndExecute<IntPtr>(asm, address);

    public T InjectAndExecute<T>(string[] asm, IntPtr address) => this.InjectAndExecute<T>(string.Join("\n", asm), address);

    public IntPtr InjectAndExecute(string[] asm, IntPtr address) => this.InjectAndExecute<IntPtr>(asm, address);

    public T InjectAndExecute<T>(string asm)
    {
      using (IAllocatedMemory allocatedMemory = this.Inject(asm))
        return this.Execute<T>(allocatedMemory.BaseAddress);
    }

    public IntPtr InjectAndExecute(string asm) => this.InjectAndExecute<IntPtr>(asm);

    public T InjectAndExecute<T>(string[] asm) => this.InjectAndExecute<T>(string.Join("\n", asm));

    public IntPtr InjectAndExecute(string[] asm) => this.InjectAndExecute<IntPtr>(asm);

    public Task<T> InjectAndExecuteAsync<T>(string asm, IntPtr address) => Task.Run<T>((Func<T>) (() => this.InjectAndExecute<T>(asm, address)));

    public Task<IntPtr> InjectAndExecuteAsync(string asm, IntPtr address) => this.InjectAndExecuteAsync<IntPtr>(asm, address);

    public Task<T> InjectAndExecuteAsync<T>(string[] asm, IntPtr address) => Task.Run<T>((Func<T>) (() => this.InjectAndExecute<T>(asm, address)));

    public Task<IntPtr> InjectAndExecuteAsync(string[] asm, IntPtr address) => this.InjectAndExecuteAsync<IntPtr>(asm, address);

    public Task<T> InjectAndExecuteAsync<T>(string asm) => Task.Run<T>((Func<T>) (() => this.InjectAndExecute<T>(asm)));

    public Task<IntPtr> InjectAndExecuteAsync(string asm) => this.InjectAndExecuteAsync<IntPtr>(asm);

    public Task<T> InjectAndExecuteAsync<T>(string[] asm) => Task.Run<T>((Func<T>) (() => this.InjectAndExecute<T>(asm)));

    public Task<IntPtr> InjectAndExecuteAsync(string[] asm) => this.InjectAndExecuteAsync<IntPtr>(asm);
  }
}
