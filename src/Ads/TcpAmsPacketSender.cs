﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Ads.Net;
using Ads.Routing;

namespace Ads {
  /// <summary>
  /// Sends <see cref="AmsPacket">AMS packets</see> over TCP/IP.
  /// </summary>
  public class TcpAmsPacketSender : IAmsPacketSender, IAmsPacketVisitor, IAmsAddressVisitor {
    readonly IAmsAddressRouter _amsAddressRouter;
    readonly ITcpClient _tcpClient;

    AmsNetId _targetAmsNetId;
    AmsPort _targetAmsPort;

    public TcpAmsPacketSender(IAmsAddressRouter amsAddressRouter, ITcpClient tcpClient) {
      if (amsAddressRouter == null) throw new ArgumentNullException("amsAddressRouter");
      if (tcpClient == null) throw new ArgumentNullException("tcpClient");
      _amsAddressRouter = amsAddressRouter;
      _tcpClient = tcpClient;
    }

    /// <summary>
    /// Sends the given <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="amsPacket">The <see cref="AmsPacket"/> to send.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    public async Task Send(AmsPacket amsPacket) {
      amsPacket.Accept(this);
      var targetIpAddress = await _amsAddressRouter.ResolveAsync(_targetAmsNetId);
      await _tcpClient.ConnectAsync(targetIpAddress, _targetAmsPort);
      using (var stream = _tcpClient.GetStream()) {
        var buffer = new Buffer();
        amsPacket.Accept(new AmsPacketNetworkBufferWritingVisitor(buffer));
        buffer.ResizeHead(6 + buffer.Length).
          Set(new byte[] { 0, 0}, 0, 2).                    // leading zeroes of AMS/TCP header
          Set(BitConverter.GetBytes(buffer.Length), 2, 4);  // length of the full packet
        await stream.WriteAsync(buffer.ToArray(), 0, buffer.Length);
      }
    }

    /// <summary>
    /// Visits the target address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="targetAmsAddress">The target <see cref="AmsAddress"/>.</param>
    void IAmsPacketVisitor.VisitTargetAmsAddress(AmsAddress targetAmsAddress) {
      targetAmsAddress.Accept(this);
    }

    /// <summary>
    /// Visits the source address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="sourceAmsAddress">The source <see cref="AmsAddress"/>.</param>
    void IAmsPacketVisitor.VisitSourceAmsAddress(AmsAddress sourceAmsAddress) { }

    /// <summary>
    /// Visits the <see cref="CommandId"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="commandId">The <see cref="CommandId"/>.</param>
    void IAmsPacketVisitor.VisitCommandId(CommandId commandId) { }

    /// <summary>
    /// Visits the <see cref="StateFlags"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="stateFlags">The <see cref="StateFlags"/>.</param>
    void IAmsPacketVisitor.VisitStateFlags(StateFlags stateFlags) { }

    /// <summary>
    /// Visits the payload of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="payload">The payload.</param>
    void IAmsPacketVisitor.VisitPayload(byte[] payload) { }

    /// <summary>
    /// Visits the <see cref="AmsNetId"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/> to visit.</param>
    void IAmsAddressVisitor.Visit(AmsNetId amsNetId) {
      _targetAmsNetId = amsNetId;
    }

    /// <summary>
    /// Visits the <see cref="AmsPort"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsPort">The <see cref="AmsPort"/> to visit.</param>
    void IAmsAddressVisitor.Visit(AmsPort amsPort) {
      _targetAmsPort = amsPort;
    }
  }
}