using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;

namespace FlowLauncher.Controls;

public class FlowButton : FlowClickable
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<FlowButton, string>(nameof(Text));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<Geometry?> IconProperty =
        AvaloniaProperty.Register<FlowButton, Geometry?>(nameof(Icon));

    public Geometry? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<Thickness> IconMarginProperty =
        AvaloniaProperty.Register<FlowButton, Thickness>(nameof(IconMargin));

    public Thickness IconMargin
    {
        get => GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    public static readonly StyledProperty<double> IconWidthProperty =
        AvaloniaProperty.Register<FlowButton, double>(nameof(IconWidth));

    public double IconWidth
    {
        get => GetValue(IconWidthProperty);
        set => SetValue(IconWidthProperty, value);
    }

    public static readonly StyledProperty<IBrush?> PressingForegroundProperty =
        AvaloniaProperty.Register<FlowButton, IBrush?>(nameof(PressingForeground));

    public IBrush? PressingForeground
    {
        get => GetValue(PressingForegroundProperty);
        set => SetValue(PressingForegroundProperty, value);
    }

    public static readonly StyledProperty<HorizontalAlignment> TextAlignmentProperty =
        AvaloniaProperty.Register<FlowButton, HorizontalAlignment>(nameof(TextAlignment), HorizontalAlignment.Center);

    public HorizontalAlignment TextAlignment
    {
        get => GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public static readonly StyledProperty<IControlTemplate?> RightAreaTemplateProperty =
        AvaloniaProperty.Register<FlowButton, IControlTemplate?>(nameof(RightAreaTemplate));

    public IControlTemplate? RightAreaTemplate
    {
        get => GetValue(RightAreaTemplateProperty);
        set => SetValue(RightAreaTemplateProperty, value);
    }
}
