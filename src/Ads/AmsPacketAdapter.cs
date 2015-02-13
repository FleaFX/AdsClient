using System;
using System.Linq;

namespace Ads {
  public class AmsPacketAdapter : IAdapter<byte[], AmsPacket> {
    /// <summary>
    /// Adapts a <see><cref>byte[]</cref></see> to produce a <see cref="AmsPacket"/>.
    /// </summary>
    /// <param name="subject">The object being adapter.</param>
    /// <returns>A <see cref="AmsPacket"/>.</returns>
    public AmsPacket Adapt(byte[] subject) {
      // TODO: find out if bytes are big endian or little endian
      var payloadLength = BitConverter.ToInt32(subject.Skip(20).Take(4).Reverse().ToArray(), 0);
      return new AmsPacket(
        new AmsAddress(new AmsNetId(subject.Skip(8).Take(6).ToArray()), new AmsPort(BitConverter.ToUInt16(subject.Skip(14).Take(2).ToArray(), 0))),
        new AmsAddress(new AmsNetId(subject.Take(6).ToArray()), new AmsPort(BitConverter.ToUInt16(subject.Skip(6).Take(2).ToArray(), 0))),
        (CommandId)BitConverter.ToUInt16(subject.Skip(16).Take(2).Reverse().ToArray(), 0),
        (StateFlags)BitConverter.ToUInt16(subject.Skip(18).Take(2).Reverse().ToArray(), 0),
        subject.Skip(32).Take(payloadLength).ToArray());
    }
  }
}