using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ads.Net {
  public interface ITcpClient {
    /// <summary>
    /// Connects the client to a remote TCP host using the specified IP address and port number as an asynchronous operation.
    /// </summary>
    /// <param name="address">The <see cref="IPAddress"/> of the host to which you intend to connect.</param>
    /// <param name="port">The port number to which you intend to connect.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task ConnectAsync(IPAddress address, int port);

    /// <summary>
    /// Returns the <see cref="NetworkStream"/> used to send and receive data.
    /// </summary>
    /// <returns>The underlying NetworkStream.</returns>
    Stream GetStream();
  }
}