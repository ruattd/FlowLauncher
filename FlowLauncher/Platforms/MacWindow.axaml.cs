using Avalonia.Controls;
using FlowLauncher.Views;
using FlowNet.Core;

namespace FlowLauncher.Platforms;

public partial class MacWindow : Window
{
    public MacWindow()
    {
        Content = new RootPage { ParentWindow = this };
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Flow.InvokeTask("app:func:stop");
    }
}
