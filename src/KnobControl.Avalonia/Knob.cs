using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

/// <summary>
/// Knob control
/// </summary>
[PseudoClasses(":pressed")]
public partial class Knob : RangeBase
{
    private const double Tolerance = 0.0001;
    private const double MinDraggingChangesValue = 2.0;

    private bool _isFocusEngaged;
    private bool _isDragging;
    private bool _isCaptured;

    private Point _startDragPoint;

    /// <inheritdoc />
    public Knob()
    {
        UpdateRange();
    }

    /// <summary>
    /// Get the center of the control.
    /// </summary>
    protected Point Center => new(Bounds.Width / 2, Bounds.Height / 2);

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
    protected override void UpdateDataValidation(
        AvaloniaProperty property,
        BindingValueType state,
        Exception? error)
    {
        if (property == ValueProperty)
        {
            DataValidationErrors.SetError(this, error);
        }
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        RecalculateAngles();
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerPressedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer pressed event args</param>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (!IsEnabled)
            return;

        if (!e.Properties.IsLeftButtonPressed)
            return;

        var center = Center;
        _isDragging = false;
        _isCaptured = true;
        _startDragPoint = e.GetPosition(this);
        _startDragPoint.Atan2FromCenter(center);

        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerMovedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer moved event args</param>
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (!IsEnabled)
            return;

        if (!_isCaptured)
            return;

        var currentPoint = e.GetPosition(this);

        // Threshold to detect actual dragging
        if (!(currentPoint.LengthFromPoints(_startDragPoint) > MinDraggingChangesValue))
            return;

        _isDragging = true;
        Snap(currentPoint);

        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerReleasedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer released event args</param>
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (!IsEnabled)
            return;

        if (!_isCaptured)
            return;

        if (_isDragging)
        {
            _isDragging = false;
            _startDragPoint = default;
        }
        else
        {
            Snap(e.GetPosition(this));
        }

        _isCaptured = false;
        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerWheelChangedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer wheel changed event args</param>
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        if (!IsEnabled)
            return;

        MoveToNextTick(e.Delta.Y * SmallChange);
        e.Handled = true;
    }

    /// <inheritdoc />
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);

        if (!IsEnabled)
            return;

        if (e.Handled || e.KeyModifiers != KeyModifiers.None)
            return;

        var usingXyNavigation = this.IsAllowedXyNavigationMode(e.KeyDeviceType);
        var allowArrowKeys = _isFocusEngaged || !usingXyNavigation;

        var handled = true;
        switch (e.Key)
        {
            case Key.Enter when usingXyNavigation:
                _isFocusEngaged = !_isFocusEngaged;
                handled = true;
                break;
            case Key.Escape when usingXyNavigation:
                _isFocusEngaged = false;
                handled = true;
                break;

            case Key.Down when allowArrowKeys:
            case Key.Left when allowArrowKeys:
                MoveToNextTick(-SmallChange);
                break;

            case Key.Up when allowArrowKeys:
            case Key.Right when allowArrowKeys:
                MoveToNextTick(SmallChange);
                break;

            case Key.PageUp:
                MoveToNextTick(LargeChange);
                break;

            case Key.PageDown:
                MoveToNextTick(-LargeChange);
                break;

            case Key.Home:
                SetCurrentValue(ValueProperty, Minimum);
                break;

            case Key.End:
                SetCurrentValue(ValueProperty, Maximum);
                break;

            default:
                handled = false;
                break;
        }

        e.Handled = handled;
    }

    /// <summary>
    /// Recalculates the angle
    /// </summary>
    protected virtual void RecalculateAngles()
    {
        var valuePosition = Range > 0 ? (Value - Minimum) / Range : 0;
        var calculatedAngle = StartAngle + SweepAngle * valuePosition;

        SetAndRaise(LevelSweepAngleProperty, ref _levelSweepAngle, calculatedAngle - StartAngle);
        SetAndRaise(PointerStartAngleProperty,
            ref _pointerStartAngle,
            calculatedAngle - PointerThickness / 2);
    }

    private void MoveToNextTick(double direction)
    {
        if (direction == 0.0)
            return;

        var value = Value;

        // Find the next value by snapping
        var next = SnapToTick(Math.Max(Minimum, Math.Min(Maximum, value + direction)));

        var greaterThan = direction > 0; //search for the next tick greater than value?

        // If the snapping brought us back to value, find the next tick point
        if (Math.Abs(next - value) < Tolerance
            && !(greaterThan && Math.Abs(value - Maximum) < Tolerance) // Stop if searching up if already at Max
            && !(!greaterThan && Math.Abs(value - Minimum) < Tolerance)) // Stop if searching down if already at Min
        {
            var ticks = Ticks;

            // If ticks collection is available, use it.
            // Note that ticks may be unsorted.
            if (ticks != null && ticks.Count > 0)
            {
                foreach (var tick in ticks)
                {
                    // Find the smallest tick greater than value or the largest tick less than value
                    if (greaterThan && MathHelpers.GreaterThan(tick, value) &&
                        (MathHelpers.LessThan(tick, next) || Math.Abs(next - value) < Tolerance)
                        || !greaterThan && MathHelpers.LessThan(tick, value) &&
                        (MathHelpers.GreaterThan(tick, next) || Math.Abs(next - value) < Tolerance))
                    {
                        next = tick;
                    }
                }
            }
            else if (MathHelpers.GreaterThan(TickFrequency, 0.0))
            {
                // Find the current tick we are at
                var tickNumber = Math.Round((value - Minimum) / TickFrequency);

                if (greaterThan)
                    tickNumber += 1.0;
                else
                    tickNumber -= 1.0;

                next = Minimum + tickNumber * TickFrequency;
            }
        }

        // Update if we've found a better value
        if (Math.Abs(next - value) > Tolerance)
        {
            SetCurrentValue(ValueProperty, next);
        }
    }

    /// <summary>
    /// Snap the input 'value'.
    /// </summary>
    private void Snap(Point currentPoint)
    {
        var center = Center;
        var currentAngle = currentPoint.Atan2FromCenter(center);

        // Convert StartAngle and SweepAngle to radians for consistent calculation
        var startAngleRad = StartAngleRad;
        var sweepAngleRad = SweepAngleRad;
        var endAngleRad = EndAngleRad;

        // Normalize the angle to be in the range [startAngleRad, startAngleRad + 2*PI)
        var normalizedAngle = currentAngle;
        while (normalizedAngle < startAngleRad)
            normalizedAngle += 2 * Math.PI;
        while (normalizedAngle >= startAngleRad + 2 * Math.PI)
            normalizedAngle -= 2 * Math.PI;

        // Check if the angle is within the sweep range
        var relativeAngle = normalizedAngle - startAngleRad; // This is the angle within the sweep

        // Within the range
        if (relativeAngle <= sweepAngleRad)
        {
            // Map the angle to a value between Minimum and Maximum
            var anglePosition = relativeAngle / sweepAngleRad;

            var newValue = Minimum + anglePosition * Range;

            // Set value in increments of SmallChange
            var targetValueInSmallChanges = Math.Round((newValue - Minimum) / SmallChange);
            SetCurrentValue(ValueProperty, SnapToTick(Minimum + targetValueInSmallChanges * SmallChange));
        }
        else
        {
            // Angle is outside the valid range - determine which end is closer

            // Calculate distances to both ends
            var distanceToStart = Math.Abs(normalizedAngle - startAngleRad);
            var distanceToEnd = Math.Abs(normalizedAngle - endAngleRad);

            // Consider wrap-around distances
            var wrapDistanceToStart = 2 * Math.PI - distanceToStart;
            var wrapDistanceToEnd = 2 * Math.PI - distanceToEnd;

            var effectiveDistanceToStart = Math.Min(distanceToStart, wrapDistanceToStart);
            var effectiveDistanceToEnd = Math.Min(distanceToEnd, wrapDistanceToEnd);

            if (effectiveDistanceToStart <= effectiveDistanceToEnd)
            {
                SetCurrentValue(ValueProperty, Minimum);
            }
            else
            {
                SetCurrentValue(ValueProperty, Maximum);
            }
        }
    }

    /// <summary>
    /// Snap the input 'value' to the closest tick.
    /// </summary>
    /// <param name="value">Value that want to snap to closest Tick.</param>
    private double SnapToTick(double value)
    {
        if (IsSnapToTickEnabled)
        {
            var previous = Minimum;
            var next = Maximum;

            // This property is rarely set so let's try to avoid the GetValue
            var ticks = Ticks;

            // If ticks collection is available, use it.
            // Note that ticks may be unsorted.
            if (ticks != null && ticks.Count > 0)
            {
                foreach (var tick in ticks)
                {
                    if (MathHelpers.AreClose(tick, value))
                    {
                        return value;
                    }

                    if (MathHelpers.LessThan(tick, value) && MathHelpers.GreaterThan(tick, previous))
                    {
                        previous = tick;
                    }
                    else if (MathHelpers.GreaterThan(tick, value) && MathHelpers.LessThan(tick, next))
                    {
                        next = tick;
                    }
                }
            }
            else if (MathHelpers.GreaterThan(TickFrequency, 0.0))
            {
                previous = Minimum + Math.Round((value - Minimum) / TickFrequency) * TickFrequency;
                next = Math.Min(Maximum, previous + TickFrequency);
            }

            // Choose the closest value between previous and next. If tie, snap to 'next'.
            value = MathHelpers.GreaterThanOrClose(value, (previous + next) * 0.5) ? next : previous;
        }

        return value;
    }
}