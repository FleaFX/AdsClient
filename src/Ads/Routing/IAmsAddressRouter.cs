using System.Net;

namespace Ads.Routing {
  /// <summary>
  /// Keeps a list of associations between an <see cref="AmsAddress"/> and an <see cref="IPAddress"/>.
  /// </summary>
  public interface IAmsAddressRouter {
    /// <summary>
    /// Returns the <see cref="IPAddress"/> that is associated with the given <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <returns>An <see cref="IPAddress"/>.</returns>
    IPAddress Resolve(AmsAddress amsAddress);

    /// <summary>
    /// Tries to resolve the <see cref="IPAddress"/> that is associated with the given <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <param name="ipAddress">If found, contains the <see cref="IPAddress"/> that is associated with the <see cref="AmsAddress"/>.</param>
    /// <returns><c>true</c> if an association was found, otherwise <c>false</c>.</returns>
    bool TryResolve(AmsAddress amsAddress, out IPAddress ipAddress);

    /// <summary>
    /// Adds an association between an <see cref="AmsAddress"/> and an <see cref="IPAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <param name="ipAddress">The <see cref="IPAddress"/>.</param>
    void Add(AmsAddress amsAddress, IPAddress ipAddress);
  }
}