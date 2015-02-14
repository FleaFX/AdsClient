using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ads.Net {
  public interface INetworkConnection : IDisposable {
    /// <summary>
    /// Sends the given bytes to the other end of the connection.
    /// </summary>
    /// <param name="buffer">The bytes to send.</param>
    /// <param name="offset">The offset in the given collection from where to begin copying to the stream.</param>
    /// <param name="count">The number of bytes to copy to the stream.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task WriteAsync(byte[] buffer, int offset, int count);

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">The buffer to write the data into.</param>
    /// <param name="offset">The byte offset in buffer at which to begin writing data from the stream.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
    Task<int> ReadAsync(byte[] buffer, int offset, int count);

    /// <summary>
    /// Gets a value that indicates whether data is available on the NetworkStream to be read.
    /// </summary>
    /// <value><c>true</c> if data is available on the stream to be read; otherwise, <c>false</c>.</value>
    bool DataAvailable { get; }
  }
}