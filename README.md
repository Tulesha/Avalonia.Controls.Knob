# Avalonia.Controls.Knob

[![NuGet](https://img.shields.io/nuget/v/Tulesha.Avalonia.Control.Knob)](https://www.nuget.org/packages/Tulesha.Avalonia.Control.Knob) [![downloads](https://img.shields.io/nuget/dt/Tulesha.Avalonia.Control.Knob)](https://www.nuget.org/packages/Tulesha.Avalonia.Control.Knob)

## Description

A customizable Knob control for Avalonia UI framework that provides a circular slider interface. The control supports
angular dragging,
click-to-position functionality, keyboard navigation, mouse wheel interaction, and tick-based positioning. Perfect for
audio controls,
settings panels, and any scenario where you need a circular value selection interface.

## API

| PROPERTY NAME                         | TYPE                  | DESCRIPTION                                                                                  |
|---------------------------------------|-----------------------|----------------------------------------------------------------------------------------------|
| Value                                 | double                | Inherited from RangeBase - Gets or sets the current value of the knob                        |
| Minimum                               | double                | Inherited from RangeBase - Gets or sets the minimum allowed value                            |
| Maximum                               | double                | Inherited from RangeBase - Gets or sets the maximum allowed value                            |
| SmallChange                           | double                | Inherited from RangeBase - Gets or sets the small change increment                           |
| LargeChange                           | double                | Inherited from RangeBase - Gets or sets the large change increment                           |
| StartAngle                            | double                | Gets or sets the start angle in degrees for the knob's sweep range (default: -240)           |
| SweepAngle                            | double                | Gets or sets the sweep angle in degrees that defines the knob's angular range (default: 300) |
| IsPointerVisible                      | bool                  | Gets or sets whether the pointer indicator is visible (default: true)                        |
| PointerThickness                      | double                | Gets or sets the thickness of the pointer indicator (default: 3.0)                           |
| IsSnapToTickEnabled                   | bool                  | Gets or sets whether the knob automatically snaps to the closest tick mark                   |
| TickFrequency                         | double                | Gets or sets the interval between tick marks for value snapping                              |
| Ticks                                 | AvaloniaList<double>? | Gets or sets a collection of custom tick values for precise positioning                      |
| IsHeaderValueVisible                  | bool                  | Gets or sets the visibility of the header control (default: true)                            |
| HeaderValueTemplate                   | IDataTemplate?        | Gets or sets the data template for the header value display                                  |
| HeaderValueHorizontalContentAlignment | HorizontalAlignment   | Gets or sets the horizontal alignment of content in the header section (default: Stretch)    |
| HeaderValueVerticalContentAlignment   | VerticalAlignment     | Gets or sets the vertical alignment of content in the header section (default: Stretch)      |
| HeaderValuePlacement                  | KnobHeaderPlacement   | Gets or sets the placement of the header control (default: Bottom)                           |

## Demo

![](https://github.com/Tulesha/Avalonia.Controls.Knob/blob/master/workflows/KnobSample.gif)