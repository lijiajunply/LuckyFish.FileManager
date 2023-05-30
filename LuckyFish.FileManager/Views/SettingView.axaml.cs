using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LuckyFish.FileManager.Views;

public partial class SettingView : Window
{
    public SettingView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}