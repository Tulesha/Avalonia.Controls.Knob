using System;
using Avalonia;
using Avalonia.Controls;

namespace KnobControl.Avalonia;

/// <summary>
/// Base class for controls display knob ticks.
/// </summary>
public abstract partial class KnobTickBarBase : Control
{
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
    /// Get the point from center, angle radians and radius 
    /// </summary>
    protected static Point GetPoint(Point center, double angleRad, double radius)
    {
        var x = center.X + Math.Cos(angleRad) * radius;
        var y = center.Y + Math.Sin(angleRad) * radius;

        return new Point(x, y);
    }
}