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
/// Base class for controls that display a value within a knob.
/// </summary>
[PseudoClasses(":pressed")]
public abstract partial class KnobBase : TemplatedControl
{
    /// <summary>
    /// Is control focused engaged;
    /// </summary>
    protected bool IsFocusEngaged;

    /// <summary>
    /// Is control dragging.
    /// </summary>
    protected bool IsDragging;

    /// <summary>
    /// Is control captured.
    /// </summary>
    protected bool IsCaptured;

    /// <summary>
    /// Start dragging point
    /// </summary>
    protected Point StartDragPoint;

    /// <summary>
    /// Tolerance
    /// </summary>
    protected const double Tolerance = 0.0001;

    /// <summary>
    /// MinDraggingChangesValue
    /// </summary>
    protected const double MinDraggingChangesValue = 2.0;

    /// <summary>
    /// Get the center of the control.
    /// </summary>
    protected Point Center => new(Bounds.Width / 2, Bounds.Height / 2);

    /// <summary>
    /// Get the start angle in radians.
    /// </summary>
    protected double StartAngleRad => StartAngle * Math.PI / 180.0;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CoerceValue(ValueProperty);
        RecalculateAngles();
    }

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

    /// <summary>
    /// Called when the <see cref="InputElement.PointerPressedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer pressed event args</param>
    protected sealed override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (!IsEnabled)
            return;

        if (!e.Properties.IsLeftButtonPressed)
            return;

        OnPointerPressedInternal(e);
        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerMovedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer moved event args</param>
    protected sealed override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (!IsEnabled)
            return;

        if (!IsCaptured)
            return;

        var currentPoint = e.GetPosition(this);

        // Threshold to detect actual dragging
        if (!(currentPoint.LengthFromPoints(StartDragPoint) > MinDraggingChangesValue))
            return;

        OnPointerMovedInternal(e);
        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerReleasedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer released event args</param>
    protected sealed override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (!IsEnabled)
            return;

        if (!IsCaptured)
            return;

        OnPointerReleasedInternal(e);
        e.Handled = true;
    }

    /// <summary>
    /// Called when the <see cref="InputElement.PointerWheelChangedEvent"/> event called.
    /// </summary>
    /// <param name="e">Pointer wheel changed event args</param>
    protected sealed override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        if (!IsEnabled)
            return;

        OnPointerWheelChangedInternal(e);
        e.Handled = true;
    }

    /// <summary>
    /// Internal handler for the <see cref="InputElement.PointerPressedEvent"/> event.
    /// </summary>
    /// <param name="e">Pointer pressed event args</param>
    protected virtual void OnPointerPressedInternal(PointerPressedEventArgs e)
    {
        var center = Center;
        IsDragging = false;
        IsCaptured = true;
        StartDragPoint = e.GetPosition(this);
        StartDragPoint.Atan2FromCenter(center);
    }

    /// <summary>
    /// Internal handler for the <see cref="InputElement.PointerMovedEvent"/> event.
    /// </summary>
    /// <param name="e">Pointer moved event args</param>
    protected virtual void OnPointerMovedInternal(PointerEventArgs e)
    {
        IsDragging = true;
    }

    /// <summary>
    /// Internal handler for the <see cref="InputElement.PointerReleasedEvent"/> event.
    /// </summary>
    /// <param name="e">Pointer released event args</param>
    protected virtual void OnPointerReleasedInternal(PointerReleasedEventArgs e)
    {
        if (IsDragging)
        {
            IsDragging = false;
            StartDragPoint = default;
        }

        IsCaptured = false;
    }

    /// <summary>
    /// Internal handler for the <see cref="InputElement.PointerWheelChangedEvent"/> event.
    /// </summary>
    /// <param name="e">Pointer wheel changed event args</param>
    protected virtual void OnPointerWheelChangedInternal(PointerWheelEventArgs e)
    {
    }

    /// <summary>
    /// Recalculates the angles.
    /// </summary>
    protected virtual void RecalculateAngles()
    {
    }
}