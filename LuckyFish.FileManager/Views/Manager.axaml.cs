using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class Manager : UserControl
{
    public Manager()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private void InputElement_OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
        var listdata = (sender as Grid).DataContext as FileSystemInfo;
        if (listdata is FileInfo)
            Process.Start(new ProcessStartInfo(){FileName = listdata.FullName,UseShellExecute = true});
        else
        {
            var dic = listdata as DirectoryInfo;
            var data = DataContext as ManagerViemModel;
            data.FilePath = listdata.FullName;
            data.Files = dic.GetFileSystemInfos();
        }
    }

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        var data = DataContext as ManagerViemModel;
        if (e.Key == Key.Enter)
        {
            FileSystemInfo listdata = IsDir(data.FilePath) ? new DirectoryInfo(data.FilePath) : new FileInfo(data.FilePath);
            if (listdata is FileInfo)
                Process.Start(new ProcessStartInfo(){FileName = listdata.FullName,UseShellExecute = true});
            else
            {
                var dic = listdata as DirectoryInfo;
                data.FilePath = listdata.FullName;
                data.Files = dic.GetFileSystemInfos();
            }
        }
    }
    private static bool IsDir(string filepath)
    {
        FileInfo fi = new FileInfo(filepath);
        return (fi.Attributes & FileAttributes.Directory) != 0;
    }
}