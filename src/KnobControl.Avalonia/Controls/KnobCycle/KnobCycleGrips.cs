using System;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace KnobControl.Avalonia;

/// <summary>
/// An element that is used for drawing <see cref="KnobCycle"/>'s Grips.
/// </summary>
public partial class KnobCycleGrips : KnobDecoratorBase
{
    /// <summary>
    /// Get the start angle in degrees.
    /// </summary>
    protected double StartAngle => 0.0;

    /// <summary>
    /// Get the sweep angle in degrees.
    /// </summary>
    protected double SweepAngle => 360.0;

    /// <summary>
    /// Get the end angle in degrees.
    /// </summary>
    protected double EndAngle => StartAngle + SweepAngle;

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

        var fillBrush = Fill?.ToImmutable();
        var pen = new ImmutablePen(fillBrush, DecoratorThickness);

        var count = GripsCount;

        if (!(count > 0.0))
            return;

        var step = EndAngleRad / count;
        var deltaAngleRad = GripsSize / radius;

        for (var value = step; value <= EndAngleRad; value += step)
        {
            var startPoint = GetPoint(center, value - deltaAngleRad / 2, radius);
            var endPoint = GetPoint(center, value + deltaAngleRad / 2, radius);

            context.DrawLine(pen, startPoint, endPoint);
        }
    }
}