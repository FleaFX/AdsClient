using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ads.Net {
  public class TcpConnectionFactory : INetworkConnectionFactory<TcpConnection, IPEndPoint> {
    readonly TcpClient _tcpClient;

    /// <summary>
    /// Creates a new <see cref="TcpConnectionFactory"/>.
    /// </summary>
    public TcpConnectionFactory() {
      _tcpClient = new TcpClient();
    }

    /// <summary>
    /// Creates a new <see cref="TcpConnection"/>.
    /// </summary>
    /// <param name="connectionInfo">The connection info.</param>
    /// <returns>A <see cref="Task{TcpConnection}"/>.</returns>
    public async Task<TcpConnection> Create(IPEndPoint connectionInfo) {
      await _tcpClient.ConnectAsync(connectionInfo.Address, connectionInfo.Port);
      return new TcpConnection(_tcpClient.GetStream());
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    bool _disposed;
    protected void Dispose(bool disposing) {
      if (disposing) {
        if (!_disposed) {
          ((IDisposable)_tcpClient).Dispose();
        }
        _disposed = true;
      }
    }
  }
}