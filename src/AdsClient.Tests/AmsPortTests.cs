using NUnit.Framework;

namespace AdsClient.Tests {
  [TestFixture]
  public class AmsPortTests {
    [Test]
    public void EqualValuesAreEqualInstances() {
      Assert.That(new AmsPort(1), Is.EqualTo(new AmsPort(1)));
      Assert.That(new AmsPort(), Is.EqualTo(new AmsPort(0)));
    }

    [Test]
    public void InequalValuesAreInequalInstances() {
      Assert.That(new AmsPort(1), Is.Not.EqualTo(new AmsPort()));
    }

    [Test]
    public void EqualValuesProduceEqualHashCodes() {
      Assert.That(new AmsPort(1).GetHashCode(), Is.EqualTo(new AmsPort(1).GetHashCode()));
    }

    [Test]
    public void InequalValuesProduceInequalHashCodes() {
      Assert.That(new AmsPort(1).GetHashCode(), Is.Not.EqualTo(new AmsPort().GetHashCode()));
    }

    [Test]
    public void ReservedPortNumbers() {
      Assert.That(AmsPort.Logger, Is.EqualTo(new AmsPort(100)));
      Assert.That(AmsPort.Eventlogger, Is.EqualTo(new AmsPort(110)));
      Assert.That(AmsPort.IO, Is.EqualTo(new AmsPort(300)));
      Assert.That(AmsPort.AdditionalTask1, Is.EqualTo(new AmsPort(301)));
      Assert.That(AmsPort.AdditionalTask2, Is.EqualTo(new AmsPort(302)));
      Assert.That(AmsPort.NC, Is.EqualTo(new AmsPort(500)));
      Assert.That(AmsPort.PlcRuntimeSystem1, Is.EqualTo(new AmsPort(801)));
      Assert.That(AmsPort.PlcRuntimeSystem2, Is.EqualTo(new AmsPort(811)));
      Assert.That(AmsPort.PlcRuntimeSystem3, Is.EqualTo(new AmsPort(821)));
      Assert.That(AmsPort.PlcRuntimeSystem4, Is.EqualTo(new AmsPort(831)));
      Assert.That(AmsPort.CamshaftController, Is.EqualTo(new AmsPort(900)));
      Assert.That(AmsPort.SystemService, Is.EqualTo(new AmsPort(10000)));
      Assert.That(AmsPort.Scope, Is.EqualTo(new AmsPort(14000)));
    }
  }
}