// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.TebStructure
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public enum TebStructure
  {
    CurrentSehFrame = 0,
    TopOfStack = 4,
    BottomOfStack = 8,
    SubSystemTeb = 12, // 0x0000000C
    FiberData = 16, // 0x00000010
    ArbitraryDataSlot = 20, // 0x00000014
    Teb = 24, // 0x00000018
    EnvironmentPointer = 28, // 0x0000001C
    ProcessId = 32, // 0x00000020
    ThreadId = 36, // 0x00000024
    RpcHandle = 40, // 0x00000028
    Tls = 44, // 0x0000002C
    Peb = 48, // 0x00000030
    LastErrorNumber = 52, // 0x00000034
    CriticalSectionsCount = 56, // 0x00000038
    CsrClientThread = 60, // 0x0000003C
    Win32ThreadInfo = 64, // 0x00000040
    Win32ClientInfo = 68, // 0x00000044
    WoW64Reserved = 192, // 0x000000C0
    CurrentLocale = 196, // 0x000000C4
    FpSoftwareStatusRegister = 200, // 0x000000C8
    SystemReserved1 = 204, // 0x000000CC
    ExceptionCode = 420, // 0x000001A4
    ActivationContextStack = 424, // 0x000001A8
    SpareBytes = 444, // 0x000001BC
    SystemReserved2 = 468, // 0x000001D4
    GdiTebBatch = 508, // 0x000001FC
    GdiRegion = 1756, // 0x000006DC
    GdiPen = 1760, // 0x000006E0
    GdiBrush = 1764, // 0x000006E4
    RealProcessId = 1768, // 0x000006E8
    RealThreadId = 1772, // 0x000006EC
    GdiCachedProcessHandle = 1776, // 0x000006F0
    GdiClientProcessId = 1780, // 0x000006F4
    GdiClientThreadId = 1784, // 0x000006F8
    GdiThreadLocalInfo = 1788, // 0x000006FC
    UserReserved1 = 1792, // 0x00000700
    GlReserved1 = 1812, // 0x00000714
    LastStatusValue = 3060, // 0x00000BF4
    StaticUnicodeString = 3064, // 0x00000BF8
    DeallocationStack = 3596, // 0x00000E0C
    TlsSlots = 3600, // 0x00000E10
    TlsLinks = 3856, // 0x00000F10
    Vdm = 3864, // 0x00000F18
    RpcReserved = 3868, // 0x00000F1C
    ThreadErrorMode = 3880, // 0x00000F28
  }
}
