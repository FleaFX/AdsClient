using System;
using System.Threading.Tasks;
using Ads.Adapters;
using Ads.Commands;
using Ads.Net;

namespace Ads {
  public class AdsDevice : IAdsDevice {
    readonly INetworkConnectionFactory _networkConnectionFactory;
    readonly AmsAddress _sourceAddres;
    readonly AmsAddress _targetAddress;

    /// <summary>
    /// Creates a new <see cref="AdsDevice"/>.
    /// </summary>
    /// <param name="networkConnectionFactory">The <see cref="INetworkConnectionFactory"/>.</param>
    /// <param name="sourceAddres">The <see cref="AmsAddress"/> of the current station.</param>
    /// <param name="targetAddress">The <see cref="AmsAddress"/> of the target station.</param>
    public AdsDevice(INetworkConnectionFactory networkConnectionFactory, AmsAddress sourceAddres, AmsAddress targetAddress) {
      if (networkConnectionFactory == null) throw new ArgumentNullException("networkConnectionFactory");
      if (sourceAddres == null) throw new ArgumentNullException("sourceAddres");
      if (targetAddress == null) throw new ArgumentNullException("targetAddress");
      _networkConnectionFactory = networkConnectionFactory;
      _sourceAddres = sourceAddres;
      _targetAddress = targetAddress;
    }

    /// <summary>
    /// Reads the name and the version number of the ADS device.
    /// </summary>
    /// <returns>A <see cref="AdsDeviceInfo"/>.</returns>
    public async Task<AdsDeviceInfo> ReadDeviceInfo() {
      using (var connection = await _networkConnectionFactory.Create(_targetAddress)) {
        await new AmsPacketSender(connection).Send(new ReadDeviceInfoCommand(_sourceAddres, _targetAddress));
        return new AdsDeviceInfoAdapter().Adapt(await new AmsPacketReceiver(connection, new AmsPacketAdapter()).Receive());
      }
    }
  }
}