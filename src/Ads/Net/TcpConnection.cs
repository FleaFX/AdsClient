using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ads.Net {
  /// <summary>
  /// TCP/IP implementation of <see cref="INetworkConnection"/>.
  /// </summary>
  public class TcpConnection : INetworkConnection, IDisposable {
    readonly NetworkStream _stream;

    /// <summary>
    /// Creates a new <see cref="TcpConnection"/>.
    /// </summary>
    /// <param name="stream"></param>
    public TcpConnection(NetworkStream stream) {
      if (stream == null) throw new ArgumentNullException("stream");
      _stream = stream;
    }

    /// <summary>
    /// Sends the given bytes to the other end of the connection.
    /// </summary>
    /// <param name="buffer">The bytes to send.</param>
    /// <param name="offset">The offset in the given collection from where to begin copying to the stream.</param>
    /// <param name="count">The number of bytes to copy to the stream.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task WriteAsync(IEnumerable<byte> buffer, int offset, int count) {
      return _stream.WriteAsync(buffer.ToArray(), offset, count);
    }

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
      if (disposing) {
        if (!_disposed) {
          _stream.Dispose();
        }
        _disposed = true;
      }
    }
  }
}