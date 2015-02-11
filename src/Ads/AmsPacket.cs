using System;
using System.Linq;
using Ads.Properties;

namespace Ads {
  /// <summary>
  /// Packet that is sent to an ADS device.
  /// </summary>
  public class AmsPacket {
    readonly AmsAddress _source;
    readonly AmsAddress _target;
    readonly CommandId _commandId;
    readonly StateFlags _stateFlags;
    readonly byte[] _payload;

    /// <summary>
    /// Creates a new <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="source">The <see cref="AmsAddress"/> of the station from which the packet was sent.</param>
    /// <param name="target">The <see cref="AmsAddress"/> of the station for which the packet is intended.</param>
    /// <param name="commandId">The ID of the command that is being executed.</param>
    /// <param name="stateFlags">Sets various flags containing information about the packet.</param>
    /// <param name="payload">Contains parameters of the command being executed.</param>
    public AmsPacket(AmsAddress source, AmsAddress target, CommandId commandId, StateFlags stateFlags, byte[] payload) {
      if (source == null) throw new ArgumentNullException("source");
      if (target == null) throw new ArgumentNullException("target");
      if (!Enum.IsDefined(typeof(CommandId), commandId)) throw new ArgumentException(Resources.AmsHeaderInvalidCommandId, "commandId");
      if (stateFlags.UsesReservedBits()) throw new ArgumentException(Resources.AmsHeaderInvalidStateFlags, "stateFlags");
      if (payload == null) throw new ArgumentNullException("payload");
      _source = source;
      _target = target;
      _commandId = commandId;
      _stateFlags = stateFlags;
      _payload = payload;
    }

    /// <summary>
    /// Accepts a <see cref="IAmsPacketVisitor"/> to inspect the data within the packet.
    /// </summary>
    /// <param name="visitor">The <see cref="IAmsPacketVisitor"/>.</param>
    public void Accept(IAmsPacketVisitor visitor) {
      visitor.VisitSourceAmsAddress(_source);
      visitor.VisitTargetAmsAddress(_target);
      visitor.VisitCommandId(_commandId);
      visitor.VisitStateFlags(_stateFlags);
      visitor.VisitPayload(_payload);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == this.GetType() && Equals((AmsPacket) obj);
    }

    protected bool Equals(AmsPacket other) {
      return Equals(_source, other._source) && Equals(_target, other._target) && _commandId == other._commandId && _stateFlags == other._stateFlags && _payload.SequenceEqual(other._payload);
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    /// <returns>
    /// A hash code for the current object.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode() {
      unchecked {
        int hashCode = (_source != null ? _source.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (_target != null ? _target.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (int) _commandId;
        hashCode = (hashCode*397) ^ (int) _stateFlags;
        hashCode = (hashCode*397) ^ (_payload != null ? _payload.GetHashCode() : 0);
        return hashCode;
      }
    }
  }
}