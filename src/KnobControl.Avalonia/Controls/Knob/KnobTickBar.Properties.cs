using Avalonia;
using Avalonia.Collections;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobTickBar
{
    #region SweepAngle Property

    /// <summary>
    /// Defines the <see cref="SweepAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
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
        if (sender is KnobTickBar knobTickBar)
            return knobTickBar.CoerceSweepAngle(value);

        return value;
    }

    #endregion

    #region Minimum Property

    /// <summary>
    /// Defines the <see cref="Minimum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(Minimum));

    /// <summary>
    /// Gets or sets the logical position where the Minimum Tick will be drawn.
    /// </summary>
    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    #endregion

    #region Maximum Property

    /// <summary>
    /// Defines the <see cref="Maximum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(Maximum));

    /// <summary>
    /// Gets or sets the logical position where the Maximum Tick will be drawn.
    /// </summary>
    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    #endregion

    #region Range Property

    private double _range;

    /// <summary>
    /// Defines the <see cref="Range"/> property.
    /// </summary>
    public static readonly DirectProperty<KnobTickBar, double> RangeProperty =
        AvaloniaProperty.RegisterDirect<KnobTickBar, double>(
            nameof(Range),
            o => o.Range);

    /// <summary>
    /// Get the range from <see cref="Minimum"/> and <see cref="Maximum"/> properties.
    /// </summary>
    public double Range => _range;

    #endregion

    #region Ticks Property

    /// <summary>
    /// Defines the <see cref="Ticks"/> property.
    /// </summary>
    public static readonly StyledProperty<AvaloniaList<double>?> TicksProperty =
        AvaloniaProperty.Register<KnobTickBar, AvaloniaList<double>?>(
            nameof(Ticks));

    /// <summary>
    /// Gets or sets the value, which contains collection of value of type Double which
    /// are the logical positions use to draw the ticks.
    /// The property value is a <see cref="AvaloniaList{T}" />.
    /// </summary>
    public AvaloniaList<double>? Ticks
    {
        get => GetValue(TicksProperty);
        set => SetValue(TicksProperty, value);
    }

    #endregion

    static KnobTickBar()
    {
        AffectsRender<KnobTickBar>(SweepAngleProperty,
            MaximumProperty,
            MinimumProperty,
            TicksProperty);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MinimumProperty ||
            change.Property == MaximumProperty)
        {
            UpdateRange();
        }
    }
    
    /// <summary>
    /// Called when the <see cref="SweepAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceSweepAngle(double baseValue)
    {
        return CoerceHelpers.CoerceSweepAngle(baseValue, SweepAngle);
    }

    private void UpdateRange()
    {
        SetAndRaise(RangeProperty, ref _range, Maximum - Minimum);
    }
}