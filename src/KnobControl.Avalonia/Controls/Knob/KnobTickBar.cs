using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

/// <summary>
/// An element that is used for drawing <see cref="Knob"/>'s Ticks.
/// </summary>
public partial class KnobTickBar : Control
{
    private static Point GetPoint(Point center, double angleRad, double radius)
    {
        var x = center.X + Math.Cos(angleRad) * radius;
        var y = center.Y + Math.Sin(angleRad) * radius;

        return new Point(x, y);
    }

    /// <summary>
    /// Get the range of the values.
    /// </summary>
    protected double Range => Maximum - Minimum;

    /// <summary>
    /// Get the center of the control.
    /// </summary>
    protected Point Center => new(Bounds.Width / 2, Bounds.Height / 2);

    /// <summary>
    /// Get the radius of the control.
    /// </summary>
    protected double Radius => Math.Min(Bounds.Width, Bounds.Height) / 2;

    /// <summary>
    /// Get the start angle in radians.
    /// </summary>
    protected double StartAngleRad => StartAngle * Math.PI / 180.0;

    /// <summary>
    /// Get the sweep angle in radians.
    /// </summary>
    protected double SweepAngleRad => SweepAngle * Math.PI / 180.0;

    /// <summary>
    /// Get the end angle in radians.
    /// </summary>
    protected double EndAngleRad => StartAngleRad + SweepAngleRad;

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        var fill = Fill;
        if (fill == null)
            return;

        var center = Center;
        var radius = Radius;

        var innerRadius = radius - 8; // Length of the min max tick line
        var innerRadius2 = radius - 4; // Length of the tick line

        var pen = new ImmutablePen(Fill?.ToImmutable());

        var range = Range;
        if (MathHelpers.IsZero(range))
            return;

        var startAngleRad = StartAngleRad;
        var sweepAngleRad = SweepAngleRad;
        var endAngleRad = EndAngleRad;

        // Reduce tick interval if it is more than would be visible on the screen
        var interval = TickFrequency;

        // Minimum tick
        var minStartPoint = GetPoint(center, startAngleRad, radius);
        var minEndPoint = GetPoint(center, startAngleRad, innerRadius);
        context.DrawLine(pen, minStartPoint, minEndPoint);

        // Maximum tick
        var maxStartPoint = GetPoint(center, endAngleRad, radius);
        var maxEndPoint = GetPoint(center, endAngleRad, innerRadius);
        context.DrawLine(pen, maxStartPoint, maxEndPoint);

        // This property is rarely set so let's try to avoid the GetValue
        // caching of the mutable default value
        var ticks = Ticks ?? null;

        if (ticks?.Count > 0)
        {
            // Draw ticks using specified Ticks collection
            foreach (var tickValue in ticks)
            {
                if (MathHelpers.LessThanOrClose(tickValue, Minimum) ||
                    MathHelpers.GreaterThanOrClose(tickValue, Maximum))
                    continue;

                var valuePosition = (tickValue - Minimum) / range;
                var angleRad = startAngleRad + sweepAngleRad * valuePosition;

                var starPoint = GetPoint(center, angleRad, radius);
                var endPoint = GetPoint(center, angleRad, innerRadius2);

                context.DrawLine(pen, starPoint, endPoint);
            }
        }
        else if (interval > 0.0)
        {
            // Draw ticks using specified TickFrequency
            for (var value = Minimum; value <= Maximum; value += interval)
            {
                if (MathHelpers.LessThanOrClose(value, Minimum) ||
                    MathHelpers.GreaterThanOrClose(value, Maximum))
                    continue;

                var valuePosition = (value - Minimum) / range;
                var angleRad = startAngleRad + sweepAngleRad * valuePosition;

                var startPoint = GetPoint(center, angleRad, radius);
                var endPoint = GetPoint(center, angleRad, innerRadius2);

                context.DrawLine(pen, startPoint, endPoint);
            }
        }
    }
}