using Avalonia;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Shapes;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using KnobControl.Avalonia.Helpers;

namespace KnobControl.Avalonia;

public partial class KnobBase
{
    #region Stroke Property

    /// <summary>
    /// Defines the <see cref="Stroke"/> property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> StrokeProperty =
        Shape.StrokeProperty.AddOwner<KnobBase>();

    /// <summary>
    /// Gets or sets the brush for shape elements inside Knob base controls.
    /// </summary>
    public IBrush? Stroke
    {
        get => GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
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

    #region SweepAngle Property

    /// <summary>
    /// Defines the <see cref="SweepAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty =
        AvaloniaProperty.Register<KnobBase, double>(
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
        if (sender is KnobBase knobBase)
            return knobBase.CoerceSweepAngle(value);

        return value;
    }

    #endregion

    #region ArcThickness Property

    /// <summary>
    /// Defines the <see cref="ArcThickness"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ArcThicknessProperty =
        AvaloniaProperty.Register<KnobBase, double>(
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
        if (sender is KnobBase knobBase)
            return knobBase.CoerceArcThickness(value);

        return value;
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

    static KnobBase()
    {
        PressedMixin.Attach<KnobBase>();
        FocusableProperty.OverrideDefaultValue<KnobBase>(true);
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
    /// Called when the <see cref="ArcThickness"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double CoerceArcThickness(double baseValue)
    {
        return ValidateHelpers.ValidateDouble(baseValue) && baseValue > 0
            ? baseValue
            : ArcThickness;
    }
}