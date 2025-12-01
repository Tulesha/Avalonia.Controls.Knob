using Avalonia;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobEndlessArrow
{
    #region ArrowSize Property

    /// <summary>
    /// Defines the <see cref="ArrowSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ArrowSizeProperty =
        AvaloniaProperty.Register<KnobEndlessArrow, double>(
            nameof(ArrowSize),
            defaultValue: 10.0,
            coerce: CoerceArrowSize);

    /// <summary>
    /// Gets or sets the arrow size.
    /// </summary>
    public double ArrowSize
    {
        get => GetValue(ArrowSizeProperty);
        set => SetValue(ArrowSizeProperty, value);
    }

    private static double CoerceArrowSize(AvaloniaObject sender, double value)
    {
        if (sender is KnobEndlessArrow knobEndlessArrow)
            return knobEndlessArrow.CoerceArrowSize(value);

        return value;
    }

    #endregion

    static KnobEndlessArrow()
    {
        AffectsRender<KnobEndlessArrow>(ArrowSizeProperty);
    }

    /// <summary>
    /// Called when the <see cref="ArrowSize"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceArrowSize(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : ArrowSize;
    }
}