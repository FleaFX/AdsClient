namespace AdsClient {
  public enum CommandId : uint {
    Invalid = 0u,
    AdsReadDeviceInfo = 1u,
    AdsRead = 2u,
    AdsWrite = 3u,
    AdsReadState = 4u,
    AdsWriteControl = 5u,
    AdsAddDeviceNotification = 6u,
    AdsDeleteDeviceNotification = 7u,
    AdsDeviceNotification = 8u,
    AdsReadWrite = 9u
  }
}