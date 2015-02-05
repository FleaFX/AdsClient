using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class AdsDeviceVersionTests {
    [Test]
    public void InstancesWithEqualMajorMinorAndBuildAreEqual() {
      Assert.That(new AdsDeviceVersion(1, 1, 1), Is.EqualTo(new AdsDeviceVersion(1, 1, 1)));
    }

    [Test]
    public void InstancesWithEqualMajorMinorButDifferentBuildAreNotEqual() {
      Assert.That(new AdsDeviceVersion(1, 1, 1), Is.Not.EqualTo(new AdsDeviceVersion(1, 1, 2)));
    }

    [Test]
    public void InstancesWithEqualMajorBuildButDifferentMinorAreNotEqual() {
      Assert.That(new AdsDeviceVersion(1, 1, 1), Is.Not.EqualTo(new AdsDeviceVersion(1, 2, 1)));
    }

    [Test]
    public void InstancesWithEqualMinorBuildButDifferentMajorAreNotEqual() {
      Assert.That(new AdsDeviceVersion(1, 1, 1), Is.Not.EqualTo(new AdsDeviceVersion(2, 1, 1)));
    }

    [Test]
    public void InstancesWithEqualMajorMinorAndBuildProduceEqualHashCode() {
      Assert.That(new AdsDeviceVersion(1, 1, 1).GetHashCode(), Is.EqualTo(new AdsDeviceVersion(1, 1, 1).GetHashCode()));
    }

    [Test]
    public void InstancesWithEqualMajorMinorButDifferentBuildProduceInequalHashCode() {
      Assert.That(new AdsDeviceVersion(1, 1, 1).GetHashCode(), Is.Not.EqualTo(new AdsDeviceVersion(1, 1, 2).GetHashCode()));
    }

    [Test]
    public void InstancesWithEqualMajorBuildButDifferentMinorProduceInequalHashCode() {
      Assert.That(new AdsDeviceVersion(1, 1, 1).GetHashCode(), Is.Not.EqualTo(new AdsDeviceVersion(1, 2, 1).GetHashCode()));
    }

    [Test]
    public void InstancesWithEqualMinorBuildButDifferentMajorProduceInequalHashCode() {
      Assert.That(new AdsDeviceVersion(1, 1, 1).GetHashCode(), Is.Not.EqualTo(new AdsDeviceVersion(2, 1, 1).GetHashCode()));
    }

    [Test]
    public void ToStringReturnsDottedNotation() {
      Assert.That(new AdsDeviceVersion(1,12,456).ToString(), Is.EqualTo("1.12.456"));
    }
  }
}