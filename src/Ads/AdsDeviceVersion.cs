namespace Ads {
  /// <summary>
  /// Holds the version number (major, minor, build) of an ADS device
  /// </summary>
  public struct AdsDeviceVersion {
    readonly byte _major;
    readonly byte _minor;
    readonly short _build;

    /// <summary>
    /// Creates a new <see cref="AdsDeviceVersion"/>.
    /// </summary>
    /// <param name="major">Major version number.</param>
    /// <param name="minor">Minor version number</param>
    /// <param name="build">Build number.</param>
    public AdsDeviceVersion(byte major, byte minor, short build) {
      _major = major;
      _minor = minor;
      _build = build;
    }

    /// <summary>
    /// Returns the dotted notation of the version number.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> containing the dotted notation of the version number.
    /// </returns>
    public override string ToString() {
      return string.Format("{0}.{1}.{2}", _major, _minor, _build);
    }
  }
}