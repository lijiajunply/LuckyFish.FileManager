using System;
using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using LuckyFish.FileManager.Serves;
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
                FileSystemServer.IsDir(data.FilePath) ? new DirectoryInfo(data.FilePath) : new FileInfo(data.FilePath);
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
        var directory = System.AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
        var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
        string fileordir = data is FileInfo ? "file" : "dir";
        var path = Path.Combine(slice.ToArray()) + $"/Assets/{fileordir}.jpg";
        image.Source = new Bitmap(path);
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
        text.Text = FileSystemServer.GetSize(data.FullName).ToString();
    }

    #region FlyoutOperation

    private void CopyClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Button).DataContext as FileSystemInfo;
        var model = DataContext as ManagerViewModel;
        model.Mark = ("copy", data.FullName);
        DataContext = model;
    }

    private void CutClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Button).DataContext as FileSystemInfo;
        var model = DataContext as ManagerViewModel;
        model.Mark = ("cut", data.FullName);
        DataContext = model;
    }

    private void PasteClick(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as ManagerViewModel;
        if (model.Mark.operation == "copy")
            FileSystemServer.Copy(model.Mark.path, model.This.FullName);
        else
            FileSystemServer.Cut(model.Mark.path, model.This.FullName);
        if (model.Mark is not (null, null)) model.ReInit();
    }

    private void RenameClick(object? sender, RoutedEventArgs e)
    {
        var p = (sender as Button).Parent.Parent.Parent.Parent as Grid;
        var data = (sender as Button).DataContext as FileSystemInfo;
        var text = new TextBox() { Text = data.Name };
        text.KeyDown += RenameFinish;
        p.Children.RemoveAt(1);
        p.Children.Add(text);
        Grid.SetColumn(text,1);
        //p.Children.Remove(text);
    }

    private void DeleteClick(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Button).DataContext as FileSystemInfo;
        FileSystemServer.Delete(data.FullName);
        (DataContext as ManagerViewModel).ReInit();
    }

    #endregion

    private void RenameFinish(object? sender, KeyEventArgs e)
    {
        var text = sender as TextBox;
        var p = text.Parent as Grid;
        if (e.Key == Key.Enter)
        {
            var data = p.DataContext as FileSystemInfo; 
            FileSystemServer.Rename(data.FullName,text.Text);
            (DataContext as ManagerViewModel).ReInit();
            p.Children.Remove(text);
            var context = new TextBlock() { Text = text.Text };
            p.Children.Add(context);
            Grid.SetColumn(context,1);
        }
    }
}