using System;
using System.Linq;
using NUnit.Framework;

namespace Ads.Adapters {
  [TestFixture]
  public class AdsDeviceInfoAdapterTests {
    [Test]
    public void GivenPacketWithUnexpectedCommandId_Throws() {
      var adapter = new AdsDeviceInfoAdapter();
      Assert.Throws<InvalidOperationException>(() =>
        adapter.Adapt(new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"), CommandId.AdsRead, StateFlags.Response | StateFlags.AdsCommand, new byte[0])));
    }

    [Test]
    public void GivenRequestPacket_Throws() {
      var adapter = new AdsDeviceInfoAdapter();
      Assert.Throws<InvalidOperationException>(() =>
        adapter.Adapt(
          new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"),
            CommandId.AdsReadDeviceInfo, StateFlags.Request | StateFlags.AdsCommand, new byte[0])));
    }

    [Test]
    public void GivenPayloadOfInvalidLength_Throws() {
      var adapter = new AdsDeviceInfoAdapter();

      // Empty payload:
      Assert.Throws<InvalidOperationException>(() =>
        adapter.Adapt(
          new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"),
            CommandId.AdsReadDeviceInfo, StateFlags.Response | StateFlags.AdsCommand, new byte[0])));

      // too few bytes
      Assert.Throws<InvalidOperationException>(() =>
        adapter.Adapt(
          new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"),
            CommandId.AdsReadDeviceInfo, StateFlags.Response | StateFlags.AdsCommand, Enumerable.Repeat<byte>(1, 23).ToArray())));

      // too many bytes
      Assert.Throws<InvalidOperationException>(() =>
        adapter.Adapt(
          new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"),
            CommandId.AdsReadDeviceInfo, StateFlags.Response | StateFlags.AdsCommand, Enumerable.Repeat<byte>(1, 25).ToArray())));
    }

    [Test]
    public void GivenValidPayload_AdaptsAndReturnsAdsDeviceInfo() {
      var adapter = new AdsDeviceInfoAdapter();

      // Empty payload:
      var adsDeviceInfo = adapter.Adapt(
          new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"),
            CommandId.AdsReadDeviceInfo, StateFlags.Response | StateFlags.AdsCommand,
            new byte[] {
              0, 0, 0, 0,                                         // result, i.e. error code
              2,                                                  // major build number
              11,                                                 // minor build number
              9, 181,                                             // build number (2485)
              65, 68, 83, 32, 68, 101, 118, 105, 99, 101, 32, 49, 0, 0, 0, 0  // device name
            }));

      Assert.That(adsDeviceInfo, Is.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(2, 11, 2485), new AdsDeviceName("ADS Device 1"))));
    }
  }
}