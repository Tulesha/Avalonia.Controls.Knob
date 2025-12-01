using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

/// <summary>
/// An element that is used for drawing circular arrow for <see cref="KnobEndless"/>.
/// </summary>
public partial class KnobEndlessArrow : KnobDecoratorBase
{
    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        if (Fill == null)
            return;

        var angle1 = MathHelpers.Deg2Rad(StartAngle);
        var angle2 = angle1 + MathHelpers.Deg2Rad(SweepAngle);

        var startAngleRad = Math.Min(angle1, angle2);
        var sweepAngleRad = Math.Max(angle1, angle2);

        var rect = new Rect(Bounds.Size);
        var deflatedRect = rect.Deflate(DecoratorThickness / 2);

        var center = new Point(rect.Center.X, rect.Center.Y);
        var radius = new Point(deflatedRect.Width / 2, deflatedRect.Height / 2);

        var immutableBrush = Fill.ToImmutable();
        DrawArc(context,
            center,
            radius,
            startAngleRad,
            sweepAngleRad,
            immutableBrush);
        DrawTriangularArrowHead(context,
            center,
            radius,
            startAngleRad,
            immutableBrush,
            true);
        DrawTriangularArrowHead(context,
            center,
            radius,
            sweepAngleRad,
            immutableBrush);
    }

    private void DrawArc(DrawingContext context,
        Point center,
        Point radius,
        double startAngleRad,
        double sweepAngleRad,
        IImmutableBrush brush)
    {
        var angleGap = RadToNormRad(sweepAngleRad - startAngleRad);

        var startPoint = GetRingPoint(radius, center, startAngleRad);
        var endPoint = GetRingPoint(radius, center, sweepAngleRad);

        var arcGeometry = new StreamGeometry();

        using (var arcContext = arcGeometry.Open())
        {
            arcContext.BeginFigure(startPoint, false);
            arcContext.ArcTo(
                endPoint,
                new Size(radius.X, radius.Y),
                rotationAngle: angleGap,
                isLargeArc: angleGap >= Math.PI,
                SweepDirection.Clockwise);
            arcContext.EndFigure(false);
        }

        var pen = new ImmutablePen(brush, DecoratorThickness);
        context.DrawGeometry(null, pen, arcGeometry);
    }

    private void DrawTriangularArrowHead(
        DrawingContext context,
        Point center,
        Point radius,
        double rad,
        IBrush brush,
        bool reverse = false)
    {
        // Wing length (half base of triangle)
        var wingHalf = ArrowSize * 0.5; // Adjust for visual balance

        // Tangent vector at tip: perpendicular to radial vector
        var tangentX = -Math.Sin(rad); // dx/dθ = -sin(θ)
        var tangentY = Math.Cos(rad); // dy/dθ = cos(θ)

        // If reverse, flip tangent direction
        if (reverse)
        {
            tangentX = -tangentX;
            tangentY = -tangentY;
        }

        // Perpendicular to tangent = radial direction (for base)
        var perpX = Math.Cos(rad);
        var perpY = Math.Sin(rad);

        var shiftX = -tangentX * wingHalf;
        var shiftY = -tangentY * wingHalf;

        // Tip point on arc
        var tipX = center.X + Math.Cos(rad) * radius.X - shiftX;
        var tipY = center.Y + Math.Sin(rad) * radius.Y - shiftY;
        var tip = new Point(tipX, tipY);

        // Wing points: offset perpendicular to tangent (radial direction)
        var wing1 = new Point(
            tipX + perpX * wingHalf + shiftX,
            tipY + perpY * wingHalf + shiftY);

        var wing2 = new Point(
            tipX - perpX * wingHalf + shiftX,
            tipY - perpY * wingHalf + shiftY);

        // Create triangle geometry: tip -> wing1 -> wing2 -> tip
        var triangle = new StreamGeometry();
        using (var ctx = triangle.Open())
        {
            ctx.BeginFigure(tip, true); // Close figure
            ctx.LineTo(wing1);
            ctx.LineTo(wing2);
            ctx.LineTo(tip); // Close triangle
        }

        // Fill the triangle
        context.DrawGeometry(brush, null, triangle);
    }

    private static double RadToNormRad(double inAngle) => (inAngle % (Math.PI * 2) + Math.PI * 2) % (Math.PI * 2);

    private static Point GetRingPoint(Point radius, Point center, double angle) =>
        new(radius.X * Math.Cos(angle) + center.X, radius.Y * Math.Sin(angle) + center.Y);
}