using System;
using System.Linq;
using System.Text.RegularExpressions;
using Ads.Properties;

namespace Ads {
  /// <summary>
  /// Extension to the TCP/IP address that identifies a TwinCAT message router.
  /// </summary>
  public struct AmsNetId {
    readonly byte[] _value;

    static readonly Regex ParseFormat = new Regex(@"^(?<b1>\d{1,3})\.(?<b2>\d{1,3})\.(?<b3>\d{1,3})\.(?<b4>\d{1,3})\.(?<b5>\d{1,3})\.(?<b6>\d{1,3})$");

    /// <summary>
    /// Represents an empty <see cref="AmsNetId"/>.
    /// </summary>
    public static AmsNetId Empty = new AmsNetId();

    /// <summary>
    /// Creates a new <see cref="AmsNetId"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public AmsNetId(params byte[] value) {
      if (value != null && value.Length != 0 && value.Length != 6) throw new ArgumentOutOfRangeException("value", Resources.AmsNetIdValueOutOfRange);
      _value = value == null || value.SequenceEqual(new byte[0]) ? Empty._value : value;
    }

    /// <summary>
    /// Attempts to parse a string representation of an AMS Net Id to its typed <see cref="AmsNetId"/> equivalent.
    /// </summary>
    /// <param name="s">A string containing an AMS Net Id to convert.</param>
    /// <param name="amsNetId">If successful, contains the resulting <see cref="AmsNetId"/>.</param>
    /// <returns><c>true</c> if the parsing was succesful, otherwise <c>false</c>.</returns>
    public static bool TryParse(string s, out AmsNetId amsNetId) {
      if (s == null) throw new ArgumentNullException("s");
      amsNetId = Empty;
      if (!ParseFormat.IsMatch(s)) return false;
      var amsNetIdMatch = ParseFormat.Match(s);
      var amsNetIdParts = new[] {
        amsNetIdMatch.Groups["b1"].Value, amsNetIdMatch.Groups["b2"].Value, amsNetIdMatch.Groups["b3"].Value,
        amsNetIdMatch.Groups["b4"].Value, amsNetIdMatch.Groups["b5"].Value, amsNetIdMatch.Groups["b6"].Value
      };
      if (!amsNetIdParts.All(val => {
        var part = int.Parse(val);
        return part >= byte.MinValue && part <= byte.MaxValue;
      })) return false;
      amsNetId = new AmsNetId(amsNetIdParts.Select(byte.Parse).ToArray());
      return true;
    }

    /// <summary>
    /// Converts the string representation of an AMS Net Id to its typed <see cref="AmsNetId"/> equivalent.
    /// </summary>
    /// <param name="s">A string containing an AMS Net Id to convert.</param>
    /// <returns></returns>
    public static AmsNetId Parse(string s) {
      if (s == null) throw new ArgumentNullException("s");
      if (!ParseFormat.IsMatch(s)) throw new FormatException(Resources.AmsNetIdParseInvalidFormat);
      var amsNetId = ParseFormat.Match(s);
      var amsNetIdParts = new[] {
        amsNetId.Groups["b1"].Value, amsNetId.Groups["b2"].Value, amsNetId.Groups["b3"].Value,
        amsNetId.Groups["b4"].Value, amsNetId.Groups["b5"].Value, amsNetId.Groups["b6"].Value
      };
      if (!amsNetIdParts.All(val => {
        var part = int.Parse(val);
        return part >= byte.MinValue && part <= byte.MaxValue;
      })) throw new OverflowException(Resources.AmsNetIdParseOverflow);
      return new AmsNetId(amsNetIdParts.Select(byte.Parse).ToArray());
    }

    public override string ToString() {
      return _value.Select(_ => _.ToString()).Aggregate((f, s) => string.Format("{0}.{1}", f, s));
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is AmsNetId && Equals((AmsNetId)obj);
    }

    public bool Equals(AmsNetId other) {
      if (_value == null) return other._value == null;
      return _value.SequenceEqual(other._value);
    }

    public override int GetHashCode() {
      return (_value != null ? _value.Select(_ => _.GetHashCode()).Aggregate((f, s) => f ^ s) : 0);
    }
  }
}