using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FileManager.Lib;
using LuckyFish.FileManager.Models;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class ImageFinderView : UserControl
{
    public ImageFinderView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void BorderDoubleTapped(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as FinderViewBase;
        var context = (sender as Control)!.DataContext as IFileSystem;
        if (model == null || context == null) return;
        model.Open(context);
    }

    private void TextKeyDown(object? sender, KeyEventArgs e)
    {
        var data = DataContext as FinderViewBase;
        if (data == null) return;
        if (e.Key != Key.Enter) return;
        var text = (sender as TextBox)?.Text;
        data.Open(text);
    }


    #region ItemFlyoutOperation

    private void RenameClick(object? sender, RoutedEventArgs e)
    {
        if ((sender as Control)?.Parent?.Parent?.Parent is not StackPanel p) return;
        var data = (sender as Control)?.DataContext as IFileSystem;
        var text = new TextBox() { Text = data?.Name };
        text.KeyDown += RenameFinish;
        p.Children.RemoveAt(1);
        p.Children.Add(text);
        Grid.SetRow(text, 1);
    }

    private void RenameFinish(object? sender, KeyEventArgs e)
    {
        var text = sender as TextBox;
        var p = text!.Parent as StackPanel;
        if (p == null || string.IsNullOrEmpty(text.Text)) return;
        if (e.Key != Key.Enter) return;
        var data = p.DataContext as IFileSystem;
        FileSystemOperation.Rename(data!.Path, text.Text);
        p.Children.Remove(text);
        var context = new TextBlock()
        {
            Text = text.Text, 
            VerticalAlignment = VerticalAlignment.Center, 
            TextWrapping = TextWrapping.WrapWithOverflow
        };
        p.Children.Add(context);
        Grid.SetRow(context, 1);
        var model = DataContext as FinderViewBase;
        model?.Init();
    }

    #endregion
}