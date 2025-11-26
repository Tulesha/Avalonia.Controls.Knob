using Avalonia;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobCycle
{
    #region GripsDash Property

    /// <summary>
    /// Defines the <see cref="GripsDash"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsDashProperty =
        AvaloniaProperty.Register<KnobCycle, double>(
            nameof(GripsDash),
            defaultValue: 3.0,
            coerce: CoerceGripsDash);

    /// <summary>
    /// Gets or sets the dash of grips inside the KnobCycle.
    /// </summary>
    public double GripsDash
    {
        get => GetValue(GripsDashProperty);
        set => SetValue(GripsDashProperty, value);
    }

    private static double CoerceGripsDash(AvaloniaObject sender, double value)
    {
        if (sender is KnobCycle knobCycle)
            return knobCycle.CoerceGripsDash(value);

        return value;
    }

    #endregion

    #region GripsThickness Property

    /// <summary>
    /// Defines the <see cref="GripsThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsThicknessProperty =
        KnobDecoratorBase.DecoratorThicknessProperty.AddOwner<KnobCycle>(
            new StyledPropertyMetadata<double>(defaultValue: 3.0));

    /// <summary>
    /// Gets or sets the thickness of grips.
    /// </summary>
    public double GripsThickness
    {
        get => GetValue(GripsThicknessProperty);
        set => SetValue(GripsThicknessProperty, value);
    }

    #endregion

    #region RotateAngle Property

    private double _rotateAngle;

    /// <summary>
    /// Defines the <see cref="RotateAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<KnobCycle, double> RotateAngleProperty =
        AvaloniaProperty.RegisterDirect<KnobCycle, double>(
            nameof(RotateAngle),
            o => o.RotateAngle);

    /// <summary>
    /// Gets the rotate angle of the arc.
    /// </summary>
    public double RotateAngle => _rotateAngle;

    #endregion

    /// <summary>
    /// Called when the <see cref="GripsDash"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceGripsDash(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > -1
            ? baseValue
            : GripsDash;
    }
}