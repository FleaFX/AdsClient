using System.Net;

namespace Ads.Routing {
  /// <summary>
  /// Keeps associations between <see cref="AmsAddress"/> and <see cref="IPAddress"/> in memory.
  /// </summary>
  public class InMemoryAmsAddressRouter : IAmsAddressRouter {
    /// <summary>
    /// Creates a new <see cref="InMemoryAmsAddressRouter"/>.
    /// </summary>
    public InMemoryAmsAddressRouter() {
      
    }

    /// <summary>
    /// Returns the <see cref="IPAddress"/> that is associated with the given <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <returns>An <see cref="IPAddress"/>.</returns>
    public IPAddress Resolve(AmsAddress amsAddress) {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// Tries to resolve the <see cref="IPAddress"/> that is associated with the given <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <param name="ipAddress">If found, contains the <see cref="IPAddress"/> that is associated with the <see cref="AmsAddress"/>.</param>
    /// <returns><c>true</c> if an association was found, otherwise <c>false</c>.</returns>
    public bool TryResolve(AmsAddress amsAddress, out IPAddress ipAddress) {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// Adds an association between an <see cref="AmsAddress"/> and an <see cref="IPAddress"/>.
    /// </summary>
    /// <param name="amsAddress">The <see cref="AmsAddress"/>.</param>
    /// <param name="ipAddress">The <see cref="IPAddress"/>.</param>
    public void Add(AmsAddress amsAddress, IPAddress ipAddress) {
      throw new System.NotImplementedException();
    }
  }
}