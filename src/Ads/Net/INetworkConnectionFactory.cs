using System.Threading.Tasks;

namespace Ads.Net {
  /// <summary>
  /// Creates <see cref="INetworkConnection">network connections</see>.
  /// </summary>
  public interface INetworkConnectionFactory {
    /// <summary>
    /// Creates a new <see cref="INetworkConnection"/>.
    /// </summary>
    /// <param name="target">The target station to connect to.</param>
    /// <returns>A <see cref="INetworkConnection"/>.</returns>
    Task<INetworkConnection> Create(AmsAddress target);
  }
}