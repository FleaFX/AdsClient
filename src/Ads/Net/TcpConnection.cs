using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ads.Net {
  public class TcpConnection : INetworkConnection {
    readonly TcpClient _client;
    readonly NetworkStream _stream;

    public TcpConnection(TcpClient client) {
      if (client == null) throw new ArgumentNullException("client");
      _client = client;
      _stream = _client.GetStream();
    }

    /// <summary>
    /// Sends the given bytes to the other end of the connection.
    /// </summary>
    /// <param name="buffer">The bytes to send.</param>
    /// <param name="offset">The offset in the given collection from where to begin copying to the stream.</param>
    /// <param name="count">The number of bytes to copy to the stream.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task WriteAsync(byte[] buffer, int offset, int count) {
      return _stream.WriteAsync(buffer, offset, count);
    }

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">The buffer to write the data into.</param>
    /// <param name="offset">The byte offset in buffer at which to begin writing data from the stream.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
    public Task<int> ReadAsync(byte[] buffer, int offset, int count) {
      return _stream.ReadAsync(buffer, offset, count);
    }

    /// <summary>
    /// Gets a value that indicates whether data is available on the NetworkStream to be read.
    /// </summary>
    /// <value><c>true</c> if data is available on the stream to be read; otherwise, <c>false</c>.</value>
    public bool DataAvailable { get { return _stream.DataAvailable; } }

    bool _disposed;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing) {
      if (!_disposed) {
        if (disposing) {
          _stream.Close();
          _stream.Dispose();

          _client.Close();
          ((IDisposable)_client).Dispose();
        }
        _disposed = true;
      }
    }
  }
}