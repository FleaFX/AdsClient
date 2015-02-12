using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AmsPacketTests {
    [Test]
    public void RequiresTargetAddress() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), null, CommandId.AdsRead, StateFlags.AdsCommand, new byte[0]));
    }

    [Test]
    public void RequiresSourceAddress() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacket(null, AmsAddress.Parse("192.168.10.14.1.1:801"), CommandId.AdsRead, StateFlags.AdsCommand, new byte[0]));
    }

    [Test]
    public void RequiresValidCommandId() {
      Assert.Throws<ArgumentException>(() => new AmsPacket(AmsAddress.Parse("192.168.10.15.1.1:801"), AmsAddress.Parse("192.168.10.14.1.1:801"), (CommandId)10u, StateFlags.AdsCommand, new byte[0]));
    }

    [TestCase(8, Description = "use of reserved bit 4")]
    [TestCase(9, Description = "use of reserved bit 4")]
    [TestCase(16, Description = "use of reserved bit 5")]
    [TestCase(18, Description = "use of reserved bit 5")]
    [TestCase(32, Description = "use of reserved bit 6")]
    [TestCase(40, Description = "use of reserved bit 6")]
    public void RequiresValidStateFlags(int stateFlagsValue) {
      Assert.Throws<ArgumentException>(() => new AmsPacket(AmsAddress.Parse("192.168.10.15.1.1:801"), AmsAddress.Parse("192.168.10.14.1.1:801"), CommandId.AdsRead, (StateFlags)stateFlagsValue, new byte[0]));
    }

    [Test]
    public void RequiresPayload() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacket(AmsAddress.Parse("192.168.10.15.1.1:801"), AmsAddress.Parse("192.168.10.14.1.1:801"), CommandId.AdsRead, StateFlags.AdsCommand, null));
    }

    [Test]
    public void AcceptTraversesThePacket() {
      var visitor = A.Fake<IAmsPacketVisitor>();
      var packet = new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:1256"), AmsAddress.Parse("192.168.10.15.1.1:801"),
        CommandId.AdsRead, StateFlags.Request & StateFlags.AdsCommand, new byte[] {0, 1, 2, 3});


      using (var scope = Fake.CreateScope()) {
        packet.Accept(visitor);
        
        using (scope.OrderedAssertions()) {
          A.CallTo(() => visitor.VisitSourceAmsAddress(AmsAddress.Parse("192.168.10.14.1.1:1256"))).MustHaveHappened();
          A.CallTo(() => visitor.VisitTargetAmsAddress(AmsAddress.Parse("192.168.10.15.1.1:801"))).MustHaveHappened();
          A.CallTo(() => visitor.VisitCommandId(CommandId.AdsRead)).MustHaveHappened();
          A.CallTo(() => visitor.VisitStateFlags(StateFlags.Request & StateFlags.AdsCommand)).MustHaveHappened();
          A.CallTo(() => visitor.VisitPayload(A<byte[]>.That.Matches(b => b.SequenceEqual(new byte[] { 0, 1, 2, 3})))).MustHaveHappened();
        }
      }
    }
  }
}