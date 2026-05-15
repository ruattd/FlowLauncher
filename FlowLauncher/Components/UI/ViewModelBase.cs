using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FlowLauncher.Components.UI;

public abstract class ViewModelBase : ObservableObject
{
    private Dictionary<string, Action>? _changingObservers = null;
    private Dictionary<string, Action>? _changedObservers = null;

    public void RegisterPropertyChanging(string propertyName, Action action)
    {
        (_changingObservers ??= [])[propertyName] = action;
    }

    public void RegisterPropertyChanged(string propertyName, Action action)
    {
        (_changedObservers ??= [])[propertyName] = action;
    }

    public void ActivatePropertyChanging(string propertyName)
    {
        if (_changingObservers == null) return;
        if (_changingObservers.TryGetValue(propertyName, out var action)) action();
    }

    public void ActivatePropertyChanged(string propertyName)
    {
        if (_changedObservers == null) return;
        if (_changedObservers.TryGetValue(propertyName, out var action)) action();
    }

    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);
        if (e.PropertyName == null) return;
        ActivatePropertyChanging(e.PropertyName);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if ( e.PropertyName == null) return;
        ActivatePropertyChanged(e.PropertyName);
    }
}
