using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Controls.Knob.Demo.ViewModels;

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
}