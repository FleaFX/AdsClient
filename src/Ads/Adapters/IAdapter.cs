namespace Ads.Adapters {
  /// <summary>
  /// Makes adaptations to an incoming <typeparamref name="TIn"/> to produce a <typeparamref name="TOut"/>.
  /// </summary>
  /// <typeparam name="TIn">The type of the incoming object.</typeparam>
  /// <typeparam name="TOut">The type of the outgoing object.</typeparam>
  public interface IAdapter<in TIn, out TOut> {
    /// <summary>
    /// Adapts a <typeparamref name="TIn"/> to produce a <typeparamref name="TOut"/>.
    /// </summary>
    /// <param name="subject">The object being adapter.</param>
    /// <returns>A <typeparamref name="TOut"/>.</returns>
    TOut Adapt(TIn subject);
  }
}