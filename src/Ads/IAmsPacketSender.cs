using System.Threading.Tasks;

namespace Ads {
  public interface IAmsPacketSender {
    /// <summary>
    /// Sends the given <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="amsPacket">The <see cref="AmsPacket"/> to send.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    Task Send(AmsPacket amsPacket);
  }
}