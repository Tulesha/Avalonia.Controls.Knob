using Avalonia.Interactivity;

namespace Avalonia.Controls;

/// <inheritdoc />
public class KnobValueChangedEventArgs : RoutedEventArgs
{
    /// <inheritdoc />
    public KnobValueChangedEventArgs(RoutedEvent routedEvent, double oldValue, double newValue) : base(routedEvent)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Get the old value.
    /// </summary>
    public double OldValue { get; }

    /// <summary>
    /// Get the new value.
    /// </summary>
    public double NewValue { get; }
}