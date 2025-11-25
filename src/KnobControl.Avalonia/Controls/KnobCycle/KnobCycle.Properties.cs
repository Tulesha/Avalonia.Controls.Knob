using Avalonia;

namespace KnobControl.Avalonia;

public partial class KnobCycle
{
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

    #region GripsCount Property

    /// <summary>
    /// Defines the <see cref="GripsCount"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsCountProperty =
        KnobCycleGrips.GripsCountProperty.AddOwner<KnobCycle>();

    /// <summary>
    /// Gets or sets the count of grips inside the KnobCycle.
    /// </summary>
    public double GripsCount
    {
        get => GetValue(GripsCountProperty);
        set => SetValue(GripsCountProperty, value);
    }

    #endregion

    #region GripsSize Property

    /// <summary>
    /// Defines the <see cref="GripsSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsSizeProperty =
        KnobCycleGrips.GripsSizeProperty.AddOwner<KnobCycle>();

    /// <summary>
    /// Gets or sets the size of grips.
    /// </summary>
    public double GripsSize
    {
        get => GetValue(GripsSizeProperty);
        set => SetValue(GripsSizeProperty, value);
    }

    #endregion

    #region GripsThickness Property

    /// <summary>
    /// Defines the <see cref="GripsThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsThicknessProperty =
        KnobDecoratorBase.DecoratorThicknessProperty.AddOwner<KnobCycle>(
            new StyledPropertyMetadata<double>(defaultValue: 3));

    /// <summary>
    /// Gets or sets the thickness of grips.
    /// </summary>
    public double GripsThickness
    {
        get => GetValue(GripsThicknessProperty);
        set => SetValue(GripsThicknessProperty, value);
    }

    #endregion
}