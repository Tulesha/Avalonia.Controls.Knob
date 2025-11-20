using System;

namespace KnobControl.Avalonia.Helpers;

/// <summary>
/// Copy of methods from <see cref="Avalonia.Utilities.MathUtilities"/>.
/// </summary>
internal static class MathHelpers
{
    private const double DoubleEpsilon = 2.2204460492503131e-016;

    /// <summary>
    /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
    /// but this is faster.
    /// </summary>
    /// <param name="value"> The double to compare to 0. </param>
    public static bool IsZero(double value)
    {
        return Math.Abs(value) < 10.0 * DoubleEpsilon;
    }

    /// <summary>
    /// AreClose - Returns whether or not two doubles are "close".  That is, whether or 
    /// not they are within epsilon of each other.
    /// </summary> 
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool AreClose(double value1, double value2)
    {
        //in case they are Infinities (then epsilon check does not work)
        if (value1 == value2) return true;
        var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DoubleEpsilon;
        var delta = value1 - value2;
        return -eps < delta && eps > delta;
    }

    /// <summary>
    /// LessThan - Returns whether or not the first double is less than the second double.
    /// That is, whether or not the first is strictly less than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool LessThan(double value1, double value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// LessThanOrClose - Returns whether or not the first double is less than or close to
    /// the second double.  That is, whether or not the first is strictly less than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool LessThanOrClose(double value1, double value2)
    {
        return value1 < value2 || AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThan - Returns whether or not the first double is greater than the second double.
    /// That is, whether or not the first is strictly greater than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool GreaterThan(double value1, double value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThanOrClose - Returns whether or not the first double is greater than or close to
    /// the second double.  That is, whether or not the first is strictly greater than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool GreaterThanOrClose(double value1, double value2)
    {
        return value1 > value2 || AreClose(value1, value2);
    }
}