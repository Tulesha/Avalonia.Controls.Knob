namespace Avalonia.Controls.Helpers;

internal static class CoerceHelpers
{
    /// <summary>
    /// Coerce for <see cref="Knob.StartAngleProperty"/> and <see cref="KnobTickBar.StartAngleProperty"/> properties.
    /// </summary>
    /// <param name="value">Value from coerce.</param>
    /// <param name="defaultValue">Value which will be returned if coerce doesn't pass.</param>
    /// <returns>Valid value.</returns>
    public static double CoerceStartAngle(double value, double defaultValue)
    {
        return ValidateHelpers.ValidateDouble(value) &&
               value >= -360 &&
               value <= 360
            ? value
            : defaultValue;
    }

    /// <summary>
    /// Coerce for <see cref="Knob.SweepAngleProperty"/> and <see cref="KnobTickBar.SweepAngleProperty"/> properties.
    /// </summary>
    /// <param name="value">Value from coerce.</param>
    /// <param name="defaultValue">Value which will be returned if coerce doesn't pass.</param>
    /// <returns>Valid value.</returns>
    public static double CoerceSweepAngle(double value, double defaultValue)
    {
        return ValidateHelpers.ValidateDouble(value) &&
               value > 0 &&
               value < 360
            ? value
            : defaultValue;
    }
}