using System.Linq;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AmsPacketNetworkBufferWritingVisitorTests {
    [Test]
    public void VisitSourceAmsAddressFillsBytes0Through7OfTheBuffer() {
      var buffer = new Buffer();
      var visitor = new AmsPacketNetworkBufferWritingVisitor(buffer);
      visitor.VisitSourceAmsAddress(AmsAddress.Parse("192.168.10.14.1.1:801"));

      Assert.That(buffer.Length, Is.GreaterThanOrEqualTo(8));
      Assert.That(buffer.Take(8).ToArray(), Is.EquivalentTo(new[] { 1, 1, 14, 10, 168, 192, 3, 33}));
    }

    [Test]
    public void VisitTargetAmsAddressFillsBytes6Through15OfTheBuffer() {
      var buffer = new Buffer();
      var visitor = new AmsPacketNetworkBufferWritingVisitor(buffer);
      visitor.VisitTargetAmsAddress(AmsAddress.Parse("192.168.10.14.1.1:801"));

      Assert.That(buffer.Length, Is.GreaterThanOrEqualTo(16));
      Assert.That(buffer.Skip(8).Take(8).ToArray(), Is.EquivalentTo(new[] { 1, 1, 14, 10, 168, 192, 3, 33 }));
    }

    [Test]
    public void VisitCommandIdFillsBytes16And17OfTheBuffer() {
      var buffer = new Buffer();
      var visitor = new AmsPacketNetworkBufferWritingVisitor(buffer);
      visitor.VisitCommandId(CommandId.AdsReadWrite);

      Assert.That(buffer.Length, Is.GreaterThanOrEqualTo(18));
      Assert.That(buffer.Skip(16).Take(2).ToArray(), Is.EquivalentTo(new[] { 0, 9 }));
    }

    [Test]
    public void VisitStateFlagsFillsBytes18And19OfTheBuffer() {
      var buffer = new Buffer();
      var visitor = new AmsPacketNetworkBufferWritingVisitor(buffer);
      visitor.VisitStateFlags(StateFlags.AdsCommand | StateFlags.Request);

      Assert.That(buffer.Length, Is.GreaterThanOrEqualTo(20));
      Assert.That(buffer.Skip(18).Take(2).ToArray(), Is.EquivalentTo(new[] { 0, 4 }));
    }

    [Test]
    public void VisitPayloadFillsBytes20Through23OfTheBufferAndAppendsFullPayloadContent() {
      var buffer = new Buffer();
      var visitor = new AmsPacketNetworkBufferWritingVisitor(buffer);
      visitor.VisitPayload(new byte[] {0, 1, 2, 3, 4, 5});

      Assert.That(buffer.Length, Is.EqualTo(38));
      Assert.That(buffer.Skip(20).Take(4).ToArray(), Is.EquivalentTo(new[] {0, 0, 0, 6}));
      Assert.That(buffer.Skip(32).ToArray(), Is.EquivalentTo(new byte[] {0, 1, 2, 3, 4, 5} ));
    }
  }
}