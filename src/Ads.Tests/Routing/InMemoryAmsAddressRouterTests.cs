using System;
using System.Net;
using NUnit.Framework;

namespace Ads.Routing {
  [TestFixture]
  public class InMemoryAmsAddressRouterTests {
    [Test]
    public void AddEmptyAmsAddressThrows() {
      var router = new InMemoryAmsAddressRouter();
      Assert.Throws<ArgumentNullException>(() => router.Add(AmsNetId.Empty, IPAddress.Loopback));
    }

    [Test]
    public void AddInvalidIpAddressThrows() {
      var router = new InMemoryAmsAddressRouter();
      Assert.Throws<ArgumentOutOfRangeException>(() => router.Add(AmsNetId.Parse("192.168.10.14.1.1"), IPAddress.Any));
      Assert.Throws<ArgumentOutOfRangeException>(() => router.Add(AmsNetId.Parse("192.168.10.14.1.1"), IPAddress.None));
      Assert.Throws<ArgumentOutOfRangeException>(() => router.Add(AmsNetId.Parse("192.168.10.14.1.1"), IPAddress.Broadcast));
    }

    [Test]
    public void GivenAssociationIsNotKnown_ResolveAsyncThrowsUnknownAmsAddressException() {
      var router = new InMemoryAmsAddressRouter();
      Assert.Throws<UnknownAmsAddressException>(async () => await router.ResolveAsync(AmsNetId.Parse("192.168.10.14.1.1")));
    }

    [Test]
    public async void GivenAssociationIsKnown_ResolveAsyncReturnsIpAddress() {
      var router = new InMemoryAmsAddressRouter();
      router.Add(AmsNetId.Parse("192.168.10.14.1.1"), IPAddress.Parse("192.168.10.14"));
      Assert.That(await router.ResolveAsync(AmsNetId.Parse("192.168.10.14.1.1")), Is.EqualTo(IPAddress.Parse("192.168.10.14")));
    }
  }
}