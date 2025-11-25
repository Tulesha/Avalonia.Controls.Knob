using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace KnobControl.Avalonia.Converters;

/// <summary>
/// Base margin conveter for controls in <see cref="Knob"/>
/// </summary>
public abstract class KnobMarginConverterBase : IMultiValueConverter
{
    /// <summary>
    /// Local converter.
    /// </summary>
    public abstract Thickness Convert(double tickSize, double arcThickness);

    /// <inheritdoc />
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 3 ||
            values[0] is not double ticksSize ||
            values[1] is not double minMaxTicksSize ||
            values[2] is not double arcThickness)
            return AvaloniaProperty.UnsetValue;

        return Convert(Math.Max(ticksSize, minMaxTicksSize), arcThickness);
    }
}