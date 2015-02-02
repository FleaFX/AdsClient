using System;
using AdsClient.Properties;

namespace AdsClient {
  public class AmsAddress {
    readonly AmsNetId _amsNetId;
    readonly AmsPort _amsPort;

    /// <summary>
    /// Creates a new <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/>.</param>
    /// <param name="amsPort">The <see cref="AmsPort"/>.</param>
    public AmsAddress(AmsNetId amsNetId, AmsPort amsPort) {
      if (AmsNetId.Empty.Equals(amsNetId)) throw new ArgumentNullException("amsNetId");
      _amsNetId = amsNetId;
      _amsPort = amsPort;
    }

    /// <summary>
    /// Converts the string representation of an AMS address (consisting of an <see cref="AmsNetId"/> and <see cref="AmsPort"/>) to its typed <see cref="AmsAddress"/> equivalent.
    /// </summary>
    /// <param name="s">A string containing an AMS address to convert.</param>
    /// <returns></returns>
    public static AmsAddress Parse(string s) {
      var parts = s.Split(':');
      if (parts.Length != 2) throw new FormatException(Resources.AmsAddressParseInvalidFormat);
      AmsNetId amsNetId;
      AmsPort amsPort;
      if (!AmsNetId.TryParse(parts[0], out amsNetId) || !AmsPort.TryParse(parts[1], out amsPort)) throw new FormatException(Resources.AmsAddressParseInvalidFormat);
      return new AmsAddress(amsNetId, amsPort);
    }

    public override string ToString() {
      return string.Format("{0}:{1}", _amsNetId, _amsPort);
    }

    protected bool Equals(AmsAddress other) {
      return _amsNetId.Equals(other._amsNetId) && _amsPort.Equals(other._amsPort);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AmsAddress)obj);
    }

    public override int GetHashCode() {
      unchecked {
        return (_amsNetId.GetHashCode() * 397) ^ _amsPort.GetHashCode();
      }
    }
  }
}