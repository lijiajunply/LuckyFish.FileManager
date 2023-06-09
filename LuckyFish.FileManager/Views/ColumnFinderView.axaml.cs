﻿using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FileManager.Lib;
using FileManager.Lib.FileModels;
using LuckyFish.FileManager.ViewModels;

namespace LuckyFish.FileManager.Views;

public partial class ColumnFinderView : UserControl
{
    public ColumnFinderView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void SelectedTapped(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as FinderViewBase;
        var context = (sender as Control)!.DataContext as IFileSystem;
        if(model == null || context == null)return;
        model.Open(context);
    }

    private void TextKeyDown(object? sender, KeyEventArgs e)
    {
        var data = DataContext as FinderViewBase;
        if(data == null)return;
        if (e.Key != Key.Enter) return;
        var text = (sender as TextBox)?.Text;
        data.Open(text);
    }


    #region ItemOperation
    
    private void RenameClick(object? sender, RoutedEventArgs e)
    {
        if((sender as Control)?.Parent?.Parent?.Parent is not Grid p)return;
        var data = (sender as Control)?.DataContext as IFileSystem;
        var text = new TextBox() { Text = data?.Name };
        text.KeyDown += RenameFinish;
        p.Children.RemoveAt(1);
        p.Children.Add(text);
        Grid.SetColumn(text, 1);
    }

    private void RenameFinish(object? sender, KeyEventArgs e)
    {
        var text = sender as TextBox;
        var p = text!.Parent as Grid;
        if(p == null || string.IsNullOrEmpty(text.Text))return;
        if (e.Key != Key.Enter) return;
        var data = p.DataContext as IFileSystem;
        FileSystemOperation.Rename(data!.Path, text.Text);
        p.Children.Remove(text);
        var context = new TextBlock() { Text = text.Text };
        p.Children.Add(context);
        Grid.SetColumn(context, 1);
        var model = DataContext as FinderViewBase;
        model?.Init();
    }

    #endregion

    private void MoreClick(object? sender, RoutedEventArgs e)
    {
        if(sender is not Control control)return;
        if(control.DataContext is not IFileSystem fileSystem)return;
        var model = FindMain((Control)Parent!);
        if(model == null)return;
        model.More(fileSystem);
    }

    private Manager? FindMain(Control control)
    {
        while (true)
        {
            if (control is Manager manager) return manager;
            if (control.Parent == null) return null;
            control = (Control)control.Parent;
        }
    }
}