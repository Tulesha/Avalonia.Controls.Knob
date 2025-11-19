namespace Tulesha.Avalonia.Controls.Helpers;

internal static class ValidateHelpers
{
    /// <summary>
    /// Checks if the double value is not infinity nor NaN.
    /// </summary>
    /// <param name="value">The value.</param>
    public static bool ValidateDouble(double value)
    {
        return !double.IsInfinity(value) && !double.IsNaN(value);
    }
}