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
#if DEBUG
        Flow.EnableTaskInvokingInfo = true;
#endif
        Task.Run(FlowInterops.Run).ContinueWith(task =>
        {
            if (task.Exception != null) throw task.Exception;
        });
        BuildAvaloniaApp().Start(AppMain, args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    private static readonly ManualResetEventSlim OnAvaloniaStart = new();
    private static readonly TaskCompletionSource OnAvaloniaEnd = new();
    private static readonly ManualResetEventSlim OnProgramEnd = new();
    private static readonly TaskCompletionSource OnProgramExit = new();
    private static readonly CancellationTokenSource OnAvaloniaEndTokenSource = new();

    private static void AppMain(Application app, string[] args)
    {
        OnAvaloniaStart.Wait();
        app.Run(OnAvaloniaEndTokenSource.Token);
        OnAvaloniaEnd.SetResult();
        OnProgramEnd.Wait();
        OnProgramExit.SetResult();
        Environment.Exit(0);
    }

    [Flow.Task] [Flow.Run]
    [Flow.Task("load:before")] [Flow.Run(After = "app")]
    [Flow.Task("load")] [Flow.Run(After = "app:load:before")]
    [Flow.Task("run:before")] [Flow.Run(After = "app:load")]
    [Flow.Task("stop:before")] [Flow.Run(After = "app:run")]
    [Flow.Task("stop")] [Flow.Run(After = "app:stop:before")]
    [Flow.Task("exit:before")] [Flow.Run(After = "app:stop")]
    [Flow.Task("exit")] [Flow.Run(After = "app:exit:before")]
    private static Task _([Flow.InvokingInfo] FlowTaskInvokingInfo
#if DEBUG
        info
#else
        _
#endif
    ) {
#if DEBUG
        var (target, _, callers) = info;
        Console.WriteLine($"Wildcard function '{target}' has been invoked by ['{string.Join("', '", callers)}']");
#endif
        return Task.CompletedTask;
    }

    [Flow.Task] [Flow.Run(After = "app:run:before")]
    private static async Task Run()
    {
        OnAvaloniaStart.Set();
        await OnAvaloniaEnd.Task.ConfigureAwait(false);
    }

    private static bool _isStopping = false;

    [Flow.Task("func:stop")]
    private static async Task StopAvaloniaAsync()
    {
        if (_isStopping)
        {
            await OnProgramExit.Task.ConfigureAwait(false);
            return;
        }
        _isStopping = true;
        Console.WriteLine("Stopping...");
        await OnAvaloniaEndTokenSource.CancelAsync().ConfigureAwait(false);
    }

    [Flow.Task("func:exit")] [Flow.Run(After = "app:exit")]
    private static async Task EndProgram()
    {
        Console.WriteLine("Exiting...");
        OnProgramEnd.Set();
        await OnProgramExit.Task.WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
        if (OnProgramExit.Task.IsCompleted) return;
        Console.WriteLine("Exit request timeout, force exiting...");
        Environment.Exit(1);
    }
}
