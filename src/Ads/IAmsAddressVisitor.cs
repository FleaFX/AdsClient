namespace Ads {
  /// <summary>
  /// Visits the fields of an <see cref="AmsAddress"/>.
  /// </summary>
  public interface IAmsAddressVisitor {
    /// <summary>
    /// Visits the <see cref="AmsNetId"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsNetId">The <see cref="AmsNetId"/> to visit.</param>
    void Visit(AmsNetId amsNetId);

    /// <summary>
    /// Visits the <see cref="AmsPort"/> portion of the <see cref="AmsAddress"/>.
    /// </summary>
    /// <param name="amsPort">The <see cref="AmsPort"/> to visit.</param>
    void Visit(AmsPort amsPort);
  }
}