using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LuckyFish.FileManager.Models;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class Manager : Window
{
    public Manager()
    {
        InitializeComponent();
    }

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    #region Left

    private void RootItemTapped(object? sender, RoutedEventArgs e)
    {
        if (DataContext is ManagerViewModel data &&
            (sender as Control)!.DataContext is DriveSimpleOperation rootData)
            data.FilePath(rootData.Name);
    }


    private void RemoveCommonTapped(object? sender, RoutedEventArgs e)
    {
        /*var data = (sender as Control)!.DataContext as DirectoryInfo;
        (DataContext as ManagerViewModel)!.RemoveCommon(data!.FullName);*/
    }

    private void CommonTapped(object? sender, RoutedEventArgs e)
    {
        if (DataContext is ManagerViewModel data &&
            (sender as Control)!.DataContext is DirectoryOperation directory)
            data.FilePath(directory.Path);
    }

    #endregion

    public void More(IFileSystem system)
    {
        var a = new DetailView
        {
            DataContext = system
        };
        a.Show();
    }
}