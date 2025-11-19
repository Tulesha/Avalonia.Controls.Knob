using Avalonia.Collections;
using Avalonia.Controls.Helpers;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Reactive;

namespace Avalonia.Controls;

public partial class Knob
{
    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(StartAngle),
            defaultValue: -240,
            coerce: CoerceStartAngle);

    /// <summary>
    /// Get or set the start angle in degree.
    /// </summary>
    public double StartAngle
    {
        get => GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    private static double CoerceStartAngle(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceStartAngle(value);

        return value;
    }

    #endregion

    #region SweepAngle Property

    /// <summary>
    /// Defines the <see cref="SweepAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(SweepAngle),
            defaultValue: 300,
            coerce: CoerceSweepAngle);

    /// <summary>
    /// Get or set the sweep angle in degree.
    /// </summary>
    public double SweepAngle
    {
        get => GetValue(SweepAngleProperty);
        set => SetValue(SweepAngleProperty, value);
    }

    private static double CoerceSweepAngle(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceSweepAngle(value);

        return value;
    }

    #endregion

    #region IsValueTextVisible Property

    /// <summary>
    /// Defines the <see cref="IsValueTextVisible"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsValueTextVisibleProperty =
        AvaloniaProperty.Register<Knob, bool>(
            nameof(IsValueTextVisible),
            true);

    /// <summary>
    /// Get or set the visibility for text value.
    /// </summary>
    public bool IsValueTextVisible
    {
        get => GetValue(IsValueTextVisibleProperty);
        set => SetValue(IsValueTextVisibleProperty, value);
    }

    #endregion

    #region Range Property

    private double _range;

    /// <summary>
    /// Defines the <see cref="Range"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> RangeProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(Range),
            o => o.Range);

    /// <summary>
    /// Get the range from <see cref="RangeBase.Minimum"/> and <see cref="RangeBase.Maximum"/> properties.
    /// </summary>
    public double Range => _range;

    #endregion

    #region LevelSweepAngle Property

    private double _levelSweepAngle;

    /// <summary>
    /// Defines the <see cref="LevelSweepAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> LevelSweepAngleProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(LevelSweepAngle),
            o => o.LevelSweepAngle);

    /// <summary>
    /// Get the pointer level sweep angle.
    /// </summary>
    public double LevelSweepAngle => _levelSweepAngle;

    #endregion

    #region PointerStartAngle Property

    private double _pointerStartAngle;

    /// <summary>
    /// Defines the <see cref="PointerStartAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> PointerStartAngleProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(PointerStartAngle),
            o => o.PointerStartAngle);

    /// <summary>
    /// Get the pointer start angle.
    /// </summary>
    public double PointerStartAngle => _pointerStartAngle;

    #endregion

    #region PointerThickness Property

    /// <summary>
    /// Defines the <see cref="PointerThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> PointerThicknessProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(PointerThickness),
            defaultValue: 3);

    /// <summary>
    /// Get or set the thickness of the pointer.
    /// </summary>
    public double PointerThickness
    {
        get => GetValue(PointerThicknessProperty);
        set => SetValue(PointerThicknessProperty, value);
    }

    #endregion

    #region IsPointerVisible Property

    /// <summary>
    /// Defines the <see cref="IsPointerVisible"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsPointerVisibleProperty =
        AvaloniaProperty.Register<Knob, bool>(
            nameof(IsPointerVisible),
            defaultValue: true);

    /// <summary>
    /// Get or set is pointer visible.
    /// </summary>
    public bool IsPointerVisible
    {
        get => GetValue(IsPointerVisibleProperty);
        set => SetValue(IsPointerVisibleProperty, value);
    }

    #endregion

    #region TickFrequency Property

    /// <summary>
    /// Defines the <see cref="TickFrequency"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TickFrequencyProperty =
        KnobTickBar.TickFrequencyProperty.AddOwner<Knob>();

    /// <summary>
    /// Gets or sets the interval between tick marks.
    /// </summary>
    public double TickFrequency
    {
        get => GetValue(TickFrequencyProperty);
        set => SetValue(TickFrequencyProperty, value);
    }

    #endregion

    #region Ticks Property

    /// <summary>
    /// Defines the <see cref="Ticks"/> property.
    /// </summary>
    public static readonly StyledProperty<AvaloniaList<double>?> TicksProperty =
        KnobTickBar.TicksProperty.AddOwner<Knob>();

    /// <summary>
    /// Defines the ticks to be drawn on the tick bar.
    /// </summary>
    public AvaloniaList<double>? Ticks
    {
        get => GetValue(TicksProperty);
        set => SetValue(TicksProperty, value);
    }

    #endregion

    #region IsSnapToTickEnabled Property

    /// <summary>
    /// Defines the <see cref="IsSnapToTickEnabled"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsSnapToTickEnabledProperty =
        AvaloniaProperty.Register<Knob, bool>(
            nameof(IsSnapToTickEnabled));

    /// <summary>
    /// Gets or sets a value that indicates whether the <see cref="Knob"/> automatically moves the pointer to the closest tick mark.
    /// </summary>
    public bool IsSnapToTickEnabled
    {
        get => GetValue(IsSnapToTickEnabledProperty);
        set => SetValue(IsSnapToTickEnabledProperty, value);
    }

    #endregion

    /// <summary>
    /// Marks a property as affecting the control's recalculating angles.
    /// </summary>
    /// <param name="properties">The properties.</param>
    /// <typeparam name="T">The control which the property affects.</typeparam>
    protected static void AffectsRecalculateAngles<T>(params AvaloniaProperty[] properties)
        where T : Knob
    {
        var invalidateObserver =
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs>(static e => (e.Sender as T)?.RecalculateAngles());

        foreach (var property in properties)
        {
            property.Changed.Subscribe(invalidateObserver);
        }
    }

    static Knob()
    {
        PressedMixin.Attach<Knob>();
        FocusableProperty.OverrideDefaultValue<Knob>(true);

        AffectsRecalculateAngles<Knob>(ValueProperty,
            MinimumProperty,
            MaximumProperty,
            StartAngleProperty,
            SweepAngleProperty,
            PointerThicknessProperty);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MinimumProperty ||
            change.Property == MaximumProperty)
        {
            UpdateRange();
        }
    }

    /// <summary>
    /// Called when the <see cref="StartAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceStartAngle(double baseValue)
    {
        return CoerceHelpers.CoerceStartAngle(baseValue, StartAngle);
    }

    /// <summary>
    /// Called when the <see cref="SweepAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceSweepAngle(double baseValue)
    {
        return CoerceHelpers.CoerceSweepAngle(baseValue, SweepAngle);
    }

    private void UpdateRange()
    {
        SetAndRaise(RangeProperty, ref _range, Maximum - Minimum);
    }
}