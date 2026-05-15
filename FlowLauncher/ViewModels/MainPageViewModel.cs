using FlowLauncher.Components.UI;
using FlowLauncher.Resources;
using FlowLauncher.Views;

namespace FlowLauncher.ViewModels;

public partial class MainPageViewModel : PageViewModel<MainPage>
{
    public MainPageViewModel() : base("main", Strings.PageTitleMain)
    {
        LeftMenuItems = [
            new MenuTitleViewModel
            {
                Title = "Title"
            },
            new MenuItemViewModel
            {
                Title = "Main",
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
