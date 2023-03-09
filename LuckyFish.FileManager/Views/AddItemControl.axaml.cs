using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class AddItemControl : Window
{
    public AddItemControl()
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

    private void OkClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void CancelClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as AddItemControlViewModel).Context = "";
        Close();
    }
}