using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ads.Routing {
  /// <summary>
  /// Keeps associations between <see cref="AmsAddress"/> and <see cref="IPAddress"/> in memory.
  /// </summary>
  public class InMemoryAmsAddressRouter : IAmsAddressRouter {
    readonly IDictionary<AmsNetId, IPAddress> _routes;

    /// <summary>
    /// Creates a new <see cref="InMemoryAmsAddressRouter"/>.
    /// </summary>
    public InMemoryAmsAddressRouter() {
      _routes = new Dictionary<AmsNetId, IPAddress>();
    }

    /// <summary>
    /// Returns the <see cref="IPAddress"/> that is associated with the given <see cref="AmsNetId"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/>.</param>
    /// <returns>An <see cref="IPAddress"/>.</returns>
    public async Task<IPAddress> ResolveAsync(AmsNetId amsNetId) {
      IPAddress ipAddress;
      if (!_routes.TryGetValue(amsNetId, out ipAddress)) throw new UnknownAmsAddressException();
      return ipAddress;
    }

    /// <summary>
    /// Adds an association between an <see cref="AmsNetId"/> and an <see cref="IPAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/>.</param>
    /// <param name="ipAddress">The <see cref="IPAddress"/>.</param>
    public void Add(AmsNetId amsNetId, IPAddress ipAddress) {
      if (AmsNetId.Empty.Equals(amsNetId)) throw new ArgumentNullException("amsNetId");
      if (IPAddress.Any.Equals(ipAddress)) throw new ArgumentOutOfRangeException("ipAddress");
      _routes[amsNetId] = ipAddress;
    }
  }
}