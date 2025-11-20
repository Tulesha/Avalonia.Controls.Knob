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
    [ObservableProperty] private double _pointerThickness = 3.0;
    [ObservableProperty] private double _tickFrequency = 10.0;
    [ObservableProperty] private bool _isPointerVisible = true;
    [ObservableProperty] private bool _isSnapToTickEnabled;
    [ObservableProperty] private bool _isHeaderValueVisible = true;
    [ObservableProperty] private KnobHeaderPlacement _selectedHeaderValuePlacement = KnobHeaderPlacement.Bottom;

    public IEnumerable<KnobHeaderPlacement> HeaderValuePlacements => Enum.GetValues<KnobHeaderPlacement>();
}