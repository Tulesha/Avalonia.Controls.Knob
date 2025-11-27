using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Primitives;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobTickBar
{
    #region Maximum Property

    /// <summary>
    /// Defines the <see cref="Maximum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MaximumProperty =
        RangeBase.MaximumProperty.AddOwner<KnobTickBar>();

    /// <summary>
    /// Gets or sets the logical position where the Maximum Tick will be drawn.
    /// </summary>
    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    #endregion

    #region Minimum Property

    /// <summary>
    /// Defines the <see cref="Minimum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinimumProperty =
        RangeBase.MinimumProperty.AddOwner<KnobTickBar>();

    /// <summary>
    /// Gets or sets the logical position where the Minimum Tick will be drawn.
    /// </summary>
    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    #endregion

    #region TickFrequency Property

    /// <summary>
    /// Defines the <see cref="TickFrequency"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TickFrequencyProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(TickFrequency),
            defaultValue: 10.0,
            coerce: CoerceTickFrequency);

    /// <summary>
    /// Gets or sets the value, which defines how the tick will be drawn.
    /// </summary>
    public double TickFrequency
    {
        get => GetValue(TickFrequencyProperty);
        set => SetValue(TickFrequencyProperty, value);
    }

    private static double CoerceTickFrequency(AvaloniaObject sender, double value)
    {
        if (sender is KnobTickBar knobTickBar)
            return knobTickBar.CoerceTickFrequency(value);

        return value;
    }

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

    #region MinMaxTicksSize Property

    /// <summary>
    /// Defines the <see cref="MinMaxTicksSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinMaxTicksSizeProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(MinMaxTicksSize),
            defaultValue: 8.0,
            coerce: CoerceMinMaxTicksSize);

    /// <summary>
    /// Gets or sets the size of min and max ticks.
    /// </summary>
    public double MinMaxTicksSize
    {
        get => GetValue(MinMaxTicksSizeProperty);
        set => SetValue(MinMaxTicksSizeProperty, value);
    }

    private static double CoerceMinMaxTicksSize(AvaloniaObject sender, double value)
    {
        if (sender is KnobTickBar knobTickBar)
            return knobTickBar.CoerceMinMaxTicksSize(value);

        return value;
    }

    #endregion

    #region TicksSize Property

    /// <summary>
    /// Defines the <see cref="TicksSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TicksSizeProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(TicksSize),
            defaultValue: 4.0,
            coerce: CoerceTicksSize);

    /// <summary>
    /// Gets or sets the size of ticks.
    /// </summary>
    public double TicksSize
    {
        get => GetValue(TicksSizeProperty);
        set => SetValue(TicksSizeProperty, value);
    }

    private static double CoerceTicksSize(AvaloniaObject sender, double value)
    {
        if (sender is KnobTickBar knobTickBar)
            return knobTickBar.CoerceTicksSize(value);

        return value;
    }

    #endregion

    static KnobTickBar()
    {
        AffectsRender<KnobTickBar>(MaximumProperty,
            MinimumProperty,
            TickFrequencyProperty,
            TicksProperty,
            MinMaxTicksSizeProperty,
            TicksSizeProperty);
    }

    /// <summary>
    /// Called when the <see cref="TickFrequency"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceTickFrequency(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue >= 0
            ? baseValue
            : TickFrequency;
    }

    /// <summary>
    /// Called when the <see cref="MinMaxTicksSize"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceMinMaxTicksSize(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : MinMaxTicksSize;
    }

    /// <summary>
    /// Called when the <see cref="TicksSize"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceTicksSize(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : TicksSize;
    }
}