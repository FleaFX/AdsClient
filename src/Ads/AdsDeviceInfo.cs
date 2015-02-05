namespace Ads {
  /// <summary>
  /// Holds the name and version number of an ADS device.
  /// </summary>
  public struct AdsDeviceInfo {
    readonly AdsDeviceVersion _version;
    readonly AdsDeviceName _name;

    /// <summary>
    /// Creates a new <see cref="AdsDeviceInfo"/>.
    /// </summary>
    /// <param name="version">The version number of the ADS device.</param>
    /// <param name="name">The name of the ADS device.</param>
    public AdsDeviceInfo(AdsDeviceVersion version, AdsDeviceName name) {
      _version = version;
      _name = name;
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <returns>
    /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false. 
    /// </returns>
    /// <param name="obj">The object to compare with the current instance. </param><filterpriority>2</filterpriority>
    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is AdsDeviceInfo && Equals((AdsDeviceInfo) obj);
    }

    bool Equals(AdsDeviceInfo other) {
      return _version.Equals(other._version) && _name.Equals(other._name);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>
    /// A 32-bit signed integer that is the hash code for this instance.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode() {
      unchecked {
        return (_version.GetHashCode()*397) ^ _name.GetHashCode();
      }
    }
  }
}