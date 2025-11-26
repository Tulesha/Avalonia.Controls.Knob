# KnobControl.Avalonia

[![NuGet](https://img.shields.io/nuget/v/Tulesha.KnobControl.Avalonia)](https://www.nuget.org/packages/Tulesha.KnobControl.Avalonia) [![downloads](https://img.shields.io/nuget/dt/Tulesha.KnobControl.Avalonia)](https://www.nuget.org/packages/Tulesha.KnobControl.Avalonia)

## Knob

A customizable `Knob` control for `Avalonia UI` framework that provides a circular slider interface. The control
supports
angular dragging,
click-to-position functionality, keyboard navigation, mouse wheel interaction, and tick-based positioning. Perfect for
audio controls,
settings panels, and any scenario where you need a circular value selection interface.

### API

| PROPERTY NAME                         | TYPE                    | DESCRIPTION                                                                                                         |
|---------------------------------------|-------------------------|---------------------------------------------------------------------------------------------------------------------|
| Value                                 | `double`                | Inherited from RangeBase - Gets or sets the current value of the knob                                               |
| Minimum                               | `double`                | Inherited from RangeBase - Gets or sets the minimum allowed value                                                   |
| Maximum                               | `double`                | Inherited from RangeBase - Gets or sets the maximum allowed value                                                   |
| SmallChange                           | `double`                | Inherited from RangeBase - Gets or sets the small change increment                                                  |
| LargeChange                           | `double`                | Inherited from RangeBase - Gets or sets the large change increment                                                  |
| IsHeaderValueVisible                  | `bool`                  | Inherited from KnobBase - Gets or sets the visibility of the header control (default: true)                         |
| HeaderValueTemplate                   | `IDataTemplate?`        | Inherited from KnobBase - Gets or sets the data template for the header value display                               |
| HeaderValueHorizontalContentAlignment | `HorizontalAlignment`   | Inherited from KnobBase - Gets or sets the horizontal alignment of content in the header section (default: Stretch) |
| HeaderValueVerticalContentAlignment   | `VerticalAlignment`     | Inherited from KnobBase - Gets or sets the vertical alignment of content in the header section (default: Stretch)   |
| HeaderValuePlacement                  | `KnobHeaderPlacement`   | Inherited from KnobBase - Gets or sets the placement of the header control (default: Bottom)                        |
| StartAngle                            | `double`                | Gets or sets the start angle in degrees for the knob's sweep range (default: -240)                                  |
| SweepAngle                            | `double`                | Gets or sets the sweep angle in degrees that defines the knob's angular range (default: 300)                        |
| IsPointerVisible                      | `bool`                  | Gets or sets whether the pointer indicator is visible (default: true)                                               |
| PointerThickness                      | `double`                | Gets or sets the thickness of the pointer indicator (default: 3.0)                                                  |
| ArcThickness                          | `double`                | Gets or sets the thckness of the arc indicator (default: 3.0)                                                       |
| TickFrequency                         | `double`                | Gets or sets the interval between tick marks for value snapping                                                     |
| Ticks                                 | `AvaloniaList<double>?` | Gets or sets a collection of custom tick values for precise positioning                                             |
| MinMaxTicksSize                       | `double`                | Gets or sets the size of the minimum and maximum ticks (default: 8.0)                                               |
| TicksSize                             | `double`                | Gets or sets the size of the ticks (default: 4.0)                                                                   |
| TicksThickness                        | `double`                | Gets or sets the thicknes of the ticks (default: 1.0)                                                               |
| IsSnapToTickEnabled                   | `bool`                  | Gets or sets whether the knob automatically snaps to the closest tick mark                                          |

### Demo

![](https://github.com/Tulesha/KnobControl.Avalonia/blob/main/workflows/KnobSample.gif)

## KnobCycle

`KnobCycle` is an extension of the original `Knob` that introduces **cyclic behavior**. There is no level arcs and looks
more like `gear`. Perfect for controls like oscilloscope knobs without stoppers.

### API

| PROPERTY NAME                         | TYPE                  | DESCRIPTION                                                                                                         |
|---------------------------------------|-----------------------|---------------------------------------------------------------------------------------------------------------------|
| Value                                 | `double`              | Inherited from RangeBase - Gets or sets the current value of the knob                                               |
| Minimum                               | `double`              | Inherited from RangeBase - Gets or sets the minimum allowed value                                                   |
| Maximum                               | `double`              | Inherited from RangeBase - Gets or sets the maximum allowed value                                                   |
| SmallChange                           | `double`              | Inherited from RangeBase - Gets or sets the small change increment                                                  |
| LargeChange                           | `double`              | Inherited from RangeBase - Gets or sets the large change increment                                                  |
| IsHeaderValueVisible                  | `bool`                | Inherited from KnobBase - Gets or sets the visibility of the header control (default: true)                         |
| HeaderValueTemplate                   | `IDataTemplate?`      | Inherited from KnobBase - Gets or sets the data template for the header value display                               |
| HeaderValueHorizontalContentAlignment | `HorizontalAlignment` | Inherited from KnobBase - Gets or sets the horizontal alignment of content in the header section (default: Stretch) |
| HeaderValueVerticalContentAlignment   | `VerticalAlignment`   | Inherited from KnobBase - Gets or sets the vertical alignment of content in the header section (default: Stretch)   |
| HeaderValuePlacement                  | `KnobHeaderPlacement` | Inherited from KnobBase - Gets or sets the placement of the header control (default: Bottom)                        |
| GripsDash                             | `double`              | Gets or sets the dash of the grips (default: 3.0)                                                                   |
| GripsThickness                        | `double`              | Gets or sets the thickness of the grips (default: 3.0)                                                              |

### Demo

![](https://github.com/Tulesha/KnobControl.Avalonia/blob/main/workflows/KnobCycleSample.gif)
