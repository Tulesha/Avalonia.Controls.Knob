using Avalonia;
using Avalonia.Media;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobTickBarBase
{
    #region Fill Property

    /// <summary>
    /// Defines the <see cref="Fill"/> property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> FillProperty =
        AvaloniaProperty.Register<KnobTickBarBase, IBrush?>(
            nameof(Fill));

    /// <summary>
    /// Gets or sets the brush used to fill the KnobTickBarBase's Ticks.
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
        if (sender is KnobTickBarBase knobTickBarBase)
            return knobTickBarBase.CoerceStartAngle(value);

        return value;
    }

    #endregion

    #region TickFrequency Property

    /// <summary>
    /// Defines the <see cref="TickFrequency"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TickFrequencyProperty =
        AvaloniaProperty.Register<KnobTickBarBase, double>(
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

    static KnobTickBarBase()
    {
        AffectsRender<KnobTickBarBase>(FillProperty,
            StartAngleProperty,
            TickFrequencyProperty);
    }

    /// <summary>
    /// Called when the <see cref="StartAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceStartAngle(double baseValue)
    {
        return CoerceHelpers.CoerceStartAngle(baseValue, StartAngle);
    }
}