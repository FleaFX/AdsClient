namespace Ads {
  public enum CommandId : ushort {
    Invalid = 0,
    AdsReadDeviceInfo = 1,
    AdsRead = 2,
    AdsWrite = 3,
    AdsReadState = 4,
    AdsWriteControl = 5,
    AdsAddDeviceNotification = 6,
    AdsDeleteDeviceNotification = 7,
    AdsDeviceNotification = 8,
    AdsReadWrite = 9
  }
}