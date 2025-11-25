using Avalonia;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for MainArc control in <see cref="Knob"/>.
/// </summary>
public class KnobMainArcMarginConverter : KnobMarginConverterBase
{
    private const double Indent = 4.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobMainArcMarginConverter Instance = new();

    /// <inheritdoc />
    public override Thickness Convert(double tickSize, double arcThickness) => new(tickSize + Indent);
}