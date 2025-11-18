using System;

namespace Avalonia.Controls.Helpers;

internal static class PointUtils
{
    public static double Atan2FromCenter(this Point original, Point centerPoint)
    {
        return Math.Atan2(original.Y - centerPoint.Y, original.X - centerPoint.X);
    }

    public static double LengthFromPoints(this Point original, Point secondPoint)
    {
        return Math.Sqrt(Math.Pow(original.X - secondPoint.X, 2) + Math.Pow(original.Y - secondPoint.Y, 2));
    }
}