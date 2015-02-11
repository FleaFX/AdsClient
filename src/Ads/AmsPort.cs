using System;

namespace Ads {
  public struct AmsPort {
    readonly uint _value;

    // Reserved port numbers:
    public static readonly AmsPort Logger = new AmsPort(100);
    public static readonly AmsPort Eventlogger = new AmsPort(110);
    public static readonly AmsPort IO = new AmsPort(300);
    public static readonly AmsPort AdditionalTask1 = new AmsPort(301);
    public static readonly AmsPort AdditionalTask2 = new AmsPort(302);
    public static readonly AmsPort NC = new AmsPort(500);
    public static readonly AmsPort PlcRuntimeSystem1 = new AmsPort(801);
    public static readonly AmsPort PlcRuntimeSystem2 = new AmsPort(811);
    public static readonly AmsPort PlcRuntimeSystem3 = new AmsPort(821);
    public static readonly AmsPort PlcRuntimeSystem4 = new AmsPort(831);
    public static readonly AmsPort CamshaftController = new AmsPort(900);
    public static readonly AmsPort SystemService = new AmsPort(10000);
    public static readonly AmsPort Scope = new AmsPort(14000);

    /// <summary>
    /// Creates a new <see cref="AmsPort"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public AmsPort(uint value) {
      _value = value;
    }

    public static bool TryParse(string s, out AmsPort amsPort) {
      try {
        amsPort = new AmsPort(uint.Parse(s));
        return true;
      } catch (Exception) {
        amsPort = new AmsPort();
        return false;
      }
    }

    /// <summary>
    /// Converts the given <see cref="AmsPort"/> to a byte array.
    /// </summary>
    /// <param name="instance">The instance to convert.</param>
    /// <returns>A byte array.</returns>
    public static implicit operator byte[](AmsPort instance) {
      return BitConverter.GetBytes(instance._value);
    }

    /// <summary>
    /// Converts the given <see cref="AmsPort"/> to an <see cref="int"/>.
    /// </summary>
    /// <param name="instance">The instance to convert.</param>
    /// <returns>An <see cref="int"/>.</returns>
    public static implicit operator int(AmsPort instance) {
      return (int) instance._value;
    }

    public override string ToString() {
      return _value.ToString();
    }

    public bool Equals(AmsPort other) {
      return _value == other._value;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is AmsPort && Equals((AmsPort)obj);
    }

    public override int GetHashCode() {
      return (int)_value;
    }
  }
}