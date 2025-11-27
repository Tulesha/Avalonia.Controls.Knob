using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace KnobControl.Avalonia;

/// <summary>
/// An element that is used for drawing <see cref="KnobBase"/>'s decorations.
/// </summary>
public abstract partial class KnobDecoratorBase : Control
{
    /// <summary>
    /// Get the point from center, radians angle and radius
    /// </summary>
    protected static Point GetPoint(Point center, double angleRad, double radius)
    {
        var x = center.X + Math.Cos(angleRad) * radius;
        var y = center.Y + Math.Sin(angleRad) * radius;

        return new Point(x, y);
    }

    /// <summary>
    /// Get the center of the control.
    /// </summary>
    protected Point Center => new(Bounds.Width / 2, Bounds.Height / 2);

    /// <summary>
    /// Get the radius of the control.
    /// </summary>
    protected double Radius => Math.Min(Bounds.Width, Bounds.Height) / 2;

    /// <inheritdoc />
    public abstract override void Render(DrawingContext context);
}