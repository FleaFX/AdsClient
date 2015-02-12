using System;
using System.Threading.Tasks;

namespace Ads.Net {
  public interface ITcpConnection : IDisposable {
    Task WriteAsync(byte[] buffer, int offset, int count);
  }
}