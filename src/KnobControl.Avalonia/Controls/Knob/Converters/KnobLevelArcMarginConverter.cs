using Avalonia;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for LevelArc control in <see cref="Knob"/>. 
/// </summary>
public class KnobLevelArcMarginConverter : KnobMarginConverterBase
{
    private const double Indent = 1.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobLevelArcMarginConverter Instance = new();

    /// <inheritdoc />
    public override Thickness Convert(double tickSize, double arcThickness)
    {
        var mainArcMainThickness = KnobMainArcMarginConverter.Instance.Convert(tickSize, arcThickness);
        return new Thickness(mainArcMainThickness.Left - Indent,
            mainArcMainThickness.Top - Indent,
            mainArcMainThickness.Right - Indent,
            mainArcMainThickness.Bottom - Indent);
    }
}