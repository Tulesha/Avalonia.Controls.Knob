using System;
using Avalonia.Controls.Helpers;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.Controls;

/// <summary>
/// Knob control
/// </summary>
[PseudoClasses(":pressed")]
public partial class Knob : RangeBase
{
    private const double MinDraggingChangesValue = 2.0;

    private bool _isFocusEngaged;
    private bool _isDragging;
    private bool _isCaptured;

    private Point _startDragPoint;
    private double _startDragAngle;

    private IDisposable? _pointerPressedDispose;
    private IDisposable? _pointerMovedDispose;
    private IDisposable? _pointerReleasedDispose;
    private IDisposable? _pointerWheelDispose;

    /// <inheritdoc />
    public Knob()
    {
        UpdateValueRange();
    }

    /// <summary>
    /// Get the center of the control.
    /// </summary>
    protected Point Center => new(Bounds.Width / 2, Bounds.Height / 2);

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

        _pointerPressedDispose?.Dispose();
        _pointerMovedDispose?.Dispose();
        _pointerReleasedDispose?.Dispose();
        _pointerWheelDispose?.Dispose();

        _pointerPressedDispose = this.AddDisposableHandler(PointerPressedEvent,
            OnPointerPressedInternal,
            RoutingStrategies.Tunnel);
        _pointerMovedDispose = this.AddDisposableHandler(PointerMovedEvent,
            OnPointerMovedInternal,
            RoutingStrategies.Tunnel);
        _pointerReleasedDispose = this.AddDisposableHandler(PointerReleasedEvent,
            OnPointerReleasedInternal,
            RoutingStrategies.Tunnel);
        _pointerWheelDispose = this.AddDisposableHandler(PointerWheelChangedEvent,
            OnPointerWheelChangedInternal,
            RoutingStrategies.Tunnel);

        RecalculateAngles();
    }

    /// <inheritdoc />
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
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
                MoveToNextValue(-SmallChange);
                break;

            case Key.Up when allowArrowKeys:
            case Key.Right when allowArrowKeys:
                MoveToNextValue(SmallChange);
                break;

            case Key.PageUp:
                MoveToNextValue(LargeChange);
                break;

            case Key.PageDown:
                MoveToNextValue(-LargeChange);
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
        // v1, v2:
        // var newAngle = (StartAngle + SweepAngle - StartAngle) / (Maximum - Minimum) * (Value - Minimum);
        //
        // SetAndRaise(LevelSweepAngleProperty, ref _levelSweepAngle, newAngle);
        // SetAndRaise(PointerStartAngleProperty, ref _pointerStartAngle, StartAngle + newAngle - PointerThickness);

        // v3:
        var valuePosition = ValueRange > 0 ? (Value - Minimum) / ValueRange : 0;
        var calculatedAngle = StartAngle + SweepAngle * valuePosition;

        SetAndRaise(LevelSweepAngleProperty, ref _levelSweepAngle, calculatedAngle - StartAngle);
        SetAndRaise(PointerStartAngleProperty,
            ref _pointerStartAngle,
            calculatedAngle - PointerThickness); // Adjust pointer width
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerPressedEvent"/> event called.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Pointer pressed event args</param>
    protected virtual void OnPointerPressedInternal(object sender, PointerPressedEventArgs e)
    {
        if (!IsEnabled)
            return;

        if (!e.Properties.IsLeftButtonPressed)
            return;

        StartCapturing(e.GetPosition(this));

        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerMovedEvent"/> event called.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Pointer moved event args</param>
    protected virtual void OnPointerMovedInternal(object sender, PointerEventArgs e)
    {
        if (!IsEnabled)
            return;

        if (!_isCaptured)
            return;

        var currentPoint = e.GetPosition(this);

        // Threshold to detect actual dragging
        if (!(currentPoint.LengthFromPoints(_startDragPoint) > MinDraggingChangesValue))
            return;

        ProcessDragging(currentPoint);

        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerReleasedEvent"/> event called.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Pointer released event args</param>
    protected virtual void OnPointerReleasedInternal(object sender, PointerReleasedEventArgs e)
    {
        if (!IsEnabled)
            return;

        if (!_isCaptured)
            return;

        if (_isDragging)
        {
            StopDragging();
        }
        else
        {
            ProcessPressing(e.GetPosition(this));
        }

        _isCaptured = false;
        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerWheelChangedEvent"/> event called.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Pointer wheel changed event args</param>
    protected virtual void OnPointerWheelChangedInternal(object sender, PointerWheelEventArgs e)
    {
        if (!IsEnabled)
            return;

        MoveToNextValue(e.Delta.Y * SmallChange);
        e.Handled = true;
    }

    private void MoveToNextValue(double direction)
    {
        if (direction == 0.0) return;

        SetCurrentValue(ValueProperty, Value + direction);
    }

    private void StartCapturing(Point currentPoint)
    {
        var center = Center;
        _isDragging = false;
        _isCaptured = true;
        _startDragPoint = currentPoint;
        _startDragAngle = _startDragPoint.Atan2FromCenter(center);
    }

    private void ProcessDragging(Point currentPoint)
    {
        _isDragging = true;

        var center = Center;
        var currentAngle = currentPoint.Atan2FromCenter(center);

        // Calculate the difference in angle from the start of the drag
        var angleDelta = currentAngle - _startDragAngle;

        // Normalize the angle delta to handle wrap-around
        while (angleDelta > Math.PI)
            angleDelta -= 2 * Math.PI;
        while (angleDelta < -Math.PI)
            angleDelta += 2 * Math.PI;

        // Calculate the total sweep in radians
        var sweepAngleRad = SweepAngle * Math.PI / 180.0;

        // Map the angle delta to the value range
        var normalizedDelta = angleDelta / sweepAngleRad;
        var rawValueChange = normalizedDelta * ValueRange;

        SetCurrentValue(ValueProperty, Value + Math.Sign(rawValueChange) * SmallChange);
        _startDragAngle = currentAngle;
    }

    private void StopDragging()
    {
        // Reset dragging state
        _isDragging = false;
        _startDragPoint = default;
        _startDragAngle = 0;
    }

    private void ProcessPressing(Point currentPoint)
    {
        var center = Center;
        var currentAngle = currentPoint.Atan2FromCenter(center);

        // Convert StartAngle and SweepAngle to radians for consistent calculation
        var startAngleRad = StartAngle * Math.PI / 180.0;
        var sweepAngleRad = SweepAngle * Math.PI / 180.0;
        var endAngleRad = startAngleRad + sweepAngleRad;

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

            var newValue = Minimum + anglePosition * ValueRange;

            // Set value in increments of SmallChange
            var targetValueInSmallChanges = Math.Round((newValue - Minimum) / SmallChange);
            SetCurrentValue(ValueProperty, Minimum + targetValueInSmallChanges * SmallChange);
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
}