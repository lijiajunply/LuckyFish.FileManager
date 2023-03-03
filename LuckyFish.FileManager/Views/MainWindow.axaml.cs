using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RootItemTapped(object? sender, RoutedEventArgs e)
    {
        var rootdata = (sender as Grid)!.DataContext as KeyValuePair<string, string>?;
        var data = DataContext as MainWindowViewModel;
        data.Model.PathManage(rootdata.Value.Value);
    }
}