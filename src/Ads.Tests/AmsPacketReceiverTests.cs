using System;
using System.Linq;
using Ads.Adapters;
using Ads.Net;
using FakeItEasy;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AmsPacketReceiverTests {
    [Test]
    public void RequiresConnection() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacketReceiver(null, A.Dummy<IAdapter<byte[], AmsPacket>>()));
    }

    [Test]
    public void RequiresAmsPacketAdapter() {
      Assert.Throws<ArgumentNullException>(() => new AmsPacketReceiver(A.Dummy<INetworkConnection>(), null));
    }

    [Test]
    public async void ReceiveAdaptsBytesReceivedFromNetworkConnectionAndReturnsAdaptedPacket() {
      var connection = A.Dummy<INetworkConnection>();
      var adapter = A.Fake<IAdapter<byte[], AmsPacket>>();
      var expectedPacket = new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"), CommandId.AdsReadDeviceInfo, StateFlags.AdsCommand | StateFlags.Response, new byte[] {0, 1, 2, 3, 4});

      A.CallTo(() => connection.DataAvailable).Returns( false);
      A.CallTo(() => connection.ReadAsync(A<byte[]>._, 0, 1024)).Invokes(call => Array.Copy(new byte[] { 0, 1, 2, 3, 4 }, 0, (byte[])call.Arguments[0], 0, 5)).Returns(5);
      A.CallTo(() => adapter.Adapt(A<byte[]>.That.Matches(b => b.SequenceEqual(new byte[] { 0, 1, 2, 3, 4 })))).Returns(expectedPacket);

      var receiver = new AmsPacketReceiver(connection, adapter);
      var result = await receiver.Receive();

      Assert.That(result, Is.EqualTo(expectedPacket));
    }

    [Test]
    public async void ReceiveKeepsReadingFromNetworkConnectionAsLongAsThereIsDataAvailable() {
      var connection = A.Dummy<INetworkConnection>();
      var adapter = A.Fake<IAdapter<byte[], AmsPacket>>();
      var expectedPacket = new AmsPacket(AmsAddress.Parse("192.168.10.14.1.1:801"), AmsAddress.Parse("127.0.0.1.1.1:1275"), CommandId.AdsReadDeviceInfo, StateFlags.AdsCommand | StateFlags.Response, new byte[] { 0, 1, 2, 3, 4 });

      A.CallTo(() => connection.ReadAsync(A<byte[]>._, 0, 1024)).
        Invokes(call => {
          var bytesRead = Enumerable.Repeat<byte>(1, 1024).ToArray();
          Array.Copy(bytesRead, 0, (byte[])call.Arguments[0], 0, 1024);
        }).
        ReturnsNextFromSequence(1024, 1024, 1024, 5);
      A.CallTo(() => connection.DataAvailable).ReturnsNextFromSequence(true, true, true, false);
      A.CallTo(() => adapter.Adapt(A<byte[]>.That.Matches(b => b.Take(3077).SequenceEqual(Enumerable.Repeat<byte>(1, 3077))))).Returns(expectedPacket);

      var receiver = new AmsPacketReceiver(connection, adapter);
      var result = await receiver.Receive();

      A.CallTo(() => connection.ReadAsync(A<byte[]>._, 0, 1024)).MustHaveHappened(Repeated.Exactly.Times(4));
      Assert.That(result, Is.EqualTo(expectedPacket));
    }
  }
}