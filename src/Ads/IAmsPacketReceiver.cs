using System.Threading.Tasks;

namespace Ads {
  public interface IAmsPacketReceiver {
    /// <summary>
    /// Receives an <see cref="AmsPacket"/> when it is available.
    /// </summary>
    /// <returns>An awaitable <see cref="Task{AmsPacket}"/>.</returns>
    Task<AmsPacket> Receive();
  }
}