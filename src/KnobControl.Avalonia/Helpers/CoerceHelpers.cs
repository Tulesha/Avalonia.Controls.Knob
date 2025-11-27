namespace KnobControl.Avalonia.Helpers;

internal static class CoerceHelpers
{
    /// <summary>
    /// Coerce for <see cref="KnobBase.StartAngleProperty"/> and <see cref="KnobDecoratorBase.StartAngleProperty"/> properties.
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
    /// Coerce for <see cref="KnobBase.SweepAngleProperty"/> and <see cref="KnobDecoratorBase.SweepAngleProperty"/> properties.
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