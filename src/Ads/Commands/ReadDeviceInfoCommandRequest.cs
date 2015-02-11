﻿using System;

namespace Ads.Commands {
  /// <summary>
  /// A request to read the name and the version number of the ADS device.
  /// </summary>
  public class ReadDeviceInfoCommandRequest {
    readonly AmsAddress _sourceAmsAddress;
    readonly AmsAddress _targetAmsAddress;

    /// <summary>
    /// Creates a new <see cref="ReadDeviceInfoCommandRequest"/>.
    /// </summary>
    /// <param name="sourceAmsAddress">The <see cref="AmsAddress"/> of the station from which the request is sent.</param>
    /// <param name="targetAmsAddress">The <see cref="AmsAddress"/> of the station for which the request is intended.</param>
    public ReadDeviceInfoCommandRequest(AmsAddress sourceAmsAddress, AmsAddress targetAmsAddress) {
      if (sourceAmsAddress == null) throw new ArgumentNullException("sourceAmsAddress");
      if (targetAmsAddress == null) throw new ArgumentNullException("targetAmsAddress");
      _sourceAmsAddress = sourceAmsAddress;
      _targetAmsAddress = targetAmsAddress;
    }

    /// <summary>
    /// Converts the given <see cref="ReadDeviceInfoCommandRequest"/> to an <see cref="AmsPacket"/> to be sent over the network.
    /// </summary>
    /// <param name="instance">The <see cref="ReadDeviceInfoCommandRequest"/> to convert.</param>
    /// <returns>An <see cref="AmsPacket"/>.</returns>
    public static implicit operator AmsPacket(ReadDeviceInfoCommandRequest instance) {
      return new AmsPacket(instance._sourceAmsAddress, instance._targetAmsAddress, CommandId.AdsReadDeviceInfo, StateFlags.AdsCommand | StateFlags.Request, new byte[0]);
    }
  }
}