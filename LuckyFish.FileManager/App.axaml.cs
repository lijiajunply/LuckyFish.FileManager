using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LuckyFish.FileManager.ViewModels;
using LuckyFish.FileManager.Views;

namespace LuckyFish.FileManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Manager()
            {
                DataContext = new ManagerViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}