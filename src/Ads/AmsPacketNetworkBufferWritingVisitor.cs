using System;

namespace Ads {
  /// <summary>
  /// <see cref="IAmsPacketVisitor"/> that writes the contents of an <see cref="AmsPacket"/> to a byte array to be sent over the network.
  /// </summary>
  public class AmsPacketNetworkBufferWritingVisitor : IAmsPacketVisitor, IAmsAddressVisitor {
    readonly Buffer _buffer;

    Action<AmsNetId> _visitNetId = amsNetId => { };
    Action<AmsPort> _visitPort = amsPort => { };

    /// <summary>
    /// Creates a new <see cref="AmsPacketNetworkBufferWritingVisitor"/>.
    /// </summary>
    public AmsPacketNetworkBufferWritingVisitor(Buffer buffer) {
      _buffer = buffer;
      _buffer.ResizeTail(32);
    }

    /// <summary>
    /// Visits the source address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="sourceAmsAddress">The source <see cref="AmsAddress"/>.</param>
    public void VisitSourceAmsAddress(AmsAddress sourceAmsAddress) {
      using (new SetSourceNetId(this) + new SetSourcePort(this)) {
        sourceAmsAddress.Accept(this);
      }
    }

    /// <summary>
    /// Visits the target address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="targetAmsAddress">The target <see cref="AmsAddress"/>.</param>
    public void VisitTargetAmsAddress(AmsAddress targetAmsAddress) {
      using (new SetTargetNetId(this) + new SetTargetPort(this)) {
        targetAmsAddress.Accept(this);
      }
    }

    /// <summary>
    /// Visits the <see cref="CommandId"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="commandId">The <see cref="CommandId"/>.</param>
    public void VisitCommandId(CommandId commandId) {
      var commandIdBuffer = BitConverter.GetBytes((ushort)commandId);
      _buffer.Set(commandIdBuffer, 16, commandIdBuffer.Length);
    }

    /// <summary>
    /// Visits the <see cref="StateFlags"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="stateFlags">The <see cref="StateFlags"/>.</param>
    public void VisitStateFlags(StateFlags stateFlags) {
      var stateFlagsBuffer = BitConverter.GetBytes((ushort)stateFlags);
      _buffer.Set(stateFlagsBuffer, 18, stateFlagsBuffer.Length);
    }

    /// <summary>
    /// Visits the payload of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="payload">The payload.</param>
    public void VisitPayload(byte[] payload) {
      var lengthBuffer = BitConverter.GetBytes(payload.Length);
      _buffer.
        Set(lengthBuffer, 20, lengthBuffer.Length).
        ResizeTail(_buffer.Length + payload.Length).
        Set(payload, 32, payload.Length);
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

    class SetSourceNetId : DisposableAction {
      public SetSourceNetId(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitNetId = amsNetId => { }; }) {
        owner._visitNetId = amsNetId => {
          var amsNetIdBuffer = (byte[])amsNetId;
          owner._buffer.Set(amsNetIdBuffer, 0, amsNetIdBuffer.Length);
        };
      }
    }

    class SetTargetNetId : DisposableAction {
      public SetTargetNetId(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitNetId = amsNetId => { }; }) {
        owner._visitNetId = amsNetId => {
          var amsNetIdBuffer = (byte[])amsNetId;
          owner._buffer.Set(amsNetIdBuffer, 8, amsNetIdBuffer.Length);
        };
      }
    }

    class SetSourcePort : DisposableAction {
      public SetSourcePort(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitPort = amsPort => { }; }) {
        owner._visitPort = amsPort => {
          var amsPortBuffer = (byte[])amsPort;
          owner._buffer.Set(amsPortBuffer, 6, amsPortBuffer.Length);
        };
      }
    }

    class SetTargetPort : DisposableAction {
      public SetTargetPort(AmsPacketNetworkBufferWritingVisitor owner)
        : base(() => { owner._visitPort = amsPort => { }; }) {
        owner._visitPort = amsPort => {
          var amsPortBuffer = (byte[])amsPort;
          owner._buffer.Set(amsPortBuffer, 14, amsPortBuffer.Length);
        };
      }
    }
  }
}