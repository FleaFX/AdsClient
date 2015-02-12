using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ads {
  /// <summary>
  /// Stores a sequence of bytes.
  /// </summary>
  public class Buffer : IEnumerable<byte> {
    readonly List<byte> _buffer;

    /// <summary>
    /// Creates a new <see cref="Buffer"/>.
    /// </summary>
    public Buffer() {
      _buffer = new List<byte>();
    }

    /// <summary>
    /// Creates a new <see cref="Buffer"/>, prefilled with the given values.
    /// </summary>
    /// <param name="initialValues">The initial values to store in the buffer.</param>
    public Buffer(IEnumerable<byte> initialValues) {
      _buffer = new List<byte>(initialValues);
    }

    /// <summary>
    /// Gets the number of bytes in the buffer.
    /// </summary>
    public int Length { get { return _buffer.Count; } }

    /// <summary>
    /// Resizes the buffer, adding or removing bytes from the tail.
    /// </summary>
    /// <param name="totalSize">The new total size of the buffer.</param>
    /// <returns>The new <see cref="Buffer"/>.</returns>
    public Buffer ResizeTail(int totalSize) {
      if (totalSize < 0) throw new ArgumentOutOfRangeException("totalSize");
      if (totalSize > _buffer.Count) _buffer.AddRange(Enumerable.Repeat<byte>(0, totalSize - _buffer.Count));
      else _buffer.RemoveRange(totalSize, _buffer.Count - totalSize);
      return this;
    }

    /// <summary>
    /// Resizes the buffer, adding or removing bytes from the head.
    /// </summary>
    /// <param name="totalSize">The new total size of the buffer.</param>
    /// <returns>The new <see cref="Buffer"/>.</returns>
    public Buffer ResizeHead(int totalSize) {
      if (totalSize < 0) throw new ArgumentOutOfRangeException("totalSize");
      if (totalSize > _buffer.Count) _buffer.InsertRange(0, Enumerable.Repeat<byte>(0, totalSize - _buffer.Count));
      else _buffer.RemoveRange(0, _buffer.Count - totalSize);
      return this;
    }

    /// <summary>
    /// Sets the values of the buffer at the given places.
    /// </summary>
    /// <param name="values">The new values.</param>
    /// <param name="index">The index of where to start setting the values.</param>
    /// <param name="count">The number of values to set.</param>
    /// <returns>The new <see cref="Buffer"/>.</returns>
    public Buffer Set(byte[] values, int index, int count) {
      if (values == null) throw new ArgumentNullException("values");
      if (index < 0) throw new ArgumentOutOfRangeException("index");
      if (count < 0) throw new ArgumentOutOfRangeException("count");
      if ((index + count) > _buffer.Count) throw new OverflowException();
      _buffer.RemoveRange(index, count);
      _buffer.InsertRange(index, values.Take(count));
      return this;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    public IEnumerator<byte> GetEnumerator() {
      return _buffer.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }
}