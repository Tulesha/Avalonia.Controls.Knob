using System;
using Avalonia.Interactivity;

namespace Avalonia.Controls;

public partial class Knob
{
    #region ValueChanged Event

    /// <summary>
    /// Defines the <see cref="ValueChanged"/> event.
    /// </summary>
    public static readonly RoutedEvent<KnobValueChangedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<Knob, KnobValueChangedEventArgs>(
            nameof(ValueChanged),
            RoutingStrategies.Bubble);

    /// <summary>
    /// Raised when the <see cref="Value"/> changes.
    /// </summary>
    public event EventHandler<KnobValueChangedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    #endregion

    /// <summary>
    /// Raises the <see cref="ValueChanged"/> event.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void RaiseValueChangedEvent(double oldValue, double newValue)
    {
        var e = new KnobValueChangedEventArgs(ValueChangedEvent, oldValue, newValue);
        RaiseEvent(e);
    }
}