using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Margin converter for Ellipse control in <see cref="KnobEndless"/>. 
/// </summary>
public class KnobEndlessEllipseMarginConverter : IValueConverter
{
    private const double Indent = 14.0;

    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobEndlessEllipseMarginConverter Instance = new();

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