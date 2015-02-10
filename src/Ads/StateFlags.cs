using System;

namespace Ads {
  [Flags]
  public enum StateFlags : ushort {
    Request = 0,
    Response = 1,
    AdsCommand = 4,
    ReservedBit4 = 8,
    ReservedBit5 = 16,
    ReservedBit6 = 32,
    UseUdp = 64
  }
}