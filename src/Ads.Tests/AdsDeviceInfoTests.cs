using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AdsDeviceInfoTests {
    [Test]
    public void InstancesWithEqualVersionNumberAndNameAreEqual() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1,1,1), new AdsDeviceName("Device1")),
        Is.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1,1,1), new AdsDeviceName("Device1"))));
    }

    [Test]
    public void InstancesWithEqualVersionNumberAndNameProduceEqualHashcode() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")).GetHashCode(),
        Is.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")).GetHashCode()));
    }

    [Test]
    public void InstancesWithInequalVersionNumberAndEqualNameAreInequal() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")),
        Is.Not.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 2), new AdsDeviceName("Device1"))));
    }

    [Test]
    public void InstancesWithEqualVersionNumberAndInequalNameAreInequal() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")),
        Is.Not.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device2"))));
    }

    [Test]
    public void InstancesWithInequalVersionNumberAndEqualNameProduceInequalHashcode() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")).GetHashCode(),
        Is.Not.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 2), new AdsDeviceName("Device1")).GetHashCode()));
    }

    [Test]
    public void InstancesWithEqualVersionNumberAndInequalNameProduceInequalHashcode() {
      Assert.That(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device1")).GetHashCode(),
        Is.Not.EqualTo(new AdsDeviceInfo(new AdsDeviceVersion(1, 1, 1), new AdsDeviceName("Device2")).GetHashCode()));
    }
  }
}