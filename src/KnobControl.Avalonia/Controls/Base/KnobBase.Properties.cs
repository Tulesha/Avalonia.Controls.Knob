using Avalonia;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Templates;
using Avalonia.Layout;

namespace KnobControl.Avalonia;

public partial class KnobBase
{
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
}