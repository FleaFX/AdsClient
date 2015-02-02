namespace AdsClient {
  public static class StateFlagsExtensions {
    /// <summary>
    /// Indicates whether a reserved bit is used. See http://infosys.beckhoff.com/content/1033/tcadsamsspec/html/tcadsamsspec_amsheader.htm?id=18623#State%20Flags for more information.
    /// </summary>
    /// <param name="stateFlags">The <see cref="StateFlags"/>.</param>
    /// <returns><c>true</c> if the <see cref="StateFlags"/> uses any of the reserved bits, otherwise <c>false</c>.</returns>
    public static bool UsesReservedBits(this StateFlags stateFlags) {
      return stateFlags.HasFlag(StateFlags.ReservedBit4) ||
             stateFlags.HasFlag(StateFlags.ReservedBit5) ||
             stateFlags.HasFlag(StateFlags.ReservedBit6);
    }
  }
}