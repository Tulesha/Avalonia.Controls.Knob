using Avalonia;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for Ellipse control in <see cref="Knob"/>. 
/// </summary>
public class KnobEllipseMarginConverter : KnobMarginConverterBase
{
    private const double Indent = 4.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobEllipseMarginConverter Instance = new();

    /// <inheritdoc />
    public override Thickness Convert(double tickSize, double arcThickness)
    {
        var mainArcMarginThickness = KnobLevelArcMarginConverter.Instance.Convert(tickSize, arcThickness);
        var levelStrokeThickness = KnobLevelStrokeThicknessConverter.Instance.Convert(arcThickness);

        return new Thickness(mainArcMarginThickness.Left + Indent + levelStrokeThickness,
            mainArcMarginThickness.Top + Indent + levelStrokeThickness,
            mainArcMarginThickness.Right + Indent + levelStrokeThickness,
            mainArcMarginThickness.Bottom + Indent + levelStrokeThickness);
    }
}