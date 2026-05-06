using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using FlowNet.Core;

namespace FlowLauncher.Platforms;

public partial class WindowsWindow : Window
{
    public WindowsWindow()
    {
        InitializeComponent();
    }

    private void TitleBar_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        BackgroundPanel.RenderTransform = new ScaleTransform(1.0, 1.0);
        BackgroundPanel.Opacity = 1;
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Flow.InvokeTask("app:func:stop");
    }
}
