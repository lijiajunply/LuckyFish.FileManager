using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FileManager.Lib.FileModels;
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
            (sender as Control)!.DataContext is CommonModel commonModel)
            data.FilePath(commonModel.Path);
    }

    #endregion

    public static void More(IFileSystem system)
    {
        var a = new DetailView
        {
            DataContext = system
        };
        a.Show();
    }

    public async void AddFile(string path)
    {
        if(DataContext is not ManagerViewModel model)return;
        var a = new AddFileSystemView(path,true);
        var r = await a.ShowDialog<bool>(this);
        if(!r)return;
        model.ReInit();
    }
    
    public async void AddFolder(string path)
    {
        if(DataContext is not ManagerViewModel model)return;
        var a = new AddFileSystemView(path,false);
        var r = await a.ShowDialog<bool>(this);
        if(!r)return;
        model.ReInit();
    }

    private void SettingClick(object? sender, RoutedEventArgs e)
    {
        var a = new SettingView();
        a.ShowDialog(this);
    }
}