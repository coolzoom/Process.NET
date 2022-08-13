// Decompiled with JetBrains decompiler
// Type: Process.NET.Native.Types.PebStructure
// Assembly: Process.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9637A61E-08D4-4715-82E9-FCE8163D082E
// Assembly location: F:\WOWServer\Source\WowClassicGrindBot\BlazorServer\bin\x64\Debug\net6.0\Process.NET.dll

namespace Process.NET.Native.Types
{
  public enum PebStructure
  {
    InheritedAddressSpace = 0,
    ReadImageFileExecOptions = 1,
    BeingDebugged = 2,
    SpareBool = 3,
    Mutant = 4,
    ImageBaseAddress = 8,
    Ldr = 12, // 0x0000000C
    ProcessParameters = 16, // 0x00000010
    SubSystemData = 20, // 0x00000014
    ProcessHeap = 24, // 0x00000018
    FastPebLock = 28, // 0x0000001C
    FastPebLockRoutine = 32, // 0x00000020
    FastPebUnlockRoutine = 36, // 0x00000024
    EnvironmentUpdateCount = 40, // 0x00000028
    KernelCallbackTable = 44, // 0x0000002C
    SystemReserved = 48, // 0x00000030
    AtlThunkSListPtr32 = 52, // 0x00000034
    FreeList = 56, // 0x00000038
    TlsExpansionCounter = 60, // 0x0000003C
    TlsBitmap = 64, // 0x00000040
    TlsBitmapBits = 68, // 0x00000044
    ReadOnlySharedMemoryBase = 76, // 0x0000004C
    ReadOnlySharedMemoryHeap = 80, // 0x00000050
    ReadOnlyStaticServerData = 84, // 0x00000054
    AnsiCodePageData = 88, // 0x00000058
    OemCodePageData = 92, // 0x0000005C
    UnicodeCaseTableData = 96, // 0x00000060
    NumberOfProcessors = 100, // 0x00000064
    NtGlobalFlag = 104, // 0x00000068
    CriticalSectionTimeout = 112, // 0x00000070
    HeapSegmentReserve = 120, // 0x00000078
    HeapSegmentCommit = 124, // 0x0000007C
    HeapDeCommitTotalFreeThreshold = 128, // 0x00000080
    HeapDeCommitFreeBlockThreshold = 132, // 0x00000084
    NumberOfHeaps = 136, // 0x00000088
    MaximumNumberOfHeaps = 140, // 0x0000008C
    ProcessHeaps = 144, // 0x00000090
    GdiSharedHandleTable = 148, // 0x00000094
    ProcessStarterHelper = 152, // 0x00000098
    GdiDcAttributeList = 156, // 0x0000009C
    LoaderLock = 160, // 0x000000A0
    OsMajorVersion = 164, // 0x000000A4
    OsMinorVersion = 168, // 0x000000A8
    OsBuildNumber = 172, // 0x000000AC
    OsCsdVersion = 174, // 0x000000AE
    OsPlatformId = 176, // 0x000000B0
    ImageSubsystem = 180, // 0x000000B4
    ImageSubsystemMajorVersion = 184, // 0x000000B8
    ImageSubsystemMinorVersion = 188, // 0x000000BC
    ImageProcessAffinityMask = 192, // 0x000000C0
    GdiHandleBuffer = 196, // 0x000000C4
    PostProcessInitRoutine = 332, // 0x0000014C
    TlsExpansionBitmap = 336, // 0x00000150
    TlsExpansionBitmapBits = 340, // 0x00000154
    SessionId = 468, // 0x000001D4
    AppCompatFlags = 472, // 0x000001D8
    AppCompatFlagsUser = 480, // 0x000001E0
    ShimData = 488, // 0x000001E8
    AppCompatInfo = 492, // 0x000001EC
    CsdVersion = 496, // 0x000001F0
    ActivationContextData = 504, // 0x000001F8
    ProcessAssemblyStorageMap = 508, // 0x000001FC
    SystemDefaultActivationContextData = 512, // 0x00000200
    SystemAssemblyStorageMap = 516, // 0x00000204
    MinimumStackCommit = 520, // 0x00000208
  }
}
