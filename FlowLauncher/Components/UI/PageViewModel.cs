using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using FlowLauncher.Platforms;

namespace FlowLauncher.Components.UI;

public abstract partial class PageViewModel(string id, string title = "Untitled") : ViewModelBase
{
    public static RootLayoutViewModel RootLayout => BaseWindow.RootLayout;

    public string Id { get; } = id;

    public string Title { get; protected set => SetProperty(ref field, value); } = title;

    [ObservableProperty]
    public partial Collection<MenuItemViewModel> LeftMenuItems { get; set; } = new ObservableCollection<MenuItemViewModel>();

    public PageContent? LeftExtraContent { get; init; } = null;

    [ObservableProperty]
    public partial PageContentViewModel? Content { get; set; } = null;

    protected static Geometry? Icon(string resourceKey)
    {
        if (Application.Current?.Resources is not { } res) return null;
        if (!res.TryGetValue(resourceKey, out var value)) return null;
        return value as Geometry;
    }

    protected PageContentViewModel<TThisClass> PageContent<TThisClass, TPageContent>()
        where TThisClass : PageViewModel
        where TPageContent : PageContentViewModel<TThisClass>, new()
    {
        return this is TThisClass page ? new TPageContent { Page = page }
            : throw new InvalidCastException("Type mismatch: please use current class as the first type parameter.");
    }

    protected PageContentViewModel PageContent<TPageContent>()
        where TPageContent : PageContent, new()
    {
        var view = new TPageContent();
        return new PageContentViewModelUsingPageViewModel(view, this);
    }
}

public abstract class PageViewModel<TThisClass, TMainContent> : PageViewModel
    where TThisClass : PageViewModel
    where TMainContent : PageContentViewModel<TThisClass>, new()
{
    protected PageViewModel(string id, string title = "Untitled") : base(id, title)
    {
        Content = PageContent<TThisClass, TMainContent>();
    }
}

public abstract class PageViewModel<TMainContent> : PageViewModel
    where TMainContent : PageContent, new()
{
    protected PageViewModel(string id, string title = "Untitled") : base(id, title)
    {
        Content = PageContent<TMainContent>();
    }
}

file class PageContentViewModelUsingPageViewModel : PageContentViewModel
{
    public override PageContent ViewControl { get; }

    public PageContentViewModelUsingPageViewModel(PageContent view, PageViewModel viewModel)
    {
        ViewControl = view;
        ViewModel = viewModel;
    }
}
