using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AmsPacketAdapterTests {
    IAdapter<byte[], AmsPacket> _adapter;
    AmsPacket _result;
    
    [TestFixtureSetUp]
    public void FixtureSetUp() {
      _adapter = new AmsPacketAdapter();
      _result = _adapter.Adapt(new byte[] {
        192, 168, 10, 14, 1, 1, 33, 3,  // 8 bytes: target address (net id + port)
        127, 0, 0, 1, 1, 1, 217, 4,     // 8 bytes: source address (net id + port)
        0, 1,                           // 2 bytes: command id
        0, 5,                           // 2 bytes: state flags
        0, 0, 0, 24,                    // 4 bytes: length
        0, 0, 0, 24,                    // 4 bytes: error code
        0, 0, 0, 25,                    // 4 bytes: invoke id
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 // data: number of bytes as specified in length field
      });
    }

    [Test]
    public void AdaptCreatesExpectedAmsPacket() {
      Assert.That(_result, Is.EqualTo(
        new AmsPacket(
          AmsAddress.Parse("127.0.0.1.1.1:1241"),
          AmsAddress.Parse("192.168.10.14.1.1:801"),
          CommandId.AdsReadDeviceInfo,
          StateFlags.AdsCommand | StateFlags.Response,
          new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 })));
    }
  }
}