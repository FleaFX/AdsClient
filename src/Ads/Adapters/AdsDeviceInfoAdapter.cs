using System;
using System.Linq;

namespace Ads.Adapters {
  /// <summary>
  /// Adapts a received <see cref="AmsPacket"/> to an <see cref="AdsDeviceInfo"/>.
  /// </summary>
  public class AdsDeviceInfoAdapter : IAdapter<AmsPacket, AdsDeviceInfo>, IAmsPacketVisitor {
    AdsDeviceInfo _adsDeviceInfo;

    /// <summary>
    /// Adapts a <see cref="AmsPacket"/> to produce a <see cref="AdsDeviceInfo"/>.
    /// </summary>
    /// <param name="subject">The object being adapter.</param>
    /// <returns>A <see cref="AdsDeviceInfo"/>.</returns>
    public AdsDeviceInfo Adapt(AmsPacket subject) {
      subject.Accept(this);
      return _adsDeviceInfo;
    }

    void IAmsPacketVisitor.VisitSourceAmsAddress(AmsAddress sourceAmsAddress) { }

    void IAmsPacketVisitor.VisitTargetAmsAddress(AmsAddress targetAmsAddress) { }

    void IAmsPacketVisitor.VisitCommandId(CommandId commandId) {
      if (commandId != CommandId.AdsReadDeviceInfo)
        throw new InvalidOperationException(string.Format("The packet contains a {0} command, expected ReadDeviceInfo.", Enum.GetName(typeof(CommandId), commandId))); // throw better type
    }

    void IAmsPacketVisitor.VisitStateFlags(StateFlags stateFlags) {
      if (!stateFlags.HasFlag(StateFlags.Response))
        throw new InvalidOperationException("The packet represents a request, expected a response."); // todo: throw better type
    }

    void IAmsPacketVisitor.VisitPayload(byte[] payload) {
      if (payload.Length != 24)
        throw new InvalidOperationException("The packet contains an invalid payload to adapt. Expected 24 bytes in the payload.");
      var version = new AdsDeviceVersion(payload.Skip(4).First(), payload.Skip(5).First(), BitConverter.ToInt16(payload.Skip(6).Take(2).Reverse().ToArray(), 0));
      var name = new AdsDeviceName(payload.Skip(8).ToArray());
      _adsDeviceInfo = new AdsDeviceInfo(version, name);
    }
  }
}