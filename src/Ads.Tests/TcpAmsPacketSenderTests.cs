using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Ads.Commands;
using Ads.Net;
using Ads.Routing;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class TcpAmsPacketSenderTests {
    [Test]
    public void RequiresAmsAddressRouter() {
      Assert.Throws<ArgumentNullException>(() => new TcpAmsPacketSender(null, A.Dummy<ITcpClient>()));
    }

    [Test]
    public void RequiresTcpClient() {
      Assert.Throws<ArgumentNullException>(() => new TcpAmsPacketSender(A.Dummy<IAmsAddressRouter>(), null));
    }

    [Test]
    public async void SendConnectsToTargetAndWritesPacketToNetworkStream() {
      var amsAddressRouter = A.Dummy<IAmsAddressRouter>();
      var tcpClient = A.Dummy<ITcpClient>();
      var networkStream = A.Fake<Stream>();

      A.CallTo(() => amsAddressRouter.ResolveAsync(AmsNetId.Parse("192.168.10.14.1.1"))).Returns(IPAddress.Parse("192.168.10.14"));
      A.CallTo(() => tcpClient.GetStream()).Returns(networkStream);

      var tcpAmsPacketSender = new TcpAmsPacketSender(amsAddressRouter, tcpClient);
      var packet = (AmsPacket)new ReadDeviceInfoCommandRequest(AmsAddress.Parse("127.0.0.1.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801"));
      var buffer = new Buffer();
      packet.Accept(new AmsPacketNetworkBufferWritingVisitor(buffer));

      await tcpAmsPacketSender.Send(packet);

      A.CallTo(() => networkStream.WriteAsync(A<byte[]>.That.Matches(b =>
        b.Take(6).SequenceEqual(new byte[] { 0, 0, 32, 0, 0, 0}) && // AMS/TCP header
        b.Skip(6).SequenceEqual(buffer)), // AMS header + data
        0,
        buffer.Length + 6,  // length of the AMS/TCP header + AMS header + data
        A<CancellationToken>._)).MustHaveHappened();
    }
  }
}