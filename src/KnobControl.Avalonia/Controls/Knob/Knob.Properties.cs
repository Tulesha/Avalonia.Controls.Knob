using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Mixins;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class Knob
{
    #region Minimum Property

    /// <summary>
    /// Defines the <see cref="Minimum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Minimum),
            coerce: CoerceMinimum);

    /// <summary>
    /// Gets or sets the minimum possible value.
    /// </summary>
    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    private static double CoerceMinimum(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceMinimum(value);

        return value;
    }

    #endregion

    #region Maximum Property

    /// <summary>
    /// Defines the <see cref="Maximum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Maximum),
            100,
            coerce: CoerceMaximum);

    /// <summary>
    /// Gets or sets the maximum possible value.
    /// </summary>
    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    private static double CoerceMaximum(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceMaximum(value);

        return value;
    }

    #endregion

    #region SweepAngle Property

    /// <summary>
    /// Defines the <see cref="SweepAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(SweepAngle),
            defaultValue: 300.0,
            coerce: CoerceSweepAngle);

    /// <summary>
    /// Gets or sets the sweep angle in degree.
    /// </summary>
    public double SweepAngle
    {
        get => GetValue(SweepAngleProperty);
        set => SetValue(SweepAngleProperty, value);
    }

    private static double CoerceSweepAngle(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceSweepAngle(value);

        return value;
    }

    #endregion

    #region Range Property

    private double _range;

    /// <summary>
    /// Defines the <see cref="Range"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> RangeProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(Range),
            o => o.Range);

    /// <summary>
    /// Gets the range from <see cref="Knob.Minimum"/> and <see cref="Knob.Maximum"/> properties.
    /// </summary>
    public double Range => _range;

    #endregion

    #region Ticks Property

    /// <summary>
    /// Defines the <see cref="Ticks"/> property.
    /// </summary>
    public static readonly StyledProperty<AvaloniaList<double>?> TicksProperty =
        KnobTickBar.TicksProperty.AddOwner<Knob>();

    /// <summary>
    /// Defines the ticks to be drawn on the tick bar.
    /// </summary>
    public AvaloniaList<double>? Ticks
    {
        get => GetValue(TicksProperty);
        set => SetValue(TicksProperty, value);
    }

    #endregion

    static Knob()
    {
        PressedMixin.Attach<Knob>();
        FocusableProperty.OverrideDefaultValue<Knob>(true);

        AffectsRecalculateAngles<Knob>(MinimumProperty,
            MaximumProperty,
            SweepAngleProperty);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MinimumProperty)
        {
            OnMinimumChanged();
            UpdateRange();
        }
        else if (change.Property == MaximumProperty)
        {
            OnMaximumChanged();
            UpdateRange();
        }
    }

    /// <inheritdoc />
    protected override double CoerceValue(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue)
            ? MathHelpers.Clamp(baseValue, Minimum, Maximum)
            : Value;
    }

    /// <summary>
    /// Called when the <see cref="Minimum"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceMinimum(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue)
            ? baseValue
            : Minimum;
    }

    /// <summary>
    /// Called when the <see cref="Maximum"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceMaximum(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue)
            ? Math.Max(baseValue, Minimum)
            : Maximum;
    }

    /// <summary>
    /// Called when the <see cref="SweepAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceSweepAngle(double baseValue)
    {
        return CoerceHelpers.CoerceSweepAngle(baseValue, SweepAngle);
    }

    private void OnMinimumChanged()
    {
        if (IsInitialized && !_isDataContextChanging)
        {
            CoerceValue(MaximumProperty);
            CoerceValue(ValueProperty);
        }
    }

    private void OnMaximumChanged()
    {
        if (IsInitialized && !_isDataContextChanging)
        {
            CoerceValue(MinimumProperty);
            CoerceValue(ValueProperty);
        }
    }

    private void UpdateRange()
    {
        SetAndRaise(RangeProperty, ref _range, Maximum - Minimum);
    }
}