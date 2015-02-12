using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ads.Net {
  public interface INetworkConnection {
    /// <summary>
    /// Sends the given bytes to the other end of the connection.
    /// </summary>
    /// <param name="buffer">The bytes to send.</param>
    /// <param name="offset">The offset in the given collection from where to begin copying to the stream.</param>
    /// <param name="count">The number of bytes to copy to the stream.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task WriteAsync(IEnumerable<byte> buffer, int offset, int count);
  }
}