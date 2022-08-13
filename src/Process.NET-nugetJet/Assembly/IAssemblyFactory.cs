// Decompiled with JetBrains decompiler
// Type: Process.NET.Assembly.IAssemblyFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Assembly.Assemblers;
using Process.NET.Memory;
using Process.NET.Native.Types;
using System;
using System.Threading.Tasks;

namespace Process.NET.Assembly
{
  public interface IAssemblyFactory : IDisposable
  {
    IAssembler Assembler { get; set; }

    IProcess Process { get; }

    AssemblyTransaction BeginTransaction(bool autoExecute = true);

    AssemblyTransaction BeginTransaction(IntPtr address, bool autoExecute = true);

    IntPtr Execute(IntPtr address);

    IntPtr Execute(IntPtr address, object parameter);

    IntPtr Execute(
      IntPtr address,
      CallingConventions callingConvention,
      params object[] parameters);

    T Execute<T>(IntPtr address);

    T Execute<T>(IntPtr address, object parameter);

    T Execute<T>(IntPtr address, CallingConventions callingConvention, params object[] parameters);

    Task<IntPtr> ExecuteAsync(IntPtr address);

    Task<IntPtr> ExecuteAsync(IntPtr address, object parameter);

    Task<IntPtr> ExecuteAsync(
      IntPtr address,
      CallingConventions callingConvention,
      params object[] parameters);

    Task<T> ExecuteAsync<T>(IntPtr address);

    Task<T> ExecuteAsync<T>(IntPtr address, object parameter);

    Task<T> ExecuteAsync<T>(
      IntPtr address,
      CallingConventions callingConvention,
      params object[] parameters);

    IAllocatedMemory Inject(string[] asm);

    IAllocatedMemory Inject(string asm);

    void Inject(string[] asm, IntPtr address);

    void Inject(string asm, IntPtr address);

    IntPtr InjectAndExecute(string[] asm);

    IntPtr InjectAndExecute(string asm);

    IntPtr InjectAndExecute(string[] asm, IntPtr address);

    IntPtr InjectAndExecute(string asm, IntPtr address);

    T InjectAndExecute<T>(string[] asm);

    T InjectAndExecute<T>(string asm);

    T InjectAndExecute<T>(string[] asm, IntPtr address);

    T InjectAndExecute<T>(string asm, IntPtr address);

    Task<IntPtr> InjectAndExecuteAsync(string[] asm);

    Task<IntPtr> InjectAndExecuteAsync(string asm);

    Task<IntPtr> InjectAndExecuteAsync(string[] asm, IntPtr address);

    Task<IntPtr> InjectAndExecuteAsync(string asm, IntPtr address);

    Task<T> InjectAndExecuteAsync<T>(string[] asm);

    Task<T> InjectAndExecuteAsync<T>(string asm);

    Task<T> InjectAndExecuteAsync<T>(string[] asm, IntPtr address);

    Task<T> InjectAndExecuteAsync<T>(string asm, IntPtr address);
  }
}
