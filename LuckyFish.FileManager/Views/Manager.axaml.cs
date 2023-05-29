using System;
using System.Collections.Generic;
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
            (sender as Control)!.DataContext is DriveOperation rootData) 
            data.Finder.FilePath = rootData.Path;
    }
    

    private void RemoveCommonTapped(object? sender, RoutedEventArgs e)
    {
        /*var data = (sender as Control)!.DataContext as DirectoryInfo;
        (DataContext as ManagerViewModel)!.RemoveCommon(data!.FullName);*/
    }
    
    private void CommonTapped(object? sender, RoutedEventArgs e)
    {
        if (DataContext is ManagerViewModel data && 
            (sender as Control)!.DataContext is DirectoryOperation rootData) 
            data.Finder.FilePath = rootData.Path;
    }
    #endregion

}