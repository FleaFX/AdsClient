using System;
using System.Linq;
using NUnit.Framework;

namespace Ads {
  [TestFixture]
  public class BufferTests {
    [Test]
    public void WhenCreatedHasZeroBytes() {
      Assert.That(new Buffer(), Is.EquivalentTo(new byte[0]));
    }

    [Test]
    public void CreateWithValueHoldsGivenValue() {
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }), Is.EquivalentTo(new byte[] { 0, 1, 2, 3, 4 }));
    }

    [Test]
    public void ResizeTailRequiresZeroOrPositiveNumber() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new Buffer().ResizeTail(-1));
    }

    [Test]
    public void GivenNewBufferWhenResizeTailThenHasNewSizeWithAllZeroes() {
      Assert.That(new Buffer().ResizeTail(10), Is.EquivalentTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
    }

    [Test]
    public void GivenFilledBufferWhenResizeTailToEnlargeThenHasNewSizeWithAllZeroesAtEnd() {
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(10), Is.EquivalentTo(new byte[] { 0, 1, 2, 3, 4, 0, 0, 0, 0, 0 }));
    }

    [Test]
    public void GivenFilledBufferWhenResizeTailToShrinkThenHasNewSizeWithValuesTruncatedFromEnd() {
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(1), Is.EquivalentTo(new byte[] { 0 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(2), Is.EquivalentTo(new byte[] { 0, 1 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(3), Is.EquivalentTo(new byte[] { 0, 1, 2 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(4), Is.EquivalentTo(new byte[] { 0, 1, 2, 3 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeTail(5), Is.EquivalentTo(new byte[] { 0, 1, 2, 3, 4 }));
    }

    [Test]
    public void ResizeHeadRequiresZeroOrPositiveNumber() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new Buffer().ResizeHead(-1));
    }

    [Test]
    public void GivenNewBufferWhenResizeHeadThenHasNewSizeWithAllZeroes() {
      Assert.That(new Buffer().ResizeHead(10), Is.EquivalentTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
    }

    [Test]
    public void GivenFilledBufferWhenResizeHeadToEnlargeThenHasNewSizeWithAllZeroesAtStart() {
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(10), Is.EquivalentTo(new byte[] { 0, 0, 0, 0, 0, 0, 1, 2, 3, 4 }));
    }

    [Test]
    public void GivenFilledBufferWhenResizeHeadToShrinkThenHasNewSizeWithValuesTruncatedFromStart() {
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(1), Is.EquivalentTo(new byte[] { 4 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(2), Is.EquivalentTo(new byte[] { 3, 4 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(3), Is.EquivalentTo(new byte[] { 2, 3, 4 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(4), Is.EquivalentTo(new byte[] { 1, 2, 3, 4 }));
      Assert.That(new Buffer(new byte[] { 0, 1, 2, 3, 4 }).ResizeHead(5), Is.EquivalentTo(new byte[] { 0, 1, 2, 3, 4 }));
    }

    [Test]
    public void SetRequiresBuffer() {
      var buffer = new Buffer();
      Assert.Throws<ArgumentNullException>(() => buffer.Set(null, 0, 0));
    }

    [Test]
    public void SetRequiresPositiveOrZeroIndex() {
      var buffer = new Buffer();
      Assert.Throws<ArgumentOutOfRangeException>(() => buffer.Set(new byte[1], -1, 0));
    }

    [Test]
    public void SetRequiresPositiveOrZeroCount() {
      var buffer = new Buffer();
      Assert.Throws<ArgumentOutOfRangeException>(() => buffer.Set(new byte[1], 0, -1));

    }

    [Test]
    public void GivenSetValuesWillOccupyPlacesOutsizeBufferThrows() {
      var buffer = new Buffer(Enumerable.Repeat<byte>(0, 5).ToArray());
      Assert.Throws<OverflowException>(() => buffer.Set(new byte[] { 1, 2, 3 }, 3, 3)); // overflow buffer at tail
      Assert.Throws<OverflowException>(() => buffer.Set(new byte[] { 1, 2, 3, 4, 5, 6 }, 0, 6)); // set larger than what buffer accomodates
    }

    [Test]
    public void SetFillsBufferWithGivenValues() {
      var buffer = new Buffer(Enumerable.Repeat<byte>(0, 5).ToArray());
      Assert.DoesNotThrow(() => buffer.Set(new byte[] { 1, 2, 3, 4, 5 }, 2, 3));
      Assert.That(buffer, Is.EquivalentTo(new byte[] { 0, 0, 1, 2, 3 }));
    }
  }
}