using Avalonia;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobCycleGrips
{
    #region GripsCount Property

    /// <summary>
    /// Defines the <see cref="GripsCount"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsCountProperty =
        AvaloniaProperty.Register<KnobCycleGrips, double>(
            nameof(GripsCount),
            defaultValue: 10.0,
            coerce: CoerceGripsCount);

    /// <summary>
    /// Gets or sets the count of grips inside the KnobCycle.
    /// </summary>
    public double GripsCount
    {
        get => GetValue(GripsCountProperty);
        set => SetValue(GripsCountProperty, value);
    }

    private static double CoerceGripsCount(AvaloniaObject sender, double value)
    {
        if (sender is KnobCycleGrips cycleGrips)
            return cycleGrips.CoerceGripsCount(value);

        return value;
    }

    #endregion

    #region GripsSize Property

    /// <summary>
    /// Defines the <see cref="GripsSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsSizeProperty =
        AvaloniaProperty.Register<KnobCycleGrips, double>(
            nameof(GripsSize),
            defaultValue: 10.0,
            coerce: CoerceGripsSize);

    /// <summary>
    /// Gets or sets the size of grips.
    /// </summary>
    public double GripsSize
    {
        get => GetValue(GripsSizeProperty);
        set => SetValue(GripsSizeProperty, value);
    }

    private static double CoerceGripsSize(AvaloniaObject sender, double value)
    {
        if (sender is KnobCycleGrips cycleGrips)
            return cycleGrips.CoerceGripsSize(value);

        return value;
    }

    #endregion

    static KnobCycleGrips()
    {
        AffectsRender<KnobCycleGrips>(FillProperty,
            GripsCountProperty,
            GripsSizeProperty);
    }

    /// <summary>
    /// Called when the <see cref="GripsCount"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceGripsCount(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > -1
            ? baseValue
            : GripsCount;
    }

    /// <summary>
    /// Called when the <see cref="GripsSize"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceGripsSize(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : GripsSize;
    }
}