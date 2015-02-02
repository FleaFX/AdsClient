using System;
using NUnit.Framework;

namespace AdsClient.Tests {
  [TestFixture]
  public class AmsHeaderTests {
    [Test]
    public void RequiresTargetAddress() {
      Assert.Throws<ArgumentNullException>(() => new AmsHeader(null, AmsAddress.Parse("192.168.10.14.1.1:801"), CommandId.AdsRead, StateFlags.AdsCommand));
    }

    [Test]
    public void RequiresSourceAddress() {
      Assert.Throws<ArgumentNullException>(() => new AmsHeader(AmsAddress.Parse("192.168.10.14.1.1:801"), null, CommandId.AdsRead, StateFlags.AdsCommand));
    }

    [Test]
    public void RequiresValidCommandId() {
      Assert.Throws<ArgumentException>(() => new AmsHeader(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("192.168.10.15.1.1:801"), (CommandId)10u, StateFlags.AdsCommand));
    }

    [TestCase(8, Description = "use of reserved bit 4")]
    [TestCase(9, Description = "use of reserved bit 4")]
    [TestCase(16, Description = "use of reserved bit 5")]
    [TestCase(18, Description = "use of reserved bit 5")]
    [TestCase(32, Description = "use of reserved bit 6")]
    [TestCase(40, Description = "use of reserved bit 6")]
    public void RequiresValidStateFlags(int stateFlagsValue) {
      Assert.Throws<ArgumentException>(() => new AmsHeader(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("192.168.10.15.1.1:801"), CommandId.AdsRead, (StateFlags)stateFlagsValue));
    }
  }
}