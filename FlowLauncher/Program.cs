using Avalonia;
using Avalonia.Controls;
using FlowNet.Core;

namespace FlowLauncher;

[Flow.Scope("app")]
static partial class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Task.Run(FlowInterops.Run);
        BuildAvaloniaApp().Start(WaitForTriggerAndStart, args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    private static readonly ManualResetEventSlim OnAvaloniaStart = new(false);
    private static readonly TaskCompletionSource OnAvaloniaEnd = new();
    private static readonly CancellationTokenSource OnAvaloniaEndTokenSource = new();

    private static void WaitForTriggerAndStart(Application app, string[] args)
    {
        OnAvaloniaStart.Wait();
        app.Run(OnAvaloniaEndTokenSource.Token);
        OnAvaloniaEnd.SetResult();
    }

    [Flow.Task]
    [Flow.Task("load:before")] [Flow.Run(After = "app")]
    [Flow.Task("load")] [Flow.Run(After = "app:load:before")]
    [Flow.Task("run:before")] [Flow.Run(After = "app:load")]
    [Flow.Task("stop:before")] [Flow.Run(After = "app:run")]
    [Flow.Task("stop")] [Flow.Run(After = "app:stop:before")]
    private static Task _() => Task.CompletedTask;

    [Flow.Task] [Flow.Run(After = "app:run:before")]
    private static async Task Run()
    {
        OnAvaloniaStart.Set();
        await OnAvaloniaEnd.Task.ConfigureAwait(false);
    }

    [Flow.Task("func:stop")]
    private static async Task StopAvaloniaAsync()
    {
        await OnAvaloniaEndTokenSource.CancelAsync().ConfigureAwait(false);
    }

    [Flow.Task("func:exit")] [Flow.Run(After = "app:stop")]
    private static void EndProgram()
    {
        Environment.Exit(0);
    }
}
