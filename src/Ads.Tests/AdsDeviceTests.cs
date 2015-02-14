using System;
using Ads.Net;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AdsDeviceTests {
    [Test]
    public void RequiresNetworkConnectionFactory() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(null, AmsAddress.Parse("127.0.0.1.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801")));
    }

    [Test]
    public void RequiresSourceAmsAddress() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(A.Dummy<INetworkConnectionFactory>(), null, AmsAddress.Parse("192.168.10.14.1.1:801")));
    }

    [Test]
    public void RequiresTargetAmsAddress() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(A.Dummy<INetworkConnectionFactory>(), AmsAddress.Parse("127.0.0.1.1.1:1275"), null));
    }
  }
}