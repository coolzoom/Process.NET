// Decompiled with JetBrains decompiler
// Type: Process.NET.Extensions.UnsafeMemoryExtensions
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Marshaling;
using Process.NET.Native;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Process.NET.Extensions
{
  public static class UnsafeMemoryExtensions
  {
    public static IntPtr GetVtableIntPtr(this IntPtr intPtr, int functionIndex) => (intPtr.Read<IntPtr>() + functionIndex * IntPtr.Size).Read<IntPtr>();

    public static IntPtr ToFuncPtr(this Delegate d) => Marshal.GetFunctionPointerForDelegate(d);

    public static T ToDelegate<T>(this IntPtr addr) where T : class
    {
      if (typeof (T).GetCustomAttributes(typeof (UnmanagedFunctionPointerAttribute), true).Length == 0)
        throw new InvalidOperationException("This operation can only convert to delegates adorned with the UnmanagedFunctionPointerAttribute");
      return Marshal.GetDelegateForFunctionPointer(addr, typeof (T)) as T;
    }

    public static unsafe T Read<T>(this IntPtr address)
    {
      try
      {
        if (address == IntPtr.Zero)
          throw new InvalidOperationException("Cannot retrieve a value at address 0");
        object obj1;
        switch (MarshalCache<T>.TypeCode)
        {
          case TypeCode.Object:
            if (MarshalCache<T>.RealType == typeof (IntPtr))
              return (T) (ValueType) *(IntPtr*) (void*) address;
            if (!MarshalCache<T>.TypeRequiresMarshal)
            {
              T obj2 = default (T);
              Kernel32.MoveMemory(MarshalCache<T>.GetUnsafePtr(ref obj2), (void*) address, MarshalCache<T>.Size);
              return obj2;
            }
            obj1 = Marshal.PtrToStructure(address, typeof (T));
            break;
          case TypeCode.Boolean:
            obj1 = (object) (*(byte*) (void*) address > (byte) 0);
            break;
          case TypeCode.Char:
            obj1 = (object) (char) *(ushort*) (void*) address;
            break;
          case TypeCode.SByte:
            obj1 = (object) *(sbyte*) (void*) address;
            break;
          case TypeCode.Byte:
            obj1 = (object) *(byte*) (void*) address;
            break;
          case TypeCode.Int16:
            obj1 = (object) *(short*) (void*) address;
            break;
          case TypeCode.UInt16:
            obj1 = (object) *(ushort*) (void*) address;
            break;
          case TypeCode.Int32:
            obj1 = (object) *(int*) (void*) address;
            break;
          case TypeCode.UInt32:
            obj1 = (object) *(uint*) (void*) address;
            break;
          case TypeCode.Int64:
            obj1 = (object) *(long*) (void*) address;
            break;
          case TypeCode.UInt64:
            obj1 = (object) (ulong) *(long*) (void*) address;
            break;
          case TypeCode.Single:
            obj1 = (object) *(float*) (void*) address;
            break;
          case TypeCode.Double:
            obj1 = (object) *(double*) (void*) address;
            break;
          case TypeCode.Decimal:
            obj1 = (object) *(Decimal*) (void*) address;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        return (T) obj1;
      }
      catch (AccessViolationException ex)
      {
        Trace.WriteLine("Access Violation on " + address.ToString() + " with type " + typeof (T).Name + Environment.NewLine + (object) ex);
        return default (T);
      }
    }
  }
}
