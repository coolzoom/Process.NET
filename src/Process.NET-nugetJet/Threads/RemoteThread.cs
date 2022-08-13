// Decompiled with JetBrains decompiler
// Type: Process.NET.Threads.RemoteThread
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native.Types;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Process.NET.Threads
{
  public class RemoteThread : IEquatable<RemoteThread>, IRemoteThread, IDisposable
  {
    private readonly IMarshalledValue _parameter;
    private readonly Task _parameterCleaner;
    protected readonly IProcess ProcessPlus;

    public RemoteThread(IProcess processPlus, ProcessThread thread)
    {
      this.ProcessPlus = processPlus;
      this.Native = thread;
      this.Id = thread.Id;
      this.Handle = Process.NET.Utilities.ThreadHelper.OpenThread(ThreadAccessFlags.AllAccess, this.Id);
    }

    public RemoteThread(IProcess processPlus, ProcessThread thread, IMarshalledValue parameter = null)
      : this(processPlus, thread)
    {
      this._parameter = parameter;
      this._parameterCleaner = new Task((Action) (() =>
      {
        this.Join();
        this._parameter.Dispose();
      }));
    }

    public bool Equals(RemoteThread other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return this.Id == other.Id && this.ProcessPlus.Equals((object) other.ProcessPlus);
    }

    public ThreadContext Context
    {
      get
      {
        if (!this.IsAlive)
          throw new ThreadStateException(string.Format("Couldn't set the context of the thread #{0} because it is terminated.", (object) this.Id));
        bool isSuspended = this.IsSuspended;
        try
        {
          if (!isSuspended)
            this.Suspend();
          return Process.NET.Utilities.ThreadHelper.GetThreadContext(this.Handle, ThreadContextFlags.All);
        }
        finally
        {
          if (!isSuspended)
            this.Resume();
        }
      }
      set
      {
        if (!this.IsAlive)
          return;
        bool isSuspended = this.IsSuspended;
        try
        {
          if (!isSuspended)
            this.Suspend();
          Process.NET.Utilities.ThreadHelper.SetThreadContext(this.Handle, value);
        }
        finally
        {
          if (!isSuspended)
            this.Resume();
        }
      }
    }

    public SafeMemoryHandle Handle { get; }

    public int Id { get; }

    public bool IsAlive => !this.IsTerminated;

    public bool IsMainThread => this.ProcessPlus.Native.Threads[0].Id == this.Id;

    public bool IsSuspended
    {
      get
      {
        this.Refresh();
        return this.Native != null && this.Native.ThreadState == System.Diagnostics.ThreadState.Wait && this.Native.WaitReason == ThreadWaitReason.Suspended;
      }
    }

    public bool IsTerminated
    {
      get
      {
        this.Refresh();
        return this.Native == null;
      }
    }

    public ProcessThread Native { get; private set; }

    public virtual void Dispose()
    {
      this.Handle.Close();
      GC.SuppressFinalize((object) this);
    }

    public T GetExitCode<T>()
    {
      IntPtr? exitCodeThread = Process.NET.Utilities.ThreadHelper.GetExitCodeThread(this.Handle);
      return !exitCodeThread.HasValue ? default (T) : MarshalType<T>.PtrToObject(this.ProcessPlus, exitCodeThread.Value);
    }

    public override int GetHashCode() => this.Id.GetHashCode() ^ this.ProcessPlus.GetHashCode();

    public IntPtr GetRealSegmentAddress(SegmentRegisters segment)
    {
      LdtEntry threadSelectorEntry;
      switch (segment)
      {
        case SegmentRegisters.Cs:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegCs);
          break;
        case SegmentRegisters.Ds:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegDs);
          break;
        case SegmentRegisters.Es:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegEs);
          break;
        case SegmentRegisters.Fs:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegFs);
          break;
        case SegmentRegisters.Gs:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegGs);
          break;
        case SegmentRegisters.Ss:
          threadSelectorEntry = Process.NET.Utilities.ThreadHelper.GetThreadSelectorEntry(this.Handle, this.Context.SegSs);
          break;
        default:
          throw new InvalidEnumArgumentException(nameof (segment));
      }
      return new IntPtr((int) threadSelectorEntry.BaseLow | (int) threadSelectorEntry.BaseMid << 16 | (int) threadSelectorEntry.BaseHi << 24);
    }

    public void Refresh()
    {
      if (this.Native == null)
        return;
      this.ProcessPlus.Native.Refresh();
      this.Native = this.ProcessPlus.Native.Threads.Cast<ProcessThread>().FirstOrDefault<ProcessThread>((Func<ProcessThread, bool>) (t => t.Id == this.Native.Id));
    }

    public void Join()
    {
      int num = (int) Process.NET.Utilities.ThreadHelper.WaitForSingleObject(this.Handle);
    }

    public WaitValues Join(TimeSpan time) => Process.NET.Utilities.ThreadHelper.WaitForSingleObject(this.Handle, new TimeSpan?(time));

    public void Resume()
    {
      if (!this.IsAlive)
        return;
      Process.NET.Utilities.ThreadHelper.ResumeThread(this.Handle);
      if (this._parameter == null || this._parameterCleaner.IsCompleted)
        return;
      this._parameterCleaner.Start();
    }

    public IFrozenThread Suspend()
    {
      if (!this.IsAlive)
        return (IFrozenThread) null;
      Process.NET.Utilities.ThreadHelper.SuspendThread(this.Handle);
      return (IFrozenThread) new FrozenThread((IRemoteThread) this);
    }

    public void Terminate(int exitCode = 0)
    {
      if (!this.IsAlive)
        return;
      Process.NET.Utilities.ThreadHelper.TerminateThread(this.Handle, exitCode);
    }

    ~RemoteThread() => this.Dispose();

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return obj.GetType() == this.GetType() && this.Equals((RemoteThread) obj);
    }

    public static bool operator ==(RemoteThread left, RemoteThread right) => object.Equals((object) left, (object) right);

    public static bool operator !=(RemoteThread left, RemoteThread right) => !object.Equals((object) left, (object) right);

    public override string ToString() => string.Format("Id = {0} IsAlive = {1} IsMainThread = {2}", (object) this.Id, (object) this.IsAlive, (object) this.IsMainThread);
  }
}
