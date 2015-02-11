using System;
using System.Linq;

namespace Ads {
  /// <summary>
  /// <see cref="IAmsPacketVisitor"/> that writes the contents of an <see cref="AmsPacket"/> to a byte array to be sent over the network.
  /// </summary>
  public class AmsPacketNetworkBufferWritingVisitor : IAmsPacketVisitor, IAmsAddressVisitor {
    readonly byte[] _headerBuffer;
    byte[] _payloadBuffer;

    Action<AmsNetId> _visitNetId = amsNetId => { };
    Action<AmsPort> _visitPort = amsPort => { };

    /// <summary>
    /// Creates a new <see cref="AmsPacketNetworkBufferWritingVisitor"/>.
    /// </summary>
    public AmsPacketNetworkBufferWritingVisitor() {
      _headerBuffer = new byte[32];
      _payloadBuffer = new byte[0];
    }

    /// <summary>
    /// Visits the source address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="sourceAmsAddress">The source <see cref="AmsAddress"/>.</param>
    public void VisitSourceAmsAddress(AmsAddress sourceAmsAddress) {
      using (new SetSourceNetId(this)) {
        using (new SetSourcePort(this)) {
          sourceAmsAddress.Accept(this);
        }
      }
    }

    /// <summary>
    /// Visits the target address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="targetAmsAddress">The target <see cref="AmsAddress"/>.</param>
    public void VisitTargetAmsAddress(AmsAddress targetAmsAddress) {
      using (new SetTargetNetId(this)) {
        using (new SetTargetPort(this)) {
          targetAmsAddress.Accept(this);
        }
      }
    }

    /// <summary>
    /// Visits the <see cref="CommandId"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="commandId">The <see cref="CommandId"/>.</param>
    public void VisitCommandId(CommandId commandId) {
      var commandIdBuffer = BitConverter.GetBytes((ushort)commandId);
      Array.Copy(commandIdBuffer, 0, _headerBuffer, 16, commandIdBuffer.Length);
    }

    /// <summary>
    /// Visits the <see cref="StateFlags"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="stateFlags">The <see cref="StateFlags"/>.</param>
    public void VisitStateFlags(StateFlags stateFlags) {
      var stateFlagsBuffer = BitConverter.GetBytes((ushort)stateFlags);
      Array.Copy(stateFlagsBuffer, 0, _headerBuffer, 18, stateFlagsBuffer.Length);
    }

    /// <summary>
    /// Visits the payload of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="payload">The payload.</param>
    public void VisitPayload(byte[] payload) {
      var lengthBuffer = BitConverter.GetBytes(payload.Length);
      Array.Copy(lengthBuffer, 0, _headerBuffer, 20, lengthBuffer.Length);
      _payloadBuffer = (byte[])payload.Clone();
    }

    /// <summary>
    /// Visits the <see cref="AmsNetId"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/> to visit.</param>
    public void Visit(AmsNetId amsNetId) {
      _visitNetId(amsNetId);
    }

    /// <summary>
    /// Visits the <see cref="AmsPort"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsPort">The <see cref="AmsPort"/> to visit.</param>
    public void Visit(AmsPort amsPort) {
      _visitPort(amsPort);
    }

    /// <summary>
    /// Gets the buffer at its current state.
    /// </summary>
    /// <returns>A byte array.</returns>
    public byte[] GetBuffer() {
      return _headerBuffer.Concat(_payloadBuffer).ToArray();
    }

    class SetSourceNetId : DisposableAction {
      public SetSourceNetId(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitNetId = amsNetId => { }; }) {
        owner._visitNetId = amsNetId => {
          var amsNetIdBuffer = (byte[])amsNetId;
          Array.Copy(amsNetIdBuffer, 0, owner._headerBuffer, 0, amsNetIdBuffer.Length);
        };
      }
    }

    class SetTargetNetId : DisposableAction {
      public SetTargetNetId(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitNetId = amsNetId => { }; }) {
        owner._visitNetId = amsNetId => {
          var amsNetIdBuffer = (byte[])amsNetId;
          Array.Copy(amsNetIdBuffer, 0, owner._headerBuffer, 8, amsNetIdBuffer.Length);
        };
      }
    }

    class SetSourcePort : DisposableAction {
      public SetSourcePort(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitPort = amsPort => { }; }) {
        owner._visitPort = amsPort => {
          var amsPortBuffer = (byte[])amsPort;
          Array.Copy(amsPortBuffer, 0, owner._headerBuffer, 6, amsPortBuffer.Length);
        };
      }
    }

    class SetTargetPort : DisposableAction {
      public SetTargetPort(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitPort = amsPort => { }; }) {
        owner._visitPort = amsPort => {
          var amsPortBuffer = (byte[])amsPort;
          Array.Copy(amsPortBuffer, 0, owner._headerBuffer, 14, amsPortBuffer.Length);
        };
      }
    }
  }
}