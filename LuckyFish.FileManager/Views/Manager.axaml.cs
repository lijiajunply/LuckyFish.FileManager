using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FileManager.Lib;
using LuckyFish.FileManager.Serves;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class Manager : Window
{
    public Manager()
    {
        InitializeComponent();
    }

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    #region Right

    private void GridDoubleTapped(object? sender, RoutedEventArgs e)
    {
        var context = (sender as Grid).DataContext as FileSystemInfo;
        if (context is FileInfo)
            Process.Start(new ProcessStartInfo() { FileName = context.FullName, UseShellExecute = true });
        else
        {
            var data = DataContext as ManagerViewModel;
            data.PathManage(context.FullName);
        }
    }

    private void TextKeyDown(object? sender, KeyEventArgs e)
    {
        var data = DataContext as ManagerViewModel;
        if (e.Key == Key.Enter)
        {
            FileSystemInfo listdata =
                FileSystemOperation.IsDir(data.FilePath)
                    ? new DirectoryInfo(data.FilePath)
                    : new FileInfo(data.FilePath);
            if (listdata is FileInfo)
                Process.Start(new ProcessStartInfo() { FileName = listdata.FullName, UseShellExecute = true });
            else
                data.PathManage(data.FilePath);
        }
    }

    private void ImageInitialized(object? sender, EventArgs e)
    {
        var image = sender as Image;
        var data = image.DataContext as FileSystemInfo;
        image.Source = FileSystemOperation.IsDir(data.FullName) ? CodeServer.DirImage : CodeServer.FileImage;
    }

    private void LastClick(object? sender, RoutedEventArgs e)
    {
        var data = DataContext as ManagerViewModel;
        data.PathManage(data.Last.FullName);
    }

    private void ParentClick(object? sender, RoutedEventArgs e)
    {
        var data = DataContext as ManagerViewModel;
        data.PathManage(data.This.Parent.FullName);
    }

    private void SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var list = sender as ListBox;
        var data = DataContext as ManagerViewModel;
        data.Selection = list.Selection.SelectedItem as FileSystemInfo;
    }

    private void RightPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.MouseButton == MouseButton.Right)
            FlyoutBase.ShowAttachedFlyout(sender as Control);
    }

    private void LengthInitialized(object? sender, EventArgs e)
    {
        var text = sender as TextBlock;
        var data = text.DataContext as FileSystemInfo;
        if (data.Name == Path.GetPathRoot(data.FullName))
        {
            text.Text = "";
            return;
        }

        long size = FileSystemOperation.GetSize(data.FullName);
        text.Text = size <= -1 ? "" : size.ToString();
    }

    #endregion

    #region ItemFlyoutOperation

    private void CopyClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Control).DataContext as FileSystemInfo;
        var model = DataContext as ManagerViewModel;
        model.Mark = ("copy", data.FullName);
        DataContext = model;
    }

    private void CutClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Control).DataContext as FileSystemInfo;
        var model = DataContext as ManagerViewModel;
        model.Mark = ("cut", data.FullName);
        DataContext = model;
    }

    private void PasteClick(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as ManagerViewModel;
        if (model.Mark.operation == "copy")
            FileSystemOperation.Copy(model.Mark.path, model.This.FullName);
        else
            FileSystemOperation.Cut(model.Mark.path, model.This.FullName);
        if (model.Mark is not (null, null)) model.ReInit();
    }

    private void RenameClick(object? sender, RoutedEventArgs e)
    {
        var p = (sender as Control).Parent.Parent.Parent.Parent as Grid;
        var data = (sender as Control).DataContext as FileSystemInfo;
        var text = new TextBox() { Text = data.Name };
        text.KeyDown += RenameFinish;
        p.Children.RemoveAt(1);
        p.Children.Add(text);
        Grid.SetColumn(text, 1);
        //p.Children.Remove(text);
    }

    private void DeleteClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Control).DataContext as FileSystemInfo;
        FileSystemOperation.Delete(data.FullName);
        (DataContext as ManagerViewModel).ReInit();
    }

    private void RenameFinish(object? sender, KeyEventArgs e)
    {
        var text = sender as TextBox;
        var p = text!.Parent as Grid;
        if (e.Key == Key.Enter)
        {
            var data = p.DataContext as FileSystemInfo;
            FileSystemOperation.Rename(data!.FullName, text.Text);
            (DataContext as ManagerViewModel)!.ReInit();
            p.Children.Remove(text);
            var context = new TextBlock() { Text = text.Text };
            p.Children.Add(context);
            Grid.SetColumn(context, 1);
        }
    }

    private void AddCommonClick(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as ManagerViewModel;
        var data = (sender as Control)!.DataContext as FileSystemInfo;
        model.AddCommon(data!.FullName);
    }

    #endregion

    #region ListFlyoutOperation

    private async void AddFileTapped(object? sender, RoutedEventArgs e)
    {
        var model = new AddItemControlViewModel("Add File");
        var window = new AddItemControl() { DataContext = model };
        await window.ShowDialog(this);
        var name = model.Done();
        if(name == "")return;
        var dir = (DataContext as ManagerViewModel)!.This.FullName;
        FileSystemOperation.Create(dir, name, true);
        (DataContext as ManagerViewModel)!.ReInit();
    }

    private async void AddDirectoryTapped(object? sender, RoutedEventArgs e)
    {
        var model = new AddItemControlViewModel("Add Directory");
        var window = new AddItemControl() { DataContext = model };
        await window.ShowDialog(this);
        var name = model.Done();
        if(name == "")return;
        var dir = (DataContext as ManagerViewModel)!.This.FullName;
        FileSystemOperation.Create(dir, name, false);
        (DataContext as ManagerViewModel)!.ReInit();
    }

    private void OpenTerminalTapped(object? sender, RoutedEventArgs e)
    {
        
    }

    #endregion

    #region Left

    private void RootItemTapped(object? sender, RoutedEventArgs e)
    {
        var rootData = (sender as Grid)!.DataContext as KeyValuePair<string, string>?;
        var data = DataContext as ManagerViewModel;
        data.PathManage(rootData!.Value.Value);
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
    
    private void CommonChanged(object? sender, SelectionChangedEventArgs e)
    {
        var data = (sender as ListBox)!.SelectedItem as DirectoryInfo;
        (DataContext as ManagerViewModel)!.PathManage(data.FullName);
    }
    
    #endregion

}