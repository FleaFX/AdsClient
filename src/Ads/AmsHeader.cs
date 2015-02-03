using System;
using Ads.Properties;

namespace Ads {
  public class AmsHeader {
    readonly AmsAddress _target;
    readonly AmsAddress _source;
    readonly CommandId _commandId;
    readonly StateFlags _stateFlags;

    public AmsHeader(AmsAddress target, AmsAddress source, CommandId commandId, StateFlags stateFlags) {
      if (target == null) throw new ArgumentNullException("target");
      if (source == null) throw new ArgumentNullException("source");
      if (!Enum.IsDefined(typeof(CommandId), commandId)) throw new ArgumentException(Resources.AmsHeaderInvalidCommandId, "commandId");
      if (stateFlags.UsesReservedBits()) throw new ArgumentException(Resources.AmsHeaderInvalidStateFlags, "stateFlags");
      _target = target;
      _source = source;
      _commandId = commandId;
      _stateFlags = stateFlags;
    }
  }
}