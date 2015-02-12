using System;
using System.Threading.Tasks;
using Ads.Net;

namespace Ads {
  /// <summary>
  /// Sends <see cref="AmsPacket">AMS packets</see> over TCP/IP.
  /// </summary>
  public class AmsPacketSender : IAmsPacketSender {
    readonly IConnection _connection;

    /// <summary>
    /// Creates a new <see cref="AmsPacketSender"/>.
    /// </summary>
    /// <param name="connection">The <see cref="IConnection"/> to use.</param>
    public AmsPacketSender(IConnection connection) {
      if (connection == null) throw new ArgumentNullException("connection");
      _connection = connection;
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
      await _connection.WriteAsync(buffer, 0, buffer.Length);
    }
  }
}