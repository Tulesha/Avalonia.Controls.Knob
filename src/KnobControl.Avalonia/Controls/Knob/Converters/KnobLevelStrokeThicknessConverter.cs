using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Stroke thickness converter for LevelArc control in <see cref="Knob"/>.
/// </summary>
public class KnobLevelStrokeThicknessConverter : IValueConverter
{
    private const double Indent = 2.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobLevelStrokeThicknessConverter Instance = new();

    /// <summary>
    /// Local converter.
    /// </summary>
    public double Convert(double arcThickness) => arcThickness + Indent;

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double arcThickness)
            return AvaloniaProperty.UnsetValue;

        return Convert(arcThickness);
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}