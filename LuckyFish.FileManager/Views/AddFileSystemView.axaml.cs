using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FileManager.Lib.FileModels;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.Views;

public partial class AddFileSystemView : Window
{
    public AddFileSystemView()
    {
        InitializeComponent();
    }
    private bool IsFile { get; set; }
    private string? ThePath { get; set; }
    public AddFileSystemView(string path,bool isAddFile)
    {
        ThePath = path;
        IsFile = isAddFile;
        InitializeComponent();
        FileNameBox = this.FindControl<TextBox>("FileNameBox");
        Title = isAddFile ? "Add File in:" : "Add Folder in:" + path;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OkClick(object? sender, RoutedEventArgs e)
    {
        if (ThePath != null) FileServer.Create(ThePath, FileNameBox.Text, IsFile);
        Close(true);
    }

    private void CancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}