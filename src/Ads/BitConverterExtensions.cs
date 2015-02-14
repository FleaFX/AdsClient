using System;
using System.Linq;

namespace Ads {
  public static class BitConverterExtensions {
    public static byte[] BigEndian(this byte[] buffer) {
      if (BitConverter.IsLittleEndian)
        return buffer.Reverse().ToArray();
      return buffer;
    }
  }
}