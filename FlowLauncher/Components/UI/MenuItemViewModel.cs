using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FlowLauncher.Components.UI;

public partial class MenuItemViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial bool IsEnabled { get; set; } = true;

    [ObservableProperty]
    public partial string Title { get; set; } = "";

    [ObservableProperty]
    public partial Geometry? Icon { get; set; } = null;

    [ObservableProperty]
    public partial string? ToolTip { get; set; } = null;

    [ObservableProperty]
    public partial ICommand? Command { get; set; } = null;

    [ObservableProperty]
    public partial object? CommandParameter { get; set; } = null;

    [ObservableProperty]
    public partial PageContentViewModel? TargetContent { get; set; } = null;
}
