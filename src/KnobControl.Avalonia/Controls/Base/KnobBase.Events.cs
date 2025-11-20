using System;
using Avalonia.Interactivity;

namespace KnobControl.Avalonia;

public partial class KnobBase
{
    #region ValueChanged Event

    /// <summary>
    /// Defines the <see cref="ValueChanged"/> event.
    /// </summary>
    public static readonly RoutedEvent<KnobBaseValueChangedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<KnobBase, KnobBaseValueChangedEventArgs>(
            nameof(ValueChanged), RoutingStrategies.Bubble);

    /// <summary>
    /// Occurs when the <see cref="Value"/> property changes.
    /// </summary>
    public event EventHandler<KnobBaseValueChangedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    #endregion
}

/// <summary>
/// Provides data specific to a <see cref="KnobBase.ValueChanged"/> event.
/// </summary>
public class KnobBaseValueChangedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KnobBaseValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldValue">The old value of the knob value property.</param>
    /// <param name="newValue">The new value of the knob value property.</param>
    /// <param name="routedEvent">The routed event associated with these event args.</param>
    public KnobBaseValueChangedEventArgs(double oldValue, double newValue, RoutedEvent? routedEvent)
        : base(routedEvent)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KnobBaseValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldValue">The old value of the knob value property.</param>
    /// <param name="newValue">The new value of the knob value property.</param>
    /// <param name="routedEvent">The routed event associated with these event args.</param>
    /// <param name="source">The source object that raised the routed event.</param>
    public KnobBaseValueChangedEventArgs(double oldValue, double newValue, RoutedEvent? routedEvent, object? source)
        : base(routedEvent, source)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Gets the old value of the knob value property.
    /// </summary>
    public double OldValue { get; }

    /// <summary>
    /// Gets the new value of the knob value property.
    /// </summary>
    public double NewValue { get; }
}