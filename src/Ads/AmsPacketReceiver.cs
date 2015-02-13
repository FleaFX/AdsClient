using System;
using System.Linq;
using System.Threading.Tasks;
using Ads.Net;

namespace Ads {
  public class AmsPacketReceiver : IAmsPacketReceiver {
    readonly INetworkConnection _connection;
    readonly IAdapter<byte[], AmsPacket> _amsPacketAdapter;

    /// <summary>
    /// Creates a new <see cref="AmsPacketReceiver"/>.
    /// </summary>
    /// <param name="connection">The <see cref="INetworkConnection"/> to receive from.</param>
    /// <param name="amsPacketAdapter">Adapts a stream of bytes to a <see cref="AmsPacket"/>.</param>
    public AmsPacketReceiver(INetworkConnection connection, IAdapter<byte[], AmsPacket> amsPacketAdapter) {
      if (connection == null) throw new ArgumentNullException("connection");
      if (amsPacketAdapter == null) throw new ArgumentNullException("amsPacketAdapter");
      _connection = connection;
      _amsPacketAdapter = amsPacketAdapter;
    }

    /// <summary>
    /// Receives an <see cref="AmsPacket"/> when it is available.
    /// </summary>
    /// <returns>An awaitable <see cref="Task{AmsPacket}"/>.</returns>
    public async Task<AmsPacket> Receive() {
      var buffer = new Buffer();
      do {
        var endOfBuffer = buffer.Length;
        var chunkBuffer = new byte[1024];
        var bytesRead = await _connection.ReadAsync(chunkBuffer, 0, chunkBuffer.Length);
        buffer.ResizeTail(buffer.Length + bytesRead);
        buffer.Set(chunkBuffer, endOfBuffer, bytesRead);
      } while (_connection.DataAvailable);
      return _amsPacketAdapter.Adapt(buffer.ToArray());
    }
  }
}