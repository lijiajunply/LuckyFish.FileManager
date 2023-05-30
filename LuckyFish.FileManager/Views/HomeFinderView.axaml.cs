using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.Views;

public partial class HomeFinderView : UserControl
{
    public HomeFinderView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SizeBoxInit(object? sender, EventArgs e)
    {
        var a = sender as TextBlock;
        if(a == null)return;
        var model = a.DataContext as DriveSimpleOperation;
        if(model == null)return;
        a.Text = $"Use {LongSizeToString(model.Size)} and Total {LongSizeToString(model.TotalSize)}";
    }

    private string LongSizeToString(long size)
    {
        int i = 0;
        string[] name = {"B","KB","MB","GB","TB" };
        while (true)
        {
            if (size <= 1024) break;
            size /= 1024;
            i += 1;
        }
        return $"{size}{name[i]}";
    }
}