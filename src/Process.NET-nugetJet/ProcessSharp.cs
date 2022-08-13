// Decompiled with JetBrains decompiler
// Type: Process.NET.ProcessSharp
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using Process.NET.Modules;
using Process.NET.Native.Types;
using Process.NET.Threads;
using Process.NET.Utilities;
using Process.NET.Windows;
using System;
using System.Diagnostics;

namespace Process.NET
{
  public class ProcessSharp : IProcess, IDisposable
  {
    public ProcessSharp(System.Diagnostics.Process native, MemoryType type)
    {
      native.EnableRaisingEvents = true;
      native.Exited += (EventHandler) ((s, e) =>
      {
        EventHandler processExited = this.ProcessExited;
        if (processExited != null)
          processExited(s, e);
        this.HandleProcessExiting();
      });
      this.Native = native;
      this.Handle = MemoryHelper.OpenProcess(ProcessAccessFlags.AllAccess, this.Native.Id);
      if (type != MemoryType.Local)
      {
        if (type != MemoryType.Remote)
          throw new ArgumentOutOfRangeException(nameof (type), (object) type, (string) null);
        this.Memory = (IMemory) new ExternalProcessMemory(this.Handle);
      }
      else
        this.Memory = (IMemory) new LocalProcessMemory(this.Handle);
      native.ErrorDataReceived += new DataReceivedEventHandler(ProcessSharp.OutputDataReceived);
      native.OutputDataReceived += new DataReceivedEventHandler(ProcessSharp.OutputDataReceived);
      this.ThreadFactory = (IThreadFactory) new Process.NET.Threads.ThreadFactory((IProcess) this);
      this.ModuleFactory = (IModuleFactory) new Process.NET.Modules.ModuleFactory((IProcess) this);
      this.MemoryFactory = (IMemoryFactory) new Process.NET.Memory.MemoryFactory((IProcess) this);
      this.WindowFactory = (IWindowFactory) new Process.NET.Windows.WindowFactory((IProcess) this);
    }

    public ProcessSharp(string processName, MemoryType type)
      : this(ProcessHelper.FromName(processName), type)
    {
    }

    public ProcessSharp(int processId, MemoryType type)
      : this(ProcessHelper.FromProcessId(processId), type)
    {
    }

    public event EventHandler OnDispose;

    public IMemory Memory { get; set; }

    public System.Diagnostics.Process Native { get; set; }

    public SafeMemoryHandle Handle { get; set; }

    public IThreadFactory ThreadFactory { get; set; }

    public IModuleFactory ModuleFactory { get; set; }

    public IMemoryFactory MemoryFactory { get; set; }

    public IWindowFactory WindowFactory { get; set; }

    public IProcessModule this[string moduleName] => this.ModuleFactory[moduleName];

    public IPointer this[IntPtr intPtr] => (IPointer) new MemoryPointer((IProcess) this, intPtr);

    protected bool IsDisposed { get; set; }

    protected bool MustBeDisposed { get; set; } = true;

    public virtual void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      EventHandler onDispose = this.OnDispose;
      if (onDispose != null)
        onDispose((object) this, EventArgs.Empty);
      this.ThreadFactory?.Dispose();
      this.ModuleFactory?.Dispose();
      this.MemoryFactory?.Dispose();
      this.WindowFactory?.Dispose();
      this.Handle?.Close();
      GC.SuppressFinalize((object) this);
    }

    protected virtual void HandleProcessExiting()
    {
    }

    public event EventHandler ProcessExited;

    private static void OutputDataReceived(object sender, DataReceivedEventArgs e) => Trace.WriteLine(e.Data);

    ~ProcessSharp()
    {
      if (!this.MustBeDisposed)
        return;
      this.Dispose();
    }
  }
}
