using System;
using System.Text;
using Ads.Properties;

namespace Ads {
  /// <summary>
  /// Holds the name of an ADS device.
  /// </summary>
  public struct AdsDeviceName {
    readonly string _value;

    /// <summary>
    /// Creates a new <see cref="AdsDeviceName"/>.
    /// </summary>
    /// <param name="value">The name of the device</param>
    public AdsDeviceName(string value) {
      if (value == null) throw new ArgumentNullException("value");
      if (Encoding.UTF8.GetBytes(value).Length > 16) throw new OverflowException(Resources.DeviceNameOverflow);
      _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="AdsDeviceName"/>.
    /// </summary>
    /// <remarks>Use this constructor when retrieving the name via ADS Read Device Info.</remarks>
    /// <param name="value">A byte array representing the name of the device.</param>
    public AdsDeviceName(byte[] value) {
      if (value == null) throw new ArgumentNullException("value");
      if (value.Length > 16) throw new OverflowException(Resources.DeviceNameOverflow);
      _value = Encoding.UTF8.GetString(value);
    }

    /// <summary>
    /// Returns the fully qualified type name of this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> containing a fully qualified type name.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString() {
      return _value;
    }
  }
}