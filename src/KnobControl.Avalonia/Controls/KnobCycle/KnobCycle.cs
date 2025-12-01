using System;
using Avalonia;
using Avalonia.Input;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

/// <summary>
/// Knob cycle control
/// </summary>
public partial class KnobCycle : KnobBase
{
    private const double SweepAngleRad = Math.PI * 2;

    private bool _isDragging;
    private bool _isCaptured;

    private Point _startDragPoint;
    private double _startDragAngleRad;

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
        _startDragAngleRad = _startDragPoint.Atan2FromCenter(center);
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

        var center = Center;
        var currentAngle = currentPoint.Atan2FromCenter(center);

        // Calculate the difference in angle from the start of the drag
        var angleDelta = currentAngle - _startDragAngleRad;

        // Normalize the angle delta to handle wrap-around
        while (angleDelta > Math.PI)
            angleDelta -= 2 * Math.PI;
        while (angleDelta < -Math.PI)
            angleDelta += 2 * Math.PI;

        // Map the angle delta to the value range
        var normalizedDelta = angleDelta / SweepAngleRad;
        var rawValueChange = normalizedDelta * Range;

        MoveToNextValue(Math.Sign(rawValueChange) * SmallChange);
        _startDragAngleRad = currentAngle;
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
            _startDragAngleRad = 0;
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

        MoveToNextValue(e.Delta.Y * SmallChange);
        e.Handled = true;
    }

    /// <inheritdoc />
    protected override void MoveToNextValue(double value)
    {
        if (value == 0.0) return;

        SetCurrentValue(ValueProperty, Value + value);
        SetAndRaise(RotateAngleProperty, ref _rotateAngle, _rotateAngle + value);
    }
}