using System;
using NUnit.Framework;

namespace AdsClient.Tests {
  [TestFixture]
  public class AmsNetIdTests {
    [Test]
    public void EmptyInstanceValues() {
      Assert.That(new AmsNetId(null), Is.EqualTo(AmsNetId.Empty));
      Assert.That(new AmsNetId(new byte[0]), Is.EqualTo(AmsNetId.Empty));
    }

    [Test]
    public void InvalidValue() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new AmsNetId(new byte[] { 0, 1, 2, 3, 4 })); // too few bytes
      Assert.Throws<ArgumentOutOfRangeException>(() => new AmsNetId(new byte[] { 0, 1, 2, 3, 4, 5, 6 })); // too many bytes
    }

    [Test]
    public void EqualValuesAreEqualInstances() {
      Assert.That(new AmsNetId(192, 168, 10, 14, 1, 1), Is.EqualTo(new AmsNetId(192, 168, 10, 14, 1, 1)));
    }

    [Test]
    public void EqualValuesProduceEqualHashCode() {
      Assert.That(new AmsNetId(192, 168, 10, 14, 1, 1).GetHashCode(), Is.EqualTo(new AmsNetId(192, 168, 10, 14, 1, 1).GetHashCode()));
    }

    [Test]
    public void InequalValuesAreInequalInstances() {
      Assert.That(new AmsNetId(192, 168, 10, 14, 1, 1), Is.Not.EqualTo(new AmsNetId(192, 168, 10, 14, 1, 2)));
    }

    [Test]
    public void InequalValuesProduceInequalHashCode() {
      Assert.That(new AmsNetId(192, 168, 10, 14, 1, 1).GetHashCode(), Is.Not.EqualTo(new AmsNetId(192, 168, 10, 14, 1, 2).GetHashCode()));
    }

    [Test]
    public void ToStringTest() {
      Assert.That(new AmsNetId(new byte[] { 192, 168, 10, 14, 1, 1 }).ToString(), Is.EqualTo("192.168.10.14.1.1"));
    }

    [Test]
    public void InvalidTryParseReturnsFalseAssignsEmptyAmsNetId() {
      AmsNetId amsNetId;

      Assert.That(AmsNetId.TryParse("198168101411", out amsNetId), Is.False); // no dots
      Assert.That(amsNetId, Is.EqualTo(AmsNetId.Empty));

      Assert.That(AmsNetId.TryParse("198.168.10.14", out amsNetId), Is.False); // too few parts
      Assert.That(amsNetId, Is.EqualTo(AmsNetId.Empty));

      Assert.That(AmsNetId.TryParse("198.168.10.14.1.1.1", out amsNetId), Is.False); // too many parts
      Assert.That(amsNetId, Is.EqualTo(AmsNetId.Empty));

      Assert.That(AmsNetId.TryParse("257.257.257.257.257.257", out amsNetId), Is.False); // each part overflows byte
      Assert.That(amsNetId, Is.EqualTo(AmsNetId.Empty));
    }

    [Test]
    public void InvalidParseThrows() {
      Assert.Throws<ArgumentNullException>(() => AmsNetId.Parse(null));
      Assert.Throws<FormatException>(() => AmsNetId.Parse("198168101411")); // no dots
      Assert.Throws<FormatException>(() => AmsNetId.Parse("198.168.10.14")); // too few parts
      Assert.Throws<FormatException>(() => AmsNetId.Parse("198.168.10.14.1.1.1")); // too many parts
      Assert.Throws<OverflowException>(() => AmsNetId.Parse("257.257.257.257.257.257")); // each part overflows byte
    }

    [Test]
    public void Parse() {
      Assert.That(AmsNetId.Parse("192.168.10.14.1.1"), Is.EqualTo(new AmsNetId(new byte[] { 192, 168, 10, 14, 1, 1 })));
    }
  }
}