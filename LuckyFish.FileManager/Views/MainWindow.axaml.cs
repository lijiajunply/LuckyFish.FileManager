using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
        var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
    }

    private void RootItemTapped(object? sender, RoutedEventArgs e)
    {
        var rootData = (sender as Grid)!.DataContext as KeyValuePair<string, string>?;
        var data = DataContext as MainWindowViewModel;
        data.Model.PathManage(rootData.Value.Value);
    }

    private void CommonTapped(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Grid).DataContext as DirectoryInfo;
        (DataContext as MainWindowViewModel).Model.PathManage(data.FullName);
    }

    private void ProgressBarInit(object? sender, EventArgs e)
    {
        var progressBar = sender as ProgressBar;
        var rootData = progressBar.DataContext as KeyValuePair<string, string>?;
        var drive = new DriveInfo(rootData.Value.Value);
        progressBar.Maximum = drive.TotalSize;
        progressBar.Value = drive.AvailableFreeSpace;
    }
}