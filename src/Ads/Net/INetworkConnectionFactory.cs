using System;
using System.Threading.Tasks;

namespace Ads.Net {
  public interface INetworkConnectionFactory<TConnection, in TConnectionInfo> : IDisposable {
    /// <summary>
    /// Creates a new <typeparamref name="TConnection"/>, using the given <typeparamref name="TConnectionInfo"/>.
    /// </summary>
    /// <param name="connectionInfo">Information needed to connect to another station.</param>
    /// <returns>A <see cref="Task{TConnection}"/>.</returns>
    Task<TConnection> Create(TConnectionInfo connectionInfo);
  }
}