using System;
using System.Collections.Generic;
using System.Linq;
using Ads.Commands;
using Ads.Net;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AmsPacketSenderTests {
    [Test]
    public void RequiresConnection() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacketSender(null));
    }

    [Test]
    public void SendRequiresPacket() {
      var sender = new AmsPacketSender(A.Dummy<IConnection>());
      Assert.Throws<ArgumentNullException>(async () => await sender.Send(null));
    }

    [Test]
    public async void SendWritesPacketBytesToConnection() {
      var connection = A.Fake<IConnection>();
      var packet = (AmsPacket)new ReadDeviceInfoCommandRequest(AmsAddress.Parse("127.0.0.1.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801"));

      var expectedSentBytes = new Buffer();
      packet.Accept(new AmsPacketNetworkBufferWritingVisitor(expectedSentBytes));

      var sender = new AmsPacketSender(connection);
      await sender.Send(packet);

      A.CallTo(() => connection.WriteAsync(A<IEnumerable<byte>>.That.Matches(b =>
        b.Take(6).SequenceEqual(new byte[] { 0, 0, 32, 0, 0, 0 }) && // AMS/TCP header
        b.Skip(6).SequenceEqual(expectedSentBytes)), // AMS header + data
        0,
        expectedSentBytes.Length + 6  // length of the AMS/TCP header + AMS header + data
      )).MustHaveHappened();
    }

    //[Test]
    //public void RequiresAmsAddressRouter() {
    //  Assert.Throws<ArgumentNullException>(() => new AmsPacketSender(null, A.Dummy<ITcpClient>()));
    //}

    //[Test]
    //public void RequiresTcpClient() {
    //  Assert.Throws<ArgumentNullException>(() => new AmsPacketSender(A.Dummy<IAmsAddressRouter>(), null));
    //}

    //[Test]
    //public async void SendConnectsToTargetAndWritesPacketToNetworkStream() {
    //  var amsAddressRouter = A.Dummy<IAmsAddressRouter>();
    //  var tcpClient = A.Dummy<ITcpClient>();
    //  var networkStream = A.Fake<Stream>();

    //  A.CallTo(() => amsAddressRouter.ResolveAsync(AmsNetId.Parse("192.168.10.14.1.1"))).Returns(IPAddress.Parse("192.168.10.14"));
    //  A.CallTo(() => tcpClient.GetStream()).Returns(networkStream);

    //  var tcpAmsPacketSender = new AmsPacketSender(amsAddressRouter, tcpClient);
    //  var packet = (AmsPacket)new ReadDeviceInfoCommandRequest(AmsAddress.Parse("127.0.0.1.1.1:1275"), AmsAddress.Parse("192.168.10.14.1.1:801"));
    //  var buffer = new Buffer();
    //  packet.Accept(new AmsPacketNetworkBufferWritingVisitor(buffer));

    //  await tcpAmsPacketSender.Send(packet);

    //  A.CallTo(() => networkStream.WriteAsync(A<byte[]>.That.Matches(b =>
    //    b.Take(6).SequenceEqual(new byte[] { 0, 0, 32, 0, 0, 0}) && // AMS/TCP header
    //    b.Skip(6).SequenceEqual(buffer)), // AMS header + data
    //    0,
    //    buffer.Length + 6,  // length of the AMS/TCP header + AMS header + data
    //    A<CancellationToken>._)).MustHaveHappened();
    //}
  }
}