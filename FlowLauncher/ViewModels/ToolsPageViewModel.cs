using FlowLauncher.Components.UI;
using FlowLauncher.Resources;
using FlowLauncher.Views;

namespace FlowLauncher.ViewModels;

public partial class ToolsPageViewModel : PageViewModel<ToolsPage>
{
    public ToolsPageViewModel() : base("tools", Strings.PageTitleTools)
    {
        LeftMenuItems = [
            new MenuTitleViewModel
            {
                Title = "Title"
            },
            new MenuItemViewModel
            {
                Title = "MainLoooooooooooooooog",
                Icon = Icon("IconSettings"),
                TargetContent = Content
            },
            new MenuItemViewModel
            {
                Title = "456",
                Icon = Icon("IconSettings"),
                TargetContent = PageContent<MainPage>()
            },
            new MenuItemViewModel
            {
                Title = "789",
                Icon = Icon("IconSettings")
            },
        ];
    }
}
