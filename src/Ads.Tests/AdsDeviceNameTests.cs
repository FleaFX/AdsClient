using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AdsDeviceNameTests {
    [Test]
    public void NullByteArrayThrows() {
      Assert.Throws<ArgumentNullException>(() => new AdsDeviceName((byte[])null));
    }

    [Test]
    public void NullStringThrows() {
      Assert.Throws<ArgumentNullException>(() => new AdsDeviceName((string)null));
    }

    [Test]
    public void ByteArrayLargerThan16BytesThrows() {
      Assert.Throws<OverflowException>(() => new AdsDeviceName(Enumerable.Repeat<byte>(0, 17).ToArray()));
    }

    [Test]
    public void StringLargerThan16CharactersThrows() {
      Assert.Throws<OverflowException>(() => new AdsDeviceName(new string('a', 17)));
    }

    [Test]
    public void EqualNamesAreEqualInstances() {
      Assert.That(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")), Is.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1"))));
      Assert.That(new AdsDeviceName("Device1"), Is.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1"))));
    }

    [Test]
    public void EqualNamesProduceEqualHashCodes() {
      Assert.That(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")).GetHashCode(), Is.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")).GetHashCode()));
      Assert.That(new AdsDeviceName("Device1").GetHashCode(), Is.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")).GetHashCode()));
    }

    [Test]
    public void InequalNamesAreInequalInstances() {
      Assert.That(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")), Is.Not.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device2"))));
      Assert.That(new AdsDeviceName("Device1"), Is.Not.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device2"))));
    }

    [Test]
    public void InequalNamesProduceInequalHashCodes() {
      Assert.That(new AdsDeviceName(Encoding.UTF8.GetBytes("Device1")).GetHashCode(), Is.Not.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device2")).GetHashCode()));
      Assert.That(new AdsDeviceName("Device1").GetHashCode(), Is.Not.EqualTo(new AdsDeviceName(Encoding.UTF8.GetBytes("Device2")).GetHashCode()));
    }

    [Test]
    public void ToStringReturnsByteArrayInterpretedAsUtf8() {
      var deviceName = new AdsDeviceName(Encoding.UTF8.GetBytes("Deviceµ"));
      Assert.That(deviceName.ToString(), Is.EqualTo("Deviceµ"));

      var asciiDeviceName = new AdsDeviceName(Encoding.ASCII.GetBytes("Deviceµ"));
      Assert.That(asciiDeviceName.ToString(), Is.EqualTo("Device?"));  // received byte array was not from an utf8 encoded string
    }
  }
}