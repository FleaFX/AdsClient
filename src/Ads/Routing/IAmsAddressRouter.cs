using System.Net;
using System.Threading.Tasks;

namespace Ads.Routing {
  /// <summary>
  /// Keeps a list of associations between an <see cref="AmsAddress"/> and an <see cref="IPAddress"/>.
  /// </summary>
  public interface IAmsAddressRouter {
    /// <summary>
    /// Returns the <see cref="IPAddress"/> that is associated with the given <see cref="AmsNetId"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/>.</param>
    /// <returns>An <see cref="IPAddress"/>.</returns>
    Task<IPAddress> ResolveAsync(AmsNetId amsNetId);

    /// <summary>
    /// Adds an association between an <see cref="AmsNetId"/> and an <see cref="IPAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/>.</param>
    /// <param name="ipAddress">The <see cref="IPAddress"/>.</param>
    void Add(AmsNetId amsNetId, IPAddress ipAddress);
  }
}