// Decompiled with JetBrains decompiler
// Type: Process.NET.Extensions.AttributeExtensions
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Process.NET.Extensions
{
  public static class AttributeExtensions
  {
    public static T GetAttribute<T>(this Type type) => (T) ((IEnumerable<object>) type.GetCustomAttributes(typeof (T), false)).FirstOrDefault<object>();

    public static bool HasAttribute<T>(this Type item) => (uint) item.GetCustomAttributes(typeof (T), true).Length > 0U;

    public static T GetAttributes<T>(this Type type) => type.HasAttribute<T>() ? (T) ((IEnumerable<object>) type.GetCustomAttributes(typeof (T), false)).FirstOrDefault<object>() : throw new Exception(string.Format("No attirubute found for {0}.", (object) type));

    public static bool IsUnmanagedFunctionPointer(this Delegate d) => d.GetType().IsUnmanagedFunctionPointer();

    public static bool IsUnmanagedFunctionPointer(this Type t) => t.HasAttribute<UnmanagedFunctionPointerAttribute>();
  }
}
