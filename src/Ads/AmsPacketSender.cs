using System;
using System.Threading.Tasks;
using Ads.Net;

namespace Ads {
  /// <summary>
  /// Sends <see cref="AmsPacket">AMS packets</see> over TCP/IP.
  /// </summary>
  public class AmsPacketSender : IAmsPacketSender {
    readonly INetworkConnection _networkConnection;

    /// <summary>
    /// Creates a new <see cref="AmsPacketSender"/>.
    /// </summary>
    /// <param name="networkConnection">The <see cref="INetworkConnection"/> to use.</param>
    public AmsPacketSender(INetworkConnection networkConnection) {
      if (networkConnection == null) throw new ArgumentNullException("networkConnection");
      _networkConnection = networkConnection;
    }

    /// <summary>
    /// Sends the given <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="amsPacket">The <see cref="AmsPacket"/> to send.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    public async Task Send(AmsPacket amsPacket) {
      if (amsPacket == null) throw new ArgumentNullException("amsPacket");
      var buffer = new Buffer();
      amsPacket.Accept(new AmsPacketNetworkBufferWritingVisitor(buffer));
      buffer.PrependAmsTcpHeader();
      await _networkConnection.WriteAsync(buffer, 0, buffer.Length);
    }
  }
}