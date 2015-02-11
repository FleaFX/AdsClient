using System;

namespace Ads {
  /// <summary>
  /// Utility class to perform an action upon disposing the instance.
  /// </summary>
  internal class DisposableAction : IDisposable {
    readonly Action _disposeAction;

    /// <summary>
    /// Creates a new <see cref="DisposableAction"/>.
    /// </summary>
    /// <param name="disposeAction"></param>
    protected DisposableAction(Action disposeAction) {
      if (disposeAction == null) throw new ArgumentNullException("disposeAction");
      _disposeAction = disposeAction;
    }

    /// <summary>
    /// This way you can chain actions together in a single using statement.
    /// </summary>
    /// <returns>A <see cref="DisposableAction"/>.</returns>
    public static DisposableAction operator +(DisposableAction first, DisposableAction second) {
      return new DisposableAction(() => {
        first.Dispose();
        second.Dispose();
      });
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
      _disposeAction();
    }
  }
}