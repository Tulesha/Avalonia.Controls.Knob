using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace KnobControl.Avalonia.Demo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private double _value = 21.0;
    [ObservableProperty] private double _maximum = 100.0;
    [ObservableProperty] private double _minimum = -50.0;
    [ObservableProperty] private double _smallChange = 1.0;
    [ObservableProperty] private double _largeChange = 10.0;
    [ObservableProperty] private double _startAngle = -225.0;
    [ObservableProperty] private double _sweepAngle = 270.0;
    [ObservableProperty] private double _tickFrequency = 10.0;
    [ObservableProperty] private double _pointerThickness = 3.0;
    [ObservableProperty] private double _pointerSize = 10.0;
    [ObservableProperty] private double _arcThickness = 3.0;
    [ObservableProperty] private double _ticksThickness = 1.0;
    [ObservableProperty] private double _minMaxTicksSize = 8.0;
    [ObservableProperty] private double _ticksSize = 4.0;
    [ObservableProperty] private bool _isPointerVisible = true;
    [ObservableProperty] private bool _isSnapToTickEnabled;
    [ObservableProperty] private bool _isHeaderValueVisible = true;
    [ObservableProperty] private KnobHeaderPlacement _selectedHeaderValuePlacement = KnobHeaderPlacement.Bottom;

    [ObservableProperty] private double _valueEndless = 21.0;
    [ObservableProperty] private double _maximumEndless = 100.0;
    [ObservableProperty] private double _minimumEndless = -50.0;
    [ObservableProperty] private double _smallChangeEndless = 1.0;
    [ObservableProperty] private double _largeChangeEndless = 10.0;
    [ObservableProperty] private double _startAngleEndless = -225.0;
    [ObservableProperty] private double _sweepAngleEndless = 270.0;
    [ObservableProperty] private double _arcThicknessEndless = 3.0;
    [ObservableProperty] private double _arrowSizeEndless = 10.0;
    [ObservableProperty] private double _gripsDashEndless = 3.0;
    [ObservableProperty] private double _gripsThicknessEndless = 3.0;
    [ObservableProperty] private bool _isHeaderValueVisibleEndless = true;
    [ObservableProperty] private KnobHeaderPlacement _selectedHeaderValuePlacementEndless = KnobHeaderPlacement.Bottom;

    public IEnumerable<KnobHeaderPlacement> HeaderValuePlacements => Enum.GetValues<KnobHeaderPlacement>();
}