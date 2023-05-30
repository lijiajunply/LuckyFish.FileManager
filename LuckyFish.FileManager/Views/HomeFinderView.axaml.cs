using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LuckyFish.FileManager.Views;

public partial class HomeFinderView : UserControl
{
    public HomeFinderView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}