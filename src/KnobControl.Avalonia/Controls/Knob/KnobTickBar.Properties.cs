using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobTickBar
{
    #region Fill Property

    /// <summary>
    /// Defines the <see cref="Fill"/> property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> FillProperty =
        AvaloniaProperty.Register<KnobTickBar, IBrush?>(
            nameof(Fill));

    /// <summary>
    /// Gets or sets the brush used to fill the KnobTickBar's Ticks.
    /// </summary>
    public IBrush? Fill
    {
        get => GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    #endregion

    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(StartAngle),
            defaultValue: -240.0,
            coerce: CoerceStartAngle);

    /// <summary>
    /// Gets or sets the start angle in degree.
    /// </summary>
    public double StartAngle
    {
        get => GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    private static double CoerceStartAngle(AvaloniaObject sender, double value)
    {
        if (sender is KnobTickBar knobTickBar)
            return knobTickBar.CoerceStartAngle(value);

        return value;
    }

    #endregion

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

    #region TickFrequency Property

    /// <summary>
    /// Defines the <see cref="TickFrequency"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TickFrequencyProperty =
        AvaloniaProperty.Register<KnobTickBar, double>(
            nameof(TickFrequency));

    /// <summary>
    /// Gets or sets the value, which defines how the tick will be drawn.
    /// </summary>
    public double TickFrequency
    {
        get => GetValue(TickFrequencyProperty);
        set => SetValue(TickFrequencyProperty, value);
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

    static KnobTickBar()
    {
        AffectsRender<KnobTickBar>(FillProperty,
            StartAngleProperty,
            SweepAngleProperty,
            MaximumProperty,
            MinimumProperty,
            TickFrequencyProperty,
            TicksProperty);
    }

    /// <summary>
    /// Called when the <see cref="StartAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceStartAngle(double baseValue)
    {
        return CoerceHelpers.CoerceStartAngle(baseValue, StartAngle);
    }

    /// <summary>
    /// Called when the <see cref="SweepAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceSweepAngle(double baseValue)
    {
        return CoerceHelpers.CoerceSweepAngle(baseValue, SweepAngle);
    }
}