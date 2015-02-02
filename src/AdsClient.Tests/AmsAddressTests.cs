﻿using System;
using NUnit.Framework;

namespace AdsClient.Tests {
  [TestFixture]
  public class AmsAddressTests {
    [Test]
    public void RequiresAmsNetId() {
      Assert.Throws<ArgumentNullException>(() => new AmsAddress(AmsNetId.Empty, new AmsPort()));
    }

    [Test]
    public void ToStringTest() {
      Assert.That(new AmsAddress(new AmsNetId(192, 168, 10, 14, 1, 1), AmsPort.PlcRuntimeSystem1).ToString(), Is.EqualTo("192.168.10.14.1.1:801"));
    }

    [Test]
    public void ParseTests() {
      Assert.Throws<FormatException>(() => AmsAddress.Parse("some gibberish"));
      Assert.Throws<FormatException>(() => AmsAddress.Parse("192.168.10.14.257.1:801"));
      Assert.That(AmsAddress.Parse("192.168.10.14.1.1:801"), Is.EqualTo(new AmsAddress(new AmsNetId(192, 168, 10, 14, 1, 1), AmsPort.PlcRuntimeSystem1)));
    }

    [Test]
    public void EqualNetIdAndPortAreEqualInstances() {
      Assert.That(AmsAddress.Parse("192.168.10.14.1.1:801"), Is.EqualTo(AmsAddress.Parse("192.168.10.14.1.1:801")));
    }

    [Test]
    public void EqualNetIdButInequalPortAreInequalInstances() {
      Assert.That(AmsAddress.Parse("192.168.10.14.1.1:801"), Is.Not.EqualTo(AmsAddress.Parse("192.168.10.14.1.1:802")));
    }

    [Test]
    public void InequalNetIdButEqualPortAreInequalInstances() {
      Assert.That(AmsAddress.Parse("192.168.10.14.1.1:801"), Is.Not.EqualTo(AmsAddress.Parse("192.168.10.14.1.2:801")));
    }

    [Test]
    public void InequalNetIdAndInequalPortAreInequalInstances() {
      Assert.That(AmsAddress.Parse("192.168.10.14.1.1:801"), Is.Not.EqualTo(AmsAddress.Parse("192.168.10.14.1.2:802")));
    }
  }
}