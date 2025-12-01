using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Shapes;

namespace KnobControl.Avalonia;

public partial class KnobEndless
{
    #region ArrowSize Property

    /// <summary>
    /// Defines the <see cref="ArrowSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ArrowSizeProperty =
        KnobEndlessArrow.ArrowSizeProperty.AddOwner<KnobEndless>();

    /// <summary>
    /// Gets or sets the arrow size.
    /// </summary>
    public double ArrowSize
    {
        get => GetValue(ArrowSizeProperty);
        set => SetValue(ArrowSizeProperty, value);
    }

    #endregion

    #region GripsDash Property

    /// <summary>
    /// Defines the <see cref="GripsDash"/> property.
    /// </summary>
    public static readonly StyledProperty<AvaloniaList<double>?> GripsDashProperty =
        Shape.StrokeDashArrayProperty.AddOwner<KnobEndless>();

    /// <summary>
    /// Gets or sets the dash list of grips inside the <see cref="KnobEndless"/>.
    /// </summary>
    public AvaloniaList<double>? GripsDash
    {
        get => GetValue(GripsDashProperty);
        set => SetValue(GripsDashProperty, value);
    }

    #endregion

    #region GripsThickness Property

    /// <summary>
    /// Defines the <see cref="GripsThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> GripsThicknessProperty =
        AvaloniaProperty.Register<KnobEndless, double>(
            nameof(GripsThickness),
            defaultValue: 3.0);

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
    public static readonly DirectProperty<KnobEndless, double> RotateAngleProperty =
        AvaloniaProperty.RegisterDirect<KnobEndless, double>(
            nameof(RotateAngle),
            o => o.RotateAngle);

    /// <summary>
    /// Gets the rotate angle of the arc.
    /// </summary>
    public double RotateAngle => _rotateAngle;

    #endregion
}