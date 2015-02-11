using System;
using NUnit.Framework;

namespace Ads.Commands {
  [TestFixture]
  public class ReadDeviceInfoCommandRequestTests {
    [Test]
    public void RequiresSourceAmsAddress() {
      Assert.Throws<ArgumentNullException>(() => new ReadDeviceInfoCommandRequest(null, AmsAddress.Parse("192.168.10.14.1.1:801")));
    }

    [Test]
    public void RequiresTargetAmsAddress() {
      Assert.Throws<ArgumentNullException>(() => new ReadDeviceInfoCommandRequest(AmsAddress.Parse("192.168.10.14.1.1:1275"), null));
    }

    [Test]
    public void CanBeCastToAmsPacket() {
      Assert.That((AmsPacket)new ReadDeviceInfoCommandRequest(AmsAddress.Parse("192.168.10.14.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801")),
        Is.EqualTo(new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801"), CommandId.AdsReadDeviceInfo,
          StateFlags.AdsCommand | StateFlags.Request, new byte[0])));
    }
  }
}