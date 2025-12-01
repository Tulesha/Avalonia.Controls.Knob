using System;
using System.Globalization;
using Avalonia.Collections;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Demo.Converters;

public class GripsDashEndlessToAvaloniaListConverter : IValueConverter
{
    public static readonly GripsDashEndlessToAvaloniaListConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double dash)
            return new AvaloniaList<double> { 1.0, 2.0 };

        return new AvaloniaList<double> { 1.0, dash };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}