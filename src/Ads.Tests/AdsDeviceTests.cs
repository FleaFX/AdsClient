using System;
using System.Net;
using Ads.Net;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AdsDeviceTests {
    [Test]
    public void RequiresConnectionFactory() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(null, new IPEndPoint(IPAddress.Loopback, 801), A.Dummy<IAmsPacketSender>()));
    }

    [Test]
    public void RequiresTargetEndPoint() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(A.Dummy<INetworkConnectionFactory>(), null, A.Dummy<IAmsPacketSender>()));
    }

    [Test]
    public void RequiresAmsPacketSender() {
      Assert.Throws<ArgumentNullException>(() => new AdsDevice(A.Dummy<INetworkConnectionFactory>(), new IPEndPoint(IPAddress.Loopback, 801), null));
    }

    [Test]
    public void ReadDeviceInfoCreatesConnectionAndSendsReadDeviceInfoCommandPacket() {
      var connectionFactory = A.Fake<INetworkConnectionFactory>();
      var packetSender = A.Fake<IAmsPacketSender>();
    }
  }
}