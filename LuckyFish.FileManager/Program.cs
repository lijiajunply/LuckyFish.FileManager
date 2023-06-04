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
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            var a = new MessageBox(e.Message);
            a.Show();
        }
    }


    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}