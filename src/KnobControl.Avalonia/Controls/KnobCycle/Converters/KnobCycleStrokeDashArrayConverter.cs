using System;
using System.Globalization;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Stroke dash array converter for PART_Ellipse control in <see cref="KnobCycle"/>
/// </summary>
public class KnobCycleStrokeDashArrayConverter : IValueConverter
{
    /// <summary>
    /// Instance
    /// </summary>
    public static readonly KnobCycleStrokeDashArrayConverter Instance = new();

    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double dashGrips)
            return AvaloniaProperty.UnsetValue;

        return new AvaloniaList<double> { 1, dashGrips };
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}