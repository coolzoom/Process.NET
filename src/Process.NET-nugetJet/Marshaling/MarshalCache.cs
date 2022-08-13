// Decompiled with JetBrains decompiler
// Type: Process.NET.Marshaling.MarshalCache`1
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Process.NET.Marshaling
{
  public static class MarshalCache<T>
  {
    public static int Size;
    public static Type RealType;
    public static TypeCode TypeCode = Type.GetTypeCode(typeof (T));
    public static bool TypeRequiresMarshal;
    public static readonly MarshalCache<T>.GetUnsafePtrDelegate GetUnsafePtr;

    static MarshalCache()
    {
      if (typeof (T) == typeof (bool))
      {
        MarshalCache<T>.Size = 1;
        MarshalCache<T>.RealType = typeof (T);
      }
      else if (typeof (T).IsEnum)
      {
        Type enumUnderlyingType = typeof (T).GetEnumUnderlyingType();
        MarshalCache<T>.Size = MarshalCache<T>.GetSizeOf(enumUnderlyingType);
        MarshalCache<T>.RealType = enumUnderlyingType;
        MarshalCache<T>.TypeCode = Type.GetTypeCode(enumUnderlyingType);
      }
      else
      {
        MarshalCache<T>.Size = MarshalCache<T>.GetSizeOf(typeof (T));
        MarshalCache<T>.RealType = typeof (T);
      }
      MarshalCache<T>.TypeRequiresMarshal = MarshalCache<T>.RequiresMarshal(MarshalCache<T>.RealType);
      DynamicMethod dynamicMethod = new DynamicMethod(string.Format("GetPinnedPtr<{0}>", (object) typeof (T).FullName.Replace(".", "<>")), typeof (void*), new Type[1]
      {
        typeof (T).MakeByRefType()
      }, typeof (MarshalCache<>).Module);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Conv_U);
      ilGenerator.Emit(OpCodes.Ret);
      MarshalCache<T>.GetUnsafePtr = (MarshalCache<T>.GetUnsafePtrDelegate) dynamicMethod.CreateDelegate(typeof (MarshalCache<T>.GetUnsafePtrDelegate));
    }

    private static int GetSizeOf(Type t)
    {
      try
      {
        return Marshal.SizeOf(t);
      }
      catch
      {
        int num = 0;
        foreach (FieldInfo field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
          object[] customAttributes = field.GetCustomAttributes(typeof (FixedBufferAttribute), false);
          if (customAttributes.Length != 0)
          {
            FixedBufferAttribute fixedBufferAttribute = customAttributes[0] as FixedBufferAttribute;
            num += MarshalCache<T>.GetSizeOf(fixedBufferAttribute.ElementType) * fixedBufferAttribute.Length;
          }
          num += MarshalCache<T>.GetSizeOf(field.FieldType);
        }
        return num;
      }
    }

    private static bool RequiresMarshal(Type t)
    {
      foreach (FieldInfo field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        bool flag = ((IEnumerable<object>) field.GetCustomAttributes(typeof (MarshalAsAttribute), true)).Any<object>();
        if (flag)
          return true;
        if (!(t == typeof (IntPtr)) && !(t == typeof (string)))
        {
          if (Type.GetTypeCode(t) == TypeCode.Object)
            flag |= MarshalCache<T>.RequiresMarshal(field.FieldType);
          if (flag)
            return true;
        }
      }
      return false;
    }

    public unsafe delegate void* GetUnsafePtrDelegate(ref T value);
  }
}
