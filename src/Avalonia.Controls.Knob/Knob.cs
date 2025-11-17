using System;
using Avalonia.Controls.Helpers;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.Controls;

/// <summary>
/// Knob control
/// </summary>
[TemplatePart("PART_Ellipse", typeof(Ellipse), IsRequired = true)]
[TemplatePart("PART_TextBox", typeof(TextBox), IsRequired = true)]
public partial class Knob : TemplatedControl
{
    private IDisposable? _textBoxTextChangedSubscription;

    private bool _internalValueSet;
    private bool _isSyncingTextAndValueProperties;
    private bool _isTextChangedFromUi;

    private bool _capturedFromPressed;
    private Point _previousMousePosition;

    /// <summary>
    /// Gets the Ellipse template part.
    /// </summary>
    private Ellipse? _ellipse;

    /// <summary>
    /// Gets the TextBox template part.
    /// </summary>
    private TextBox? _textBox;

    /// <summary>
    /// Initializes new instance of <see cref="Knob"/> class.
    /// </summary>
    public Knob()
    {
        Initialized += (_, _) =>
        {
            if (!_internalValueSet && IsInitialized)
            {
                SyncTextAndValueProperties(false, null, true);
            }
        };
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_ellipse != null)
        {
            _ellipse.RemoveHandler(PointerPressedEvent,
                Ellipse_OnPointerPressed);
            _ellipse.RemoveHandler(PointerMovedEvent,
                Ellipse_OnPointerMoved);
            _ellipse.RemoveHandler(PointerReleasedEvent,
                Ellipse_OnPointerReleased);
            _ellipse.RemoveHandler(PointerWheelChangedEvent,
                Ellipse_OnPointerWheelChanged);
            _ellipse = null;
        }

        if (_textBox != null)
        {
            _textBoxTextChangedSubscription?.Dispose();
            _textBox.RemoveHandler(KeyDownEvent,
                TextBox_OnKeyDown);
            _textBox = null;
        }

        _textBox = e.NameScope.Find<TextBox>("PART_TextBox");
        if (_textBox != null)
        {
            _textBox.Text = Text;
            _textBoxTextChangedSubscription =
                _textBox.GetObservable(TextBox.TextProperty).Subscribe(_ => TextBoxOnTextChanged());

            _textBox.AddHandler(KeyDownEvent,
                TextBox_OnKeyDown,
                RoutingStrategies.Bubble | RoutingStrategies.Tunnel);
        }

        _ellipse = e.NameScope.Find<Ellipse>("PART_Ellipse");
        if (_ellipse != null)
        {
            _ellipse.AddHandler(PointerPressedEvent,
                Ellipse_OnPointerPressed,
                RoutingStrategies.Bubble | RoutingStrategies.Tunnel);
            _ellipse.AddHandler(PointerMovedEvent,
                Ellipse_OnPointerMoved,
                RoutingStrategies.Bubble | RoutingStrategies.Tunnel);
            _ellipse.AddHandler(PointerReleasedEvent,
                Ellipse_OnPointerReleased,
                RoutingStrategies.Bubble | RoutingStrategies.Tunnel);
            _ellipse.AddHandler(PointerWheelChangedEvent,
                Ellipse_OnPointerWheelChanged,
                RoutingStrategies.Bubble | RoutingStrategies.Tunnel);
        }

        UpdateUi();
    }

    /// <summary>
    /// Called to update the validation state for properties for which data validation is
    /// enabled.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="state">The current data binding state.</param>
    /// <param name="error">The current data binding error, if any.</param>
    protected override void UpdateDataValidation(
        AvaloniaProperty property,
        BindingValueType state,
        Exception? error)
    {
        if (property == TextProperty || property == ValueProperty)
        {
            DataValidationErrors.SetError(this, error);
        }
    }

    private void TextBoxOnTextChanged()
    {
        try
        {
            _isTextChangedFromUi = true;
            if (_textBox != null)
            {
                SetCurrentValue(TextProperty, _textBox.Text);
            }
        }
        finally
        {
            _isTextChangedFromUi = false;
        }
    }

    private void TextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Enter:
                var commitSuccess = CommitInput();
                e.Handled = !commitSuccess;
                break;
        }
    }

    private void Ellipse_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (!e.Properties.IsLeftButtonPressed)
            return;

        _capturedFromPressed = true;
        e.Pointer.Capture(_ellipse);
        _previousMousePosition = e.GetPosition(_ellipse);

        e.Handled = true;
    }

    private void Ellipse_OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (Equals(e.Pointer.Captured, _ellipse))
        {
            var newMousePosition = e.GetPosition(_ellipse);
            var dY = _previousMousePosition.Y - newMousePosition.Y;
            SetCurrentValue(ValueProperty, Value + Math.Sign(dY) * Increment);
            _previousMousePosition = newMousePosition;

            e.Handled = true;
        }
    }

    private void Ellipse_OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        if (Equals(e.Pointer.Captured, _ellipse) && _capturedFromPressed)
        {
            _previousMousePosition = default;
            e.Pointer.Capture(null);
            e.Handled = true;
        }

        _capturedFromPressed = false;
    }

    private void Ellipse_OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    {
        var d = e.Delta.Length / 120;
        SetCurrentValue(ValueProperty, Value + d * Increment);
    }

    private bool CommitInput(bool forceTextUpdate = false)
    {
        return SyncTextAndValueProperties(true, Text, forceTextUpdate);
    }

    private void UpdateUi()
    {
        var newAngle = (EndAngle - StartAngle) / (Maximum - Minimum) * (Value - Minimum) + StartAngle;
        SetAndRaise(PointerStartAngleProperty, ref _pointerStartAngle, newAngle - 3);
        SetAndRaise(PointerEndAngleProperty, ref _pointerEndAngle, newAngle + 3);
        SetAndRaise(LevelEndAngleProperty, ref _levelEndAngle, newAngle);
    }
}