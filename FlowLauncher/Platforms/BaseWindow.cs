using Avalonia.Controls;
using FlowLauncher.ViewModels;
using FlowNet.Core;
using RootLayoutViewModel = FlowLauncher.Components.UI.RootLayoutViewModel;

namespace FlowLauncher.Platforms;

public class BaseWindow : Window
{
    public static RootLayoutViewModel RootLayout { get; } = new();

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Flow.InvokeTask("app:func:stop");
    }
}
