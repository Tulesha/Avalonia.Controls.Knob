using Avalonia;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for LevelPointerArc control in <see cref="Knob"/>.
/// </summary>
public class KnobLevelPointerArcMarginConverter : KnobMarginConverterBase
{
    private const double Indent = 8.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobLevelPointerArcMarginConverter Instance = new();

    /// <inheritdoc />
    public override Thickness Convert(double tickSize, double arcThickness)
    {
        var ellipseMarginThickness = KnobEllipseMarginConverter.Instance.Convert(tickSize, arcThickness);
        return new Thickness(ellipseMarginThickness.Left + Indent,
            ellipseMarginThickness.Top + Indent,
            ellipseMarginThickness.Right + Indent,
            ellipseMarginThickness.Bottom + Indent);
    }
}