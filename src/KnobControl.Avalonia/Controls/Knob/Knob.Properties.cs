using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Mixins;
using Avalonia.Reactive;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class Knob
{
    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(StartAngle),
            defaultValue: -240.0,
            coerce: CoerceStartAngle);

    /// <summary>
    /// Gets or sets the start angle in degree.
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
            defaultValue: 300.0,
            coerce: CoerceSweepAngle);

    /// <summary>
    /// Gets or sets the sweep angle in degree.
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

    #region IsPointerVisible Property

    /// <summary>
    /// Defines the <see cref="IsPointerVisible"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsPointerVisibleProperty =
        AvaloniaProperty.Register<Knob, bool>(
            nameof(IsPointerVisible),
            defaultValue: true);

    /// <summary>
    /// Gets or sets is pointer visible.
    /// </summary>
    public bool IsPointerVisible
    {
        get => GetValue(IsPointerVisibleProperty);
        set => SetValue(IsPointerVisibleProperty, value);
    }

    #endregion

    #region PointerThickness Property

    /// <summary>
    /// Defines the <see cref="PointerThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> PointerThicknessProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(PointerThickness),
            defaultValue: 3.0,
            coerce: CoercePointerThickness);

    /// <summary>
    /// Gets or sets the thickness of the pointer.
    /// </summary>
    public double PointerThickness
    {
        get => GetValue(PointerThicknessProperty);
        set => SetValue(PointerThicknessProperty, value);
    }

    private static double CoercePointerThickness(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoercePointerThickness(value);

        return value;
    }

    #endregion

    #region ArcThickness Property

    /// <summary>
    /// Defines the <see cref="ArcThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ArcThicknessProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(ArcThickness),
            defaultValue: 3.0,
            coerce: CoerceArcThickness);

    /// <summary>
    /// Gets or sets the thickness of the arc.
    /// </summary>
    public double ArcThickness
    {
        get => GetValue(ArcThicknessProperty);
        set => SetValue(ArcThicknessProperty, value);
    }

    private static double CoerceArcThickness(AvaloniaObject sender, double value)
    {
        if (sender is Knob knob)
            return knob.CoerceArcThickness(value);

        return value;
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

    #region MinMaxTicksSize Property

    /// <summary>
    /// Defines the <see cref="MinMaxTicksSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinMaxTicksSizeProperty =
        KnobTickBar.MinMaxTicksSizeProperty.AddOwner<Knob>();

    /// <summary>
    /// Gets or sets the size of min and max ticks.
    /// </summary>
    public double MinMaxTicksSize
    {
        get => GetValue(MinMaxTicksSizeProperty);
        set => SetValue(MinMaxTicksSizeProperty, value);
    }

    #endregion

    #region TicksSize Property

    /// <summary>
    /// Defines the <see cref="TicksSize"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TicksSizeProperty =
        KnobTickBar.TicksSizeProperty.AddOwner<Knob>();

    /// <summary>
    /// Gets or sets the size of ticks.
    /// </summary>
    public double TicksSize
    {
        get => GetValue(TicksSizeProperty);
        set => SetValue(TicksSizeProperty, value);
    }

    #endregion

    #region TicksThickness Property

    /// <summary>
    /// Defines the <see cref="TicksThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TicksThicknessProperty =
        KnobDecoratorBase.DecoratorThicknessProperty.AddOwner<Knob>();

    /// <summary>
    /// Gets or sets the thickness of ticks.
    /// </summary>
    public double TicksThickness
    {
        get => GetValue(TicksThicknessProperty);
        set => SetValue(TicksThicknessProperty, value);
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
    /// Gets the pointer level sweep angle.
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
    /// Gets the pointer start angle.
    /// </summary>
    public double PointerStartAngle => _pointerStartAngle;

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

    /// <summary>
    /// Called when the <see cref="PointerThickness"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoercePointerThickness(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : PointerThickness;
    }

    /// <summary>
    /// Called when the <see cref="ArcThickness"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceArcThickness(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue >= 3
            ? baseValue
            : ArcThickness;
    }
}