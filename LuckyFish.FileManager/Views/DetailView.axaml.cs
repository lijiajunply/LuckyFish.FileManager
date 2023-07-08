using Avalonia;
using Avalonia.Controls;

namespace LuckyFish.FileManager.Views;

public partial class DetailView : Window
{
    public DetailView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

}