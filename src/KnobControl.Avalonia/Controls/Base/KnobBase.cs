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
/// A base control for Knob like controls.
/// </summary>
[PseudoClasses(":pressed")]
public abstract partial class KnobBase : RangeBase
{
    /// <summary>
    /// Min dragging changes while moving the pointer.
    /// </summary>
    protected const double MinDraggingChangesValue = 2.0;

    private bool _isFocusEngaged;

    /// <summary>
    /// Gets the range of the values.
    /// </summary>
    protected double Range => Maximum - Minimum;

    /// <summary>
    /// Gets the center of the control.
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
    /// Moves to the next value in the control
    /// </summary>
    /// <param name="value">Value</param>
    protected abstract void MoveToNextValue(double value);
}