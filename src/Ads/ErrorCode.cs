namespace Ads {
  public enum ErrorCode : uint {
    // Global error codes
    NoError = 0u,
    InternalError = 1u,
    NoRTime = 2u,
    AllocationLockedMemoryError = 3u,
    InsertMailboxError = 4u,
    WrongReceiveHMSG = 5u,
    TargetPortNotFound = 6u,
    TargetMachineNotFound = 7u,
    UnknownCommandId = 8u,
    BadTaskId = 9u,
    NoIo = 10u,
    UnknownAdsCommand = 11u,
    Win32Error = 12u,
    PortNotConnected = 13u,
    InvalidAdsLength = 14u,
    InvalidAdsNetId = 15u,
    LowInstallationLevel = 16u,
    NoDebugAvailable = 17u,
    PortDisabled = 18u,
    PortAlreadyConnected = 19u,
    AdsSyncWin32Error = 20u,
    AdsSyncTimeout = 21u,
    AdsSyncAmsError = 22u,
    AdsSyncNoIndexMap = 23u,
    InvalidAdsPort = 24u,
    NoMemory = 25u,
    TcpSendError = 26u,
    HostUnreachable = 27u,
    InvalidAmsFragment = 28u,

    // Router error codes (TODO)

    // General ADS error codes (TODO)

    // RTime error codes (TODO)

    // TCP WinSock error codes (TODO)
  }
}