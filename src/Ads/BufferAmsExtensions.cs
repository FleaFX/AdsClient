using System;

namespace Ads {
  public static class BufferAmsExtensions {
    public static Buffer PrependAmsTcpHeader(this Buffer buffer) {
      var bufferSize = buffer.Length;
      buffer.ResizeHead(6 + buffer.Length).
        Set(new byte[] { 0, 0 }, 0, 2).                // leading zeroes of AMS/TCP header
        Set(BitConverter.GetBytes(bufferSize), 2, 4);  // length of the full packet
      return buffer;
    }
  }
}