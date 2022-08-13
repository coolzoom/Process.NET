// Decompiled with JetBrains decompiler
// Type: Process.NET.Marshaling.MarshalType`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using Process.NET.Memory;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Process.NET.Marshaling
{
  public static class MarshalType<T>
  {
    static MarshalType()
    {
      if (IntPtr.Size == 8)
        MarshalType<T>.CanBeStoredInRegisters = MarshalType<T>.Test();
      else
        MarshalType<T>.CanBeStoredInRegisters = MarshalType<T>.IsIntPtr || MarshalType<T>.TypeCode == TypeCode.Boolean || MarshalType<T>.TypeCode == TypeCode.Byte || MarshalType<T>.TypeCode == TypeCode.Char || MarshalType<T>.TypeCode == TypeCode.Int16 || MarshalType<T>.TypeCode == TypeCode.Int32 || MarshalType<T>.TypeCode == TypeCode.Int64 || MarshalType<T>.TypeCode == TypeCode.SByte || MarshalType<T>.TypeCode == TypeCode.Single || MarshalType<T>.TypeCode == TypeCode.UInt16 || MarshalType<T>.TypeCode == TypeCode.UInt32;
    }

    public static bool CanBeStoredInRegisters { get; }

    public static bool IsIntPtr { get; } = typeof (T) == typeof (IntPtr);

    public static Type RealType { get; } = typeof (T);

    public static int Size { get; } = MarshalType<T>.TypeCode == TypeCode.Boolean ? 1 : Marshal.SizeOf(MarshalType<T>.RealType);

    public static TypeCode TypeCode { get; } = Type.GetTypeCode(MarshalType<T>.RealType);

    private static bool Test() => MarshalType<T>.IsIntPtr || MarshalType<T>.TypeCode == TypeCode.Int64 || MarshalType<T>.TypeCode == TypeCode.UInt64 || MarshalType<T>.TypeCode == TypeCode.Boolean || MarshalType<T>.TypeCode == TypeCode.Byte || MarshalType<T>.TypeCode == TypeCode.Char || MarshalType<T>.TypeCode == TypeCode.Int16 || MarshalType<T>.TypeCode == TypeCode.Int32 || MarshalType<T>.TypeCode == TypeCode.Int64 || MarshalType<T>.TypeCode == TypeCode.SByte || MarshalType<T>.TypeCode == TypeCode.Single || MarshalType<T>.TypeCode == TypeCode.UInt16 || MarshalType<T>.TypeCode == TypeCode.UInt32;

    public static byte[] ObjectToByteArray(T obj)
    {
      switch (MarshalType<T>.TypeCode)
      {
        case TypeCode.Object:
          if (MarshalType<T>.IsIntPtr)
          {
            switch (MarshalType<T>.Size)
            {
              case 4:
                return BitConverter.GetBytes(((IntPtr) (object) obj).ToInt32());
              case 8:
                return BitConverter.GetBytes(((IntPtr) (object) obj).ToInt64());
            }
          }
          else
            break;
          break;
        case TypeCode.Boolean:
          return BitConverter.GetBytes((bool) (object) obj);
        case TypeCode.Char:
          return Encoding.UTF8.GetBytes(new char[1]
          {
            (char) (object) obj
          });
        case TypeCode.Int16:
          return BitConverter.GetBytes((short) (object) obj);
        case TypeCode.UInt16:
          return BitConverter.GetBytes((ushort) (object) obj);
        case TypeCode.Int32:
          return BitConverter.GetBytes((int) (object) obj);
        case TypeCode.UInt32:
          return BitConverter.GetBytes((uint) (object) obj);
        case TypeCode.Int64:
          return BitConverter.GetBytes((long) (object) obj);
        case TypeCode.UInt64:
          return BitConverter.GetBytes((ulong) (object) obj);
        case TypeCode.Single:
          return BitConverter.GetBytes((float) (object) obj);
        case TypeCode.Double:
          return BitConverter.GetBytes((double) (object) obj);
        case TypeCode.String:
          throw new InvalidCastException("This method doesn't support string conversion.");
      }
      using (LocalUnmanagedMemory localUnmanagedMemory = new LocalUnmanagedMemory(MarshalType<T>.Size))
      {
        localUnmanagedMemory.Write<T>(obj);
        return localUnmanagedMemory.Read();
      }
    }

    public static T ByteArrayToObject(byte[] byteArray)
    {
      switch (MarshalType<T>.TypeCode)
      {
        case TypeCode.Object:
          if (MarshalType<T>.IsIntPtr)
          {
            switch (byteArray.Length)
            {
              case 1:
                return (T) (ValueType) new IntPtr(BitConverter.ToInt32(new byte[4]
                {
                  byteArray[0],
                  (byte) 0,
                  (byte) 0,
                  (byte) 0
                }, 0));
              case 2:
                return (T) (ValueType) new IntPtr(BitConverter.ToInt32(new byte[4]
                {
                  byteArray[0],
                  byteArray[1],
                  (byte) 0,
                  (byte) 0
                }, 0));
              case 4:
                return (T) (ValueType) new IntPtr(BitConverter.ToInt32(byteArray, 0));
              case 8:
                return (T) (ValueType) new IntPtr(BitConverter.ToInt64(byteArray, 0));
            }
          }
          else
            break;
          break;
        case TypeCode.Boolean:
          return (T) (ValueType) BitConverter.ToBoolean(byteArray, 0);
        case TypeCode.Char:
          return (T) (ValueType) Encoding.UTF8.GetChars(byteArray)[0];
        case TypeCode.Byte:
          return (T) (ValueType) byteArray[0];
        case TypeCode.Int16:
          return (T) (ValueType) BitConverter.ToInt16(byteArray, 0);
        case TypeCode.UInt16:
          return (T) (ValueType) BitConverter.ToUInt16(byteArray, 0);
        case TypeCode.Int32:
          return (T) (ValueType) BitConverter.ToInt32(byteArray, 0);
        case TypeCode.UInt32:
          return (T) (ValueType) BitConverter.ToUInt32(byteArray, 0);
        case TypeCode.Int64:
          return (T) (ValueType) BitConverter.ToInt64(byteArray, 0);
        case TypeCode.UInt64:
          return (T) (ValueType) BitConverter.ToUInt64(byteArray, 0);
        case TypeCode.Single:
          return (T) (ValueType) BitConverter.ToSingle(byteArray, 0);
        case TypeCode.Double:
          return (T) (ValueType) BitConverter.ToDouble(byteArray, 0);
        case TypeCode.String:
          throw new InvalidCastException("This method doesn't support string conversion.");
      }
      using (LocalUnmanagedMemory localUnmanagedMemory = new LocalUnmanagedMemory(MarshalType<T>.Size))
      {
        localUnmanagedMemory.Write(byteArray);
        return localUnmanagedMemory.Read<T>();
      }
    }

    public static T PtrToObject(IProcess memorySharp, IntPtr pointer) => MarshalType<T>.ByteArrayToObject(MarshalType<T>.CanBeStoredInRegisters ? BitConverter.GetBytes(pointer.ToInt64()) : memorySharp.Memory.Read<byte>(pointer, MarshalType<T>.Size));
  }
}
