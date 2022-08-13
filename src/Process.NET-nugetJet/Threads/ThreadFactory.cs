// Decompiled with JetBrains decompiler
// Type: Process.NET.Threads.ThreadFactory
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Microsoft.CSharp.RuntimeBinder;
using Process.NET.Marshaling;
using Process.NET.Native.Types;
using Process.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Process.NET.Threads
{
  public class ThreadFactory : IThreadFactory, IDisposable
  {
    protected readonly IProcess Process;

    public ThreadFactory(IProcess process) => this.Process = process;

    public IRemoteThread MainThread => (IRemoteThread) new RemoteThread(this.Process, this.NativeThreads.Aggregate<ProcessThread>((Func<ProcessThread, ProcessThread, ProcessThread>) ((current, next) => !(next.StartTime < current.StartTime) ? current : next)));

    public IEnumerable<ProcessThread> NativeThreads
    {
      get
      {
        this.Process.Native.Refresh();
        return this.Process.Native.Threads.Cast<ProcessThread>();
      }
    }

    public IEnumerable<IRemoteThread> RemoteThreads => (IEnumerable<IRemoteThread>) this.NativeThreads.Select<ProcessThread, RemoteThread>((Func<ProcessThread, RemoteThread>) (t => new RemoteThread(this.Process, t)));

    public IRemoteThread this[int threadId] => (IRemoteThread) new RemoteThread(this.Process, this.NativeThreads.First<ProcessThread>((Func<ProcessThread, bool>) (t => t.Id == threadId)));

    public IRemoteThread Create(IntPtr address, object parameter, bool isStarted = true)
    {
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, IProcess, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Marshal", (IEnumerable<Type>) null, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__0, typeof (MarshalValue), this.Process, parameter);
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "NtQueryInformationThread", (IEnumerable<Type>) null, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target1 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p3 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__3;
      Type type1 = typeof (ThreadHelper);
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, SafeMemoryHandle, IntPtr, object, ThreadCreationFlags, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "CreateRemoteThread", (IEnumerable<Type>) null, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, SafeMemoryHandle, IntPtr, object, ThreadCreationFlags, object> target2 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, SafeMemoryHandle, IntPtr, object, ThreadCreationFlags, object>> p2 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__2;
      Type type2 = typeof (ThreadHelper);
      SafeMemoryHandle handle = this.Process.Handle;
      IntPtr num = address;
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Reference", typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__1.Target((CallSite) ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__1, obj1);
      object obj3 = target2((CallSite) p2, type2, handle, num, obj2, ThreadCreationFlags.Suspended);
      object ret = target1((CallSite) p3, type1, obj3);
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__7 = CallSite<Func<CallSite, Type, IProcess, ProcessThread, object, RemoteThread>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      RemoteThread remoteThread = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__7.Target((CallSite) ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__7, typeof (RemoteThread), this.Process, this.Process.ThreadFactory.NativeThreads.First<ProcessThread>((Func<ProcessThread, bool>) (t =>
      {
        // ISSUE: reference to a compiler-generated field
        if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (ThreadFactory)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p6 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__5 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target4 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p5 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__5;
        int id = t.Id;
        // ISSUE: reference to a compiler-generated field
        if (ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ThreadId", typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__4.Target((CallSite) ThreadFactory.\u003C\u003Eo__10.\u003C\u003Ep__4, ret);
        object obj5 = target4((CallSite) p5, id, obj4);
        return target3((CallSite) p6, obj5);
      })), obj1);
      if (isStarted)
        remoteThread.Resume();
      return (IRemoteThread) remoteThread;
    }

    public IRemoteThread Create(IntPtr address, bool isStarted = true)
    {
      ThreadBasicInformation ret = ThreadHelper.NtQueryInformationThread(ThreadHelper.CreateRemoteThread(this.Process.Handle, address, IntPtr.Zero, ThreadCreationFlags.Suspended));
      RemoteThread remoteThread = new RemoteThread(this.Process, this.Process.ThreadFactory.NativeThreads.First<ProcessThread>((Func<ProcessThread, bool>) (t => t.Id == ret.ThreadId)));
      if (isStarted)
        remoteThread.Resume();
      return (IRemoteThread) remoteThread;
    }

    public IRemoteThread CreateAndJoin(IntPtr address, object parameter)
    {
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, ThreadFactory, IntPtr, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Create", (IEnumerable<Type>) null, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__0, this, address, parameter);
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Join", (IEnumerable<Type>) null, typeof (ThreadFactory), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__1.Target((CallSite) ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__1, obj);
      // ISSUE: reference to a compiler-generated field
      if (ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, IRemoteThread>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IRemoteThread), typeof (ThreadFactory)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__2.Target((CallSite) ThreadFactory.\u003C\u003Eo__12.\u003C\u003Ep__2, obj);
    }

    public IRemoteThread CreateAndJoin(IntPtr address)
    {
      IRemoteThread remoteThread = this.Create(address, true);
      remoteThread.Join();
      return remoteThread;
    }

    public void Dispose()
    {
    }

    public IRemoteThread GetThreadById(int id) => (IRemoteThread) new RemoteThread(this.Process, this.NativeThreads.First<ProcessThread>((Func<ProcessThread, bool>) (t => t.Id == id)));

    public void ResumeAll()
    {
      foreach (IRemoteThread remoteThread in this.RemoteThreads)
        remoteThread.Resume();
    }

    public void SuspendAll()
    {
      foreach (IRemoteThread remoteThread in this.RemoteThreads)
        remoteThread.Suspend();
    }
  }
}
