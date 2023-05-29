using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
        var rootData = (sender as Grid)!.DataContext as KeyValuePair<string, string>?;
        var data = DataContext as ManagerViewModel;
        //data?.PathManage(rootData!.Value.Value);
    }
    

    private void ProgressBarInit(object? sender, EventArgs e)
    {
        var progressBar = sender as ProgressBar;
        var rootData = progressBar!.DataContext as KeyValuePair<string, string>?;
        var drive = new DriveInfo(rootData!.Value.Value);
        progressBar.Maximum = drive.TotalSize;
        progressBar.Value = drive.AvailableFreeSpace;
    }

    private void RemoveCommonTapped(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Control)!.DataContext as DirectoryInfo;
        (DataContext as ManagerViewModel)!.RemoveCommon(data!.FullName);
    }
    
    private void CommonTapped(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Control)!.DataContext as DirectoryInfo;
        //(DataContext as ManagerViewModel)!.PathManage(data.FullName);
    }
    #endregion

}