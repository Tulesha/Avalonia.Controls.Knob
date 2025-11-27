using Avalonia;
using Avalonia.Media;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobDecoratorBase
{
    #region Fill Property

    /// <summary>
    /// Defines the <see cref="Fill"/> property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> FillProperty =
        AvaloniaProperty.Register<KnobDecoratorBase, IBrush?>(
            nameof(Fill));

    /// <summary>
    /// Gets or sets the brush used to fill the KnobDecoratorBase's Grips.
    /// </summary>
    public IBrush? Fill
    {
        get => GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    #endregion

    #region DecoratorThickness Property

    /// <summary>
    /// Defines the <see cref="DecoratorThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> DecoratorThicknessProperty =
        AvaloniaProperty.Register<KnobDecoratorBase, double>(
            nameof(DecoratorThickness),
            defaultValue: 1.0,
            coerce: CoerceDecoratorThickness);

    /// <summary>
    /// Gets or sets the thickness of the decorated items.
    /// </summary>
    public double DecoratorThickness
    {
        get => GetValue(DecoratorThicknessProperty);
        set => SetValue(DecoratorThicknessProperty, value);
    }

    private static double CoerceDecoratorThickness(AvaloniaObject sender, double value)
    {
        if (sender is KnobDecoratorBase knobDecoratorBase)
            return knobDecoratorBase.CoerceDecoratorThickness(value);

        return value;
    }

    #endregion

    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<KnobDecoratorBase, double>(
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
        if (sender is KnobDecoratorBase knobDecorator)
            return knobDecorator.CoerceStartAngle(value);

        return value;
    }

    #endregion

    #region SweepAngle Property

    /// <summary>
    /// Defines the <see cref="SweepAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty =
        AvaloniaProperty.Register<KnobDecoratorBase, double>(
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
        if (sender is KnobDecoratorBase knobDecorator)
            return knobDecorator.CoerceSweepAngle(value);

        return value;
    }

    #endregion

    static KnobDecoratorBase()
    {
        AffectsRender<KnobDecoratorBase>(FillProperty,
            DecoratorThicknessProperty,
            StartAngleProperty,
            SweepAngleProperty);
    }

    /// <summary>
    /// Called when the <see cref="DecoratorThickness"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceDecoratorThickness(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : DecoratorThickness;
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