using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for Ellipse control in <see cref="KnobCycle"/>. 
/// </summary>
public class KnobCycleEllipseMarginConverter : IValueConverter
{
    private const double Indent = 14.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobCycleEllipseMarginConverter Instance = new();

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double arcThickness)
            return AvaloniaProperty.UnsetValue;

        return new Thickness(arcThickness + Indent);
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}