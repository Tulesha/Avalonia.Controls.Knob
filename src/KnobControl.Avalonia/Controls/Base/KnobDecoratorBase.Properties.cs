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

    #region DecoratorStyle Property

    /// <summary>
    /// Defines the <see cref="DecoratorStyle"/> property.
    /// </summary>
    public static readonly StyledProperty<KnobDecoratorStyle> DecoratorStyleProperty =
        AvaloniaProperty.Register<KnobDecoratorBase, KnobDecoratorStyle>(
            nameof(DecoratorStyle));

    /// <summary>
    /// Gets or sets the style for decorated items.
    /// </summary>
    public KnobDecoratorStyle DecoratorStyle
    {
        get => GetValue(DecoratorStyleProperty);
        set => SetValue(DecoratorStyleProperty, value);
    }

    #endregion

    static KnobDecoratorBase()
    {
        AffectsRender<KnobDecoratorBase>(FillProperty,
            DecoratorThicknessProperty,
            DecoratorStyleProperty);
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
}