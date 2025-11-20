using Avalonia;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Reactive;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobBase
{
    #region Value Property

    /// <summary>
    /// Defines the <see cref="Value"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<KnobBase, double>(
            nameof(Value),
            defaultBindingMode: BindingMode.TwoWay,
            coerce: CoerceValue);

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private static double CoerceValue(AvaloniaObject sender, double value)
    {
        if (sender is KnobBase knobBase)
            return knobBase.CoerceValue(value);

        return value;
    }

    #endregion

    #region SmallChange Property

    /// <summary>
    /// Defines the <see cref="SmallChange"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SmallChangeProperty =
        AvaloniaProperty.Register<KnobBase, double>(
            nameof(SmallChange),
            defaultValue: 1);

    /// <summary>
    /// Gets or sets the small increment value added or subtracted from the <see cref="Value"/>.
    /// </summary>
    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    #endregion

    #region LargeChange Property

    /// <summary>
    /// Defines the <see cref="LargeChange"/> property.
    /// </summary>
    public static readonly StyledProperty<double> LargeChangeProperty =
        AvaloniaProperty.Register<KnobBase, double>(
            nameof(LargeChange),
            defaultValue: 10);

    /// <summary>
    /// Gets or sets the large increment value added or subtracted from the <see cref="Value"/>.
    /// </summary>
    public double LargeChange
    {
        get => GetValue(LargeChangeProperty);
        set => SetValue(LargeChangeProperty, value);
    }

    #endregion

    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<KnobBase, double>(
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
        if (sender is KnobBase knobBase)
            return knobBase.CoerceStartAngle(value);

        return value;
    }

    #endregion

    #region LevelSweepAngle Property

    private double _levelSweepAngle;

    /// <summary>
    /// Defines the <see cref="LevelSweepAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<KnobBase, double> LevelSweepAngleProperty =
        AvaloniaProperty.RegisterDirect<KnobBase, double>(
            nameof(LevelSweepAngle),
            o => o.LevelSweepAngle);

    /// <summary>
    /// Gets the pointer level sweep angle.
    /// </summary>
    public double LevelSweepAngle => _levelSweepAngle;

    /// <summary>
    /// Sets the pointer level sweep angle.
    /// </summary>
    /// <param name="value">Value</param>
    protected void SetAndRaiseLevelSweepAngle(double value)
    {
        SetAndRaise(LevelSweepAngleProperty, ref _levelSweepAngle, value);
    }

    #endregion

    #region PointerStartAngle Property

    private double _pointerStartAngle;

    /// <summary>
    /// Defines the <see cref="PointerStartAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<KnobBase, double> PointerStartAngleProperty =
        AvaloniaProperty.RegisterDirect<KnobBase, double>(
            nameof(PointerStartAngle),
            o => o.PointerStartAngle);

    /// <summary>
    /// Gets the pointer start angle.
    /// </summary>
    public double PointerStartAngle => _pointerStartAngle;

    /// <summary>
    /// Sets the pointer start angle.
    /// </summary>
    /// <param name="value">Value</param>
    protected void SetAndRaisePointerStartAngle(double value)
    {
        SetAndRaise(PointerStartAngleProperty, ref _pointerStartAngle, value);
    }

    #endregion

    #region PointerThickness Property

    /// <summary>
    /// Defines the <see cref="PointerThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> PointerThicknessProperty =
        AvaloniaProperty.Register<KnobBase, double>(
            nameof(PointerThickness),
            defaultValue: 3.0);

    /// <summary>
    /// Gets or sets the thickness of the pointer.
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
        AvaloniaProperty.Register<KnobBase, bool>(
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

    #region TickFrequency Property

    /// <summary>
    /// Defines the <see cref="TickFrequency"/> property.
    /// </summary>
    public static readonly StyledProperty<double> TickFrequencyProperty =
        KnobTickBarBase.TickFrequencyProperty.AddOwner<KnobBase>();

    /// <summary>
    /// Gets or sets the interval between tick marks.
    /// </summary>
    public double TickFrequency
    {
        get => GetValue(TickFrequencyProperty);
        set => SetValue(TickFrequencyProperty, value);
    }

    #endregion

    #region IsSnapToTickEnabled Property

    /// <summary>
    /// Defines the <see cref="IsSnapToTickEnabled"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsSnapToTickEnabledProperty =
        AvaloniaProperty.Register<KnobBase, bool>(
            nameof(IsSnapToTickEnabled));

    /// <summary>
    /// Gets or sets a value that indicates whether the <see cref="KnobBase"/> automatically moves the pointer to the closest tick mark.
    /// </summary>
    public bool IsSnapToTickEnabled
    {
        get => GetValue(IsSnapToTickEnabledProperty);
        set => SetValue(IsSnapToTickEnabledProperty, value);
    }

    #endregion

    #region HeaderValueTemplate Property

    /// <summary>
    /// Defines the <see cref="HeaderValueTemplate"/> property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> HeaderValueTemplateProperty =
        AvaloniaProperty.Register<KnobBase, IDataTemplate?>(
            nameof(HeaderValueTemplate));

    /// <summary>
    /// Gets or sets the template for header control.
    /// </summary>
    public IDataTemplate? HeaderValueTemplate
    {
        get => GetValue(HeaderValueTemplateProperty);
        set => SetValue(HeaderValueTemplateProperty, value);
    }

    #endregion

    #region HeaderValueHorizontalContentAlignment Property

    /// <summary>
    /// Defines the <see cref="HeaderValueHorizontalContentAlignment"/> property.
    /// </summary>
    public static readonly StyledProperty<HorizontalAlignment> HeaderValueHorizontalContentAlignmentProperty =
        AvaloniaProperty.Register<KnobBase, HorizontalAlignment>(
            nameof(HeaderValueHorizontalContentAlignment),
            defaultValue: HorizontalAlignment.Stretch);

    /// <summary>
    /// Gets or sets the horizontal alignment of content in header section.
    /// </summary>
    public HorizontalAlignment HeaderValueHorizontalContentAlignment
    {
        get => GetValue(HeaderValueHorizontalContentAlignmentProperty);
        set => SetValue(HeaderValueHorizontalContentAlignmentProperty, value);
    }

    #endregion

    #region HeaderValueVerticalContentAlignment Property

    /// <summary>
    /// Defines the <see cref="HeaderValueVerticalContentAlignment"/> property.
    /// </summary>
    public static readonly StyledProperty<VerticalAlignment> HeaderValueVerticalContentAlignmentProperty =
        AvaloniaProperty.Register<KnobBase, VerticalAlignment>(
            nameof(HeaderValueVerticalContentAlignment),
            defaultValue: VerticalAlignment.Stretch);

    /// <summary>
    /// Gets or sets the vertical alignment of control in header section.
    /// </summary>
    public VerticalAlignment HeaderValueVerticalContentAlignment
    {
        get => GetValue(HeaderValueVerticalContentAlignmentProperty);
        set => SetValue(HeaderValueVerticalContentAlignmentProperty, value);
    }

    #endregion

    #region HeaderValuePlacement Property

    /// <summary>
    /// Defines the <see cref="HeaderValuePlacement"/> property.
    /// </summary>
    public static readonly StyledProperty<KnobHeaderPlacement> HeaderValuePlacementProperty =
        AvaloniaProperty.Register<KnobBase, KnobHeaderPlacement>(
            nameof(HeaderValuePlacement),
            defaultValue: KnobHeaderPlacement.Bottom);

    /// <summary>
    /// Gets or sets the placement of header control.
    /// </summary>
    public KnobHeaderPlacement HeaderValuePlacement
    {
        get => GetValue(HeaderValuePlacementProperty);
        set => SetValue(HeaderValuePlacementProperty, value);
    }

    #endregion

    #region IsHeaderValueVisible Property

    /// <summary>
    /// Defines the <see cref="IsHeaderValueVisible"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsHeaderValueVisibleProperty =
        AvaloniaProperty.Register<KnobBase, bool>(
            nameof(IsHeaderValueVisible),
            defaultValue: true);

    /// <summary>
    /// Gets or sets the visibility of header control.
    /// </summary>
    public bool IsHeaderValueVisible
    {
        get => GetValue(IsHeaderValueVisibleProperty);
        set => SetValue(IsHeaderValueVisibleProperty, value);
    }

    #endregion

    protected static void AffectsRecalculateAngles<T>(params AvaloniaProperty[] properties)
        where T : KnobBase
    {
        var invalidateObserver =
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs>(static e => (e.Sender as T)?.RecalculateAngles());

        foreach (var property in properties)
        {
            property.Changed.Subscribe(invalidateObserver);
        }
    }

    static KnobBase()
    {
        PressedMixin.Attach<KnobBase>();
        FocusableProperty.OverrideDefaultValue<KnobBase>(true);

        AffectsRecalculateAngles<KnobBase>(ValueProperty,
            StartAngleProperty,
            PointerThicknessProperty);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ValueProperty)
        {
            var valueChangedEventArgs = new KnobBaseValueChangedEventArgs(
                change.GetOldValue<double>(),
                change.GetNewValue<double>(),
                ValueChangedEvent);
            RaiseEvent(valueChangedEventArgs);
        }
    }

    /// <summary>
    /// Called when the <see cref="Value"/> property has to be coerced
    /// </summary>
    /// <param name="baseValue"></param>
    /// <returns></returns>
    protected virtual double CoerceValue(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue)
            ? baseValue
            : Value;
    }

    /// <summary>
    /// Called when the <see cref="StartAngle"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceStartAngle(double baseValue)
    {
        return CoerceHelpers.CoerceStartAngle(baseValue, StartAngle);
    }
}