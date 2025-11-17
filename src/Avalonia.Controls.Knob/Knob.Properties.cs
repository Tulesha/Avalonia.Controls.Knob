using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Controls.Helpers;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Utilities;

namespace Avalonia.Controls;

public partial class Knob
{
    #region ClipValueToMinMax Property

    /// <summary>
    /// Defines the <see cref="ClipValueToMinMax"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> ClipValueToMinMaxProperty =
        AvaloniaProperty.Register<Knob, bool>(
            nameof(ClipValueToMinMax));

    /// <summary>
    /// Gets or sets if the value should be clipped when minimum/maximum is reached.
    /// </summary>
    public bool ClipValueToMinMax
    {
        get => GetValue(ClipValueToMinMaxProperty);
        set => SetValue(ClipValueToMinMaxProperty, value);
    }

    #endregion

    #region NumberFormat Property

    /// <summary>
    /// Defines the <see cref="NumberFormat"/> property.
    /// </summary>
    public static readonly StyledProperty<NumberFormatInfo> NumberFormatProperty =
        AvaloniaProperty.Register<Knob, NumberFormatInfo>(
            nameof(NumberFormat),
            NumberFormatInfo.CurrentInfo);

    /// <summary>
    /// Gets or sets the current NumberFormatInfo
    /// </summary>
    public NumberFormatInfo NumberFormat
    {
        get => GetValue(NumberFormatProperty);
        set => SetValue(NumberFormatProperty, value);
    }

    /// <summary>
    /// Called when the <see cref="NumberFormat"/> property value changed.
    /// </summary>
    /// <param name="e">The event args.</param>
    private static void OnNumberFormatChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knob)
        {
            var oldValue = (NumberFormatInfo?)e.OldValue;
            var newValue = (NumberFormatInfo?)e.NewValue;
            knob.OnNumberFormatChanged(oldValue, newValue);
        }
    }

    #endregion

    #region FormatString Property

    /// <summary>
    /// Defines the <see cref="FormatString"/> property.
    /// </summary>
    public static readonly StyledProperty<string> FormatStringProperty =
        AvaloniaProperty.Register<Knob, string>(
            nameof(FormatString),
            string.Empty);

    /// <summary>
    /// Gets or sets the display format of the <see cref="Value"/>.
    /// </summary>
    public string FormatString
    {
        get => GetValue(FormatStringProperty);
        set => SetValue(FormatStringProperty, value);
    }

    /// <summary>
    /// Called when the <see cref="FormatString"/> property value changed.
    /// </summary>
    /// <param name="e">The event args.</param>
    private static void OnFormatStringChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knob)
        {
            var oldValue = (string?)e.OldValue;
            var newValue = (string?)e.NewValue;
            knob.OnFormatStringChanged(oldValue, newValue);
        }
    }

    #endregion

    #region Increment Property

    /// <summary>
    /// Defines the <see cref="Increment"/> property.
    /// </summary>
    public static readonly StyledProperty<double> IncrementProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Increment),
            defaultValue: 1.0d,
            coerce: OnCoerceIncrement);

    /// <summary>
    /// Gets or sets the amount in which to increment the <see cref="Value"/>.
    /// </summary>
    public double Increment
    {
        get => GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }

    private static double OnCoerceIncrement(AvaloniaObject instance, double value)
    {
        if (instance is Knob knob)
            return knob.OnCoerceIncrement(value);

        return value;
    }

    #endregion

    #region Maximum Property

    /// <summary>
    /// Defines the <see cref="Maximum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Maximum),
            defaultValue: 100,
            coerce: OnCoerceMaximum);

    /// <summary>
    /// Get or set the maximum value.
    /// </summary>
    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    private static double OnCoerceMaximum(AvaloniaObject instance, double value)
    {
        if (instance is Knob knob)
        {
            return knob.OnCoerceMaximum(value);
        }

        return value;
    }

    private static void OnMaximumChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knop)
        {
            var oldValue = (double)e.OldValue!;
            var newValue = (double)e.NewValue!;
            knop.OnMaximumChanged(oldValue, newValue);
        }
    }

    #endregion

    #region Minimum Property

    /// <summary>
    /// Defines the <see cref="Minimum"/> property.
    /// </summary>
    public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Minimum),
            defaultValue: 0,
            coerce: OnCoerceMinimum);

    /// <summary>
    /// Get or set the minimum value.
    /// </summary>
    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    private static double OnCoerceMinimum(AvaloniaObject instance, double value)
    {
        if (instance is Knob knob)
            return knob.OnCoerceMinimum(value);

        return value;
    }

    /// <summary>
    /// Called when the <see cref="Minimum"/> property value changed.
    /// </summary>
    /// <param name="e">The event args.</param>
    private static void OnMinimumChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knob)
        {
            var oldValue = (double)e.OldValue!;
            var newValue = (double)e.NewValue!;
            knob.OnMinimumChanged(oldValue, newValue);
        }
    }

    #endregion

    #region ParsingNumberStyle Property

    /// <summary>
    /// Defines the <see cref="ParsingNumberStyle"/> property.
    /// </summary>
    public static readonly StyledProperty<NumberStyles> ParsingNumberStyleProperty =
        AvaloniaProperty.Register<Knob, NumberStyles>(
            nameof(ParsingNumberStyle),
            NumberStyles.Any);

    /// <summary>
    /// Gets or sets the parsing style (AllowLeadingWhite, Float, AllowHexSpecifier, ...). By default, Any.
    /// Note that Hex style does not work with double. 
    /// For hexadecimal display, use <see cref="TextConverter"/>.
    /// </summary>
    public NumberStyles ParsingNumberStyle
    {
        get => GetValue(ParsingNumberStyleProperty);
        set => SetValue(ParsingNumberStyleProperty, value);
    }

    #endregion

    #region Text Property

    /// <summary>
    /// Defines the <see cref="Text"/> property.
    /// </summary>
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<Knob, string?>(
            nameof(Text),
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    /// <summary>
    /// Gets or sets the formatted string representation of the value.
    /// </summary>
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Called when the <see cref="Text"/> property value changed.
    /// </summary>
    /// <param name="e">The event args.</param>
    private static void OnTextChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knob)
        {
            var oldValue = (string?)e.OldValue;
            var newValue = (string?)e.NewValue;
            knob.OnTextChanged(oldValue, newValue);
        }
    }

    #endregion

    #region TextConverter Property

    /// <summary>
    /// Defines the <see cref="TextConverter"/> property.
    /// </summary>
    public static readonly StyledProperty<IValueConverter?> TextConverterProperty =
        AvaloniaProperty.Register<Knob, IValueConverter?>(
            nameof(TextConverter),
            defaultBindingMode: BindingMode.OneWay);

    /// <summary>
    /// Gets or sets the custom bidirectional Text-Value converter.
    /// Non-null converter overrides <see cref="ParsingNumberStyle"/>, providing finer control over 
    /// string representation of the underlying value.
    /// </summary>
    public IValueConverter? TextConverter
    {
        get => GetValue(TextConverterProperty);
        set => SetValue(TextConverterProperty, value);
    }

    #endregion

    #region Value Property

    /// <summary>
    /// Defines the <see cref="Value"/> property.
    /// </summary>
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(Value),
            coerce: OnCoerceValue,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    /// <summary>
    /// Get or set the current value.
    /// </summary>
    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private static double OnCoerceValue(AvaloniaObject instance, double value)
    {
        if (instance is Knob knob)
            return knob.OnCoerceValue(value);

        return value;
    }

    /// <summary>
    /// Called when the <see cref="Value"/> property value changed.
    /// </summary>
    /// <param name="e">The event args.</param>
    private static void OnValueChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is Knob knob)
        {
            var oldValue = (double)e.OldValue!;
            var newValue = (double)e.NewValue!;
            knob.OnValueChanged(oldValue, newValue);
        }
    }

    #endregion

    #region Watermark Property

    /// <summary>
    /// Defines the <see cref="Watermark"/> property.
    /// </summary>
    public static readonly StyledProperty<string?> WatermarkProperty =
        AvaloniaProperty.Register<Knob, string?>(
            nameof(Watermark));

    /// <summary>
    /// Gets or sets the object to use as a watermark if the <see cref="Value"/> is null.
    /// </summary>
    public string? Watermark
    {
        get => GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }

    #endregion

    #region StartAngle Property

    /// <summary>
    /// Defines the <see cref="StartAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(StartAngle),
            defaultValue: -150);

    /// <summary>
    /// Get or set the start angle in degree.
    /// </summary>
    public double StartAngle
    {
        get => GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    #endregion

    #region EndAngle Property

    /// <summary>
    /// Defines the <see cref="EndAngle"/> property.
    /// </summary>
    public static readonly StyledProperty<double> EndAngleProperty =
        AvaloniaProperty.Register<Knob, double>(
            nameof(EndAngle),
            defaultValue: 150);

    /// <summary>
    /// Get or set the end angle in degree.
    /// </summary>
    public double EndAngle
    {
        get => GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
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

    #region PointerEndAngle Property

    private double _pointerEndAngle;

    /// <summary>
    /// Defines the <see cref="PointerEndAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> PointerEndAngleProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(PointerEndAngle),
            o => o.PointerEndAngle);

    /// <summary>
    /// Get the pointer end angle.
    /// </summary>
    public double PointerEndAngle => _pointerEndAngle;

    #endregion

    #region LevelEndAngle Property

    private double _levelEndAngle;

    /// <summary>
    /// Defines the <see cref="LevelEndAngle"/> property.
    /// </summary>
    public static readonly DirectProperty<Knob, double> LevelEndAngleProperty =
        AvaloniaProperty.RegisterDirect<Knob, double>(
            nameof(LevelEndAngle),
            o => o.LevelEndAngle);

    /// <summary>
    /// Get the pointer level end angle.
    /// </summary>
    public double LevelEndAngle => _levelEndAngle;

    #endregion

    static Knob()
    {
        NumberFormatProperty.Changed.Subscribe(OnNumberFormatChanged);
        FormatStringProperty.Changed.Subscribe(OnFormatStringChanged);
        MaximumProperty.Changed.Subscribe(OnMaximumChanged);
        MinimumProperty.Changed.Subscribe(OnMinimumChanged);
        TextProperty.Changed.Subscribe(OnTextChanged);
        ValueProperty.Changed.Subscribe(OnValueChanged);

        IsTabStopProperty.OverrideDefaultValue<Knob>(false);
    }

    /// <summary>
    /// Called when the <see cref="Increment"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double OnCoerceIncrement(double baseValue)
    {
        return baseValue;
    }

    /// <summary>
    /// Called when the <see cref="Maximum"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double OnCoerceMaximum(double baseValue)
    {
        return Math.Max(baseValue, Minimum);
    }

    /// <summary>
    /// Called when the <see cref="Minimum"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    /// <returns></returns>
    protected virtual double OnCoerceMinimum(double baseValue)
    {
        return Math.Min(baseValue, Maximum);
    }

    /// <summary>
    /// Called when the <see cref="Value"/> property has to be coerced.
    /// </summary>
    /// <param name="baseValue">The value.</param>
    protected virtual double OnCoerceValue(double baseValue)
    {
        return baseValue;
    }

    /// <summary>
    /// Called when the <see cref="NumberFormat"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnNumberFormatChanged(NumberFormatInfo? oldValue, NumberFormatInfo? newValue)
    {
        if (IsInitialized)
        {
            SyncTextAndValueProperties(false, null);
        }
    }

    /// <summary>
    /// Called when the <see cref="FormatString"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnFormatStringChanged(string? oldValue, string? newValue)
    {
        if (IsInitialized)
        {
            SyncTextAndValueProperties(false, null, true);
        }
    }

    /// <summary>
    /// Called when the <see cref="Maximum"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnMaximumChanged(double oldValue, double newValue)
    {
        if (ClipValueToMinMax)
        {
            SetCurrentValue(ValueProperty, MathUtilities.Clamp(Value, Minimum, Maximum));
        }
    }

    /// <summary>
    /// Called when the <see cref="Minimum"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnMinimumChanged(double oldValue, double newValue)
    {
        if (ClipValueToMinMax)
        {
            SetCurrentValue(ValueProperty, MathUtilities.Clamp(Value, Minimum, Maximum));
        }
    }

    /// <summary>
    /// Called when the <see cref="Value"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnValueChanged(double oldValue, double newValue)
    {
        if (!_internalValueSet && IsInitialized)
        {
            SyncTextAndValueProperties(false, null, true);
        }

        RaiseValueChangedEvent(oldValue, newValue);
    }

    /// <summary>
    /// Called when the <see cref="Text"/> property value changed.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected virtual void OnTextChanged(string? oldValue, string? newValue)
    {
        if (IsInitialized)
        {
            SyncTextAndValueProperties(true, Text);
        }
    }

    /// <summary>
    /// Converts the formatted text to a value.
    /// </summary>
    private double ConvertTextToValue(string? text)
    {
        double result = 0;

        if (string.IsNullOrEmpty(text))
        {
            return result;
        }

        // Since the conversion from Value to text using a FormatString may not be parsable,
        // we verify that the already existing text is not the exact same value.
        var currentValueText = ConvertValueToText();
        if (Equals(currentValueText, text))
        {
            return Value;
        }

        result = ConvertTextToValueCore(currentValueText, text);

        if (ClipValueToMinMax)
        {
            return MathUtilities.Clamp(result, Minimum, Maximum);
        }

        ValidateMinMax(result);

        return result;
    }

    /// <summary>
    /// Converts the value to formatted text.
    /// </summary>
    /// <returns></returns>
    private string? ConvertValueToText()
    {
        if (TextConverter != null)
        {
            return TextConverter.ConvertBack(Value, typeof(string), null, CultureInfo.CurrentCulture)?.ToString();
        }

        //Manage FormatString of type "{}{0:N2} °" (in xaml) or "{0:N2} °" in code-behind.
        if (FormatString.Contains("{0"))
        {
            return string.Format(NumberFormat, FormatString, Value);
        }

        return Value.ToString(FormatString, NumberFormat);
    }

    private void SetValueInternal(double value)
    {
        _internalValueSet = true;
        try
        {
            SetCurrentValue(ValueProperty, value);
        }
        finally
        {
            _internalValueSet = false;
        }
    }

    /// <summary>
    /// Synchronize <see cref="Text"/> and <see cref="Value"/> properties.
    /// </summary>
    /// <param name="updateValueFromText">If value should be updated from text.</param>
    /// <param name="text">The text.</param>
    private bool SyncTextAndValueProperties(bool updateValueFromText, string? text)
    {
        return SyncTextAndValueProperties(updateValueFromText, text, false);
    }

    /// <summary>
    /// Synchronize <see cref="Text"/> and <see cref="Value"/> properties.
    /// </summary>
    /// <param name="updateValueFromText">If value should be updated from text.</param>
    /// <param name="text">The text.</param>
    /// <param name="forceTextUpdate">Force text update.</param>
    private bool SyncTextAndValueProperties(bool updateValueFromText, string? text, bool forceTextUpdate)
    {
        if (_isSyncingTextAndValueProperties)
            return true;

        _isSyncingTextAndValueProperties = true;
        var parsedTextIsValid = true;
        try
        {
            if (updateValueFromText)
            {
                try
                {
                    var newValue = ConvertTextToValue(text);
                    if (!Equals(newValue, Value))
                    {
                        SetValueInternal(newValue);
                        UpdateUi();
                    }
                }
                catch
                {
                    parsedTextIsValid = false;
                }
            }

            // Do not touch the ongoing text input from user.
            if (!_isTextChangedFromUi)
            {
                if (forceTextUpdate)
                {
                    var newText = ConvertValueToText();
                    if (!Equals(Text, newText))
                    {
                        SetCurrentValue(TextProperty, newText);
                        UpdateUi();
                    }
                }

                // Sync Text and textBox
                if (_textBox != null)
                {
                    _textBox.Text = Text;
                }
            }
        }
        finally
        {
            _isSyncingTextAndValueProperties = false;
        }

        return parsedTextIsValid;
    }

    private double ConvertTextToValueCore(string? currentValueText, string? text)
    {
        double result = 0;

        if (string.IsNullOrEmpty(text))
        {
            return result;
        }

        if (TextConverter != null)
        {
            var valueFromText = TextConverter.Convert(text, typeof(double?), null, CultureInfo.CurrentCulture);
            return (double?)valueFromText ?? 0;
        }

        if (IsPercent(FormatString))
        {
            result = ParsePercent(text, NumberFormat);
        }
        else
        {
            // Problem while converting new text
            if (!double.TryParse(text, ParsingNumberStyle, NumberFormat, out var outputValue))
            {
                var shouldThrow = true;

                // Check if CurrentValueText is also failing => it also contains special characters. ex : 90°
                if (!string.IsNullOrEmpty(currentValueText) &&
                    !double.TryParse(currentValueText, ParsingNumberStyle, NumberFormat, out var _))
                {
                    // extract non-digit characters
                    var currentValueTextSpecialCharacters = currentValueText.Where(c => !char.IsDigit(c));
                    var textSpecialCharacters = text.Where(c => !char.IsDigit(c)).ToArray();
                    // same non-digit characters on currentValueText and new text => remove them on new Text to parse it again.
                    if (!currentValueTextSpecialCharacters.Except(textSpecialCharacters).Any())
                    {
                        foreach (var character in textSpecialCharacters)
                        {
                            text = text.Replace(character.ToString(), string.Empty);
                        }

                        // if without the special characters, parsing is good, do not throw
                        if (double.TryParse(text, ParsingNumberStyle, NumberFormat, out outputValue))
                        {
                            shouldThrow = false;
                        }
                    }
                }

                if (shouldThrow)
                {
                    throw new InvalidDataException("Input string was not in a correct format.");
                }
            }

            result = outputValue;
        }

        return result;
    }

    private void ValidateMinMax(double? value)
    {
        if (!value.HasValue)
        {
            return;
        }

        if (value < Minimum)
        {
            throw new ArgumentOutOfRangeException(nameof(value),
                $"Value must be greater than Minimum value of {Minimum}");
        }
        else if (value > Maximum)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be less than Maximum value of {Maximum}");
        }
    }

    /// <summary>
    /// Parse percent format text
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="cultureInfo">The culture info.</param>
    private static double ParsePercent(string text, IFormatProvider? cultureInfo)
    {
        var info = NumberFormatInfo.GetInstance(cultureInfo);
        text = text.Replace(info.PercentSymbol, null);
        var result = double.Parse(text, NumberStyles.Any, info);
        result = result / 100;
        return result;
    }

    private bool IsPercent(string stringToTest)
    {
        var PIndex = stringToTest.IndexOf("P", StringComparison.Ordinal);
        if (PIndex >= 0)
        {
            //stringToTest contains a "P" between 2 "'", it's considered as text, not percent
            var isText = stringToTest.Substring(0, PIndex).Contains('\'')
                         && stringToTest.Substring(PIndex, FormatString.Length - PIndex).Contains('\'');

            return !isText;
        }

        return false;
    }
}