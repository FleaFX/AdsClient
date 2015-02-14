using System;
using System.Linq;

namespace Ads {
  public static class BufferAmsExtensions {
    public static Buffer PrependAmsTcpHeader(this Buffer buffer) {
      var bufferSize = buffer.Length;
      var bufferSizeBytes = BitConverter.GetBytes(bufferSize);
      buffer.ResizeHead(6 + buffer.Length).
        Set(new byte[] { 0, 0 }, 0, 2).                // leading zeroes of AMS/TCP header
        Set(BitConverter.IsLittleEndian ? bufferSizeBytes.Reverse().ToArray() : bufferSizeBytes, 2, 4);  // length of the full packet
      return buffer;
    }
  }
}