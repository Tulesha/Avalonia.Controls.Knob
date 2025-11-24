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
}