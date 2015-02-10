namespace Ads {
  /// <summary>
  /// Visits the fields of an <see cref="AmsPacket"/>.
  /// </summary>
  public interface IAmsPacketVisitor {
    /// <summary>
    /// Visits the source address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="sourceAmsAddress">The source <see cref="AmsAddress"/>.</param>
    void VisitSourceAmsAddress(AmsAddress sourceAmsAddress);

    /// <summary>
    /// Visits the target address of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="targetAmsAddress">The target <see cref="AmsAddress"/>.</param>
    void VisitTargetAmsAddress(AmsAddress targetAmsAddress);

    /// <summary>
    /// Visits the <see cref="CommandId"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="commandId">The <see cref="CommandId"/>.</param>
    void VisitCommandId(CommandId commandId);

    /// <summary>
    /// Visits the <see cref="StateFlags"/> of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="stateFlags">The <see cref="StateFlags"/>.</param>
    void VisitStateFlags(StateFlags stateFlags);

    /// <summary>
    /// Visits the payload of a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="payload">The payload.</param>
    void VisitPayload(byte[] payload);
  }
}