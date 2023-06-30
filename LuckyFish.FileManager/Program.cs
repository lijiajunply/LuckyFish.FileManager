using Avalonia;
using Avalonia.ReactiveUI;
using System;
using LuckyFish.FileManager.Views;

namespace LuckyFish.FileManager;

abstract class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }


    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}