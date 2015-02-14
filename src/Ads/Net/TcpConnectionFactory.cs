using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ads.Routing;

namespace Ads.Net {
  public class TcpConnectionFactory : INetworkConnectionFactory, IAmsAddressVisitor {
    readonly IAmsAddressRouter _router;
    readonly int _port;

    AmsNetId _targetAmsNetId;
    AmsPort _targetAmsPort;

    public TcpConnectionFactory(IAmsAddressRouter router, int port) {
      if (router == null) throw new ArgumentNullException("router");
      _router = router;
      _port = port;
    }

    /// <summary>
    /// Creates a new <see cref="INetworkConnection"/>.
    /// </summary>
    /// <param name="target"></param>
    /// <returns>A <see cref="INetworkConnection"/>.</returns>
    public async Task<INetworkConnection> Create(AmsAddress target) {
      target.Accept(this);
      var ipAddress = await _router.ResolveAsync(_targetAmsNetId);
      var client = new TcpClient();
      await client.ConnectAsync(ipAddress, _port);
      return new TcpConnection(client);
    }

    void IAmsAddressVisitor.Visit(AmsNetId amsNetId) {
      _targetAmsNetId = amsNetId;
    }

    void IAmsAddressVisitor.Visit(AmsPort amsPort) {
      _targetAmsPort = amsPort;
    }
  }
}