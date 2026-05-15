namespace FlowLauncher.Components.UI;

public abstract class PageContentViewModel : ViewModelBase
{
    private static readonly Dictionary<Type, PageContent> _ViewCache = [];

    protected static PageContent GetViewCacheOrCreate<TViewControl>()
        where TViewControl : PageContent, new()
    {
        var type = typeof(TViewControl);
        if (!_ViewCache.TryGetValue(type, out var control))
            _ViewCache[type] = control = new TViewControl();
        return control;
    }

    public abstract PageContent ViewControl { get; }

    public ViewModelBase ViewModel { get; protected init; }

    protected PageContentViewModel()
    {
        ViewModel = this;
    }
}

public abstract class PageContentViewModel<TPage> : PageContentViewModel
    where TPage : PageViewModel
{
    public required TPage Page { get; init; }
}

public abstract class PageContentViewModel<TPage, TContent> : PageContentViewModel<TPage>
    where TPage : PageViewModel
    where TContent : PageContent, new()
{
    public override PageContent ViewControl { get; } = GetViewCacheOrCreate<TContent>();
}
