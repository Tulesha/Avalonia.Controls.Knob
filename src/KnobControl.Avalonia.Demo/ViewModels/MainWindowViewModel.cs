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

    [ObservableProperty] private double _valueCycle = 21.0;
    [ObservableProperty] private double _maximumCycle = 100.0;
    [ObservableProperty] private double _minimumCycle = -50.0;
    [ObservableProperty] private double _smallChangeCycle = 1.0;
    [ObservableProperty] private double _largeChangeCycle = 10.0;
    [ObservableProperty] private double _startAngleCycle = -225.0;
    [ObservableProperty] private double _sweepAngleCycle = 270.0;
    [ObservableProperty] private double _arcThicknessCycle = 3.0;
    [ObservableProperty] private double _arrowSizeCycle = 10.0;
    [ObservableProperty] private double _gripsDashCycle = 3.0;
    [ObservableProperty] private double _gripsThicknessCycle = 3.0;
    [ObservableProperty] private bool _isHeaderValueVisibleCycle = true;
    [ObservableProperty] private KnobHeaderPlacement _selectedHeaderValuePlacementCycle = KnobHeaderPlacement.Bottom;

    public IEnumerable<KnobHeaderPlacement> HeaderValuePlacements => Enum.GetValues<KnobHeaderPlacement>();
}