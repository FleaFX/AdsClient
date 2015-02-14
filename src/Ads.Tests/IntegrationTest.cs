using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ads.Net;
using Ads.Routing;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class IntegrationTest {
    IAdsDevice _adsDevice;

    [TestFixtureSetUp]
    public void TestFixtureSetUp() {
      var router = new InMemoryAmsAddressRouter();
      router.AddRoute(AmsNetId.Parse("5.23.199.27.1.1"), IPAddress.Parse("192.168.178.29"));
      _adsDevice = new AdsDevice(new TcpConnectionFactory(router, 48898), AmsAddress.Parse("192.168.178.27.1.1:4785"), AmsAddress.Parse("5.23.199.27.1.1:801"));
    }

    [Test, Ignore("Integration test requires live ADS device")]
    public async void TestReadDeviceInfo() {
      var adsDeviceInfo = await _adsDevice.ReadDeviceInfo();
    }
  }
}
