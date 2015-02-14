using System.Threading.Tasks;

namespace Ads {
  /// <summary>
  /// An object that has implemented the ADS interface (thus being accessible via ADS) and that offers "server services", is known as an ADS device. The detailed meaning of an ADS service is specific to each ADS device.
  /// </summary>
  public interface IAdsDevice {
    /// <summary>
    /// Reads the name and the version number of the ADS device.
    /// </summary>
    /// <returns>A <see cref="AdsDeviceInfo"/>.</returns>
    Task<AdsDeviceInfo> ReadDeviceInfo();
  }
}