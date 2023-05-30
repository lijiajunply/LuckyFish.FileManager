using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using DynamicData;
using FileManager.Lib;
using LuckyFish.FileManager.Models;
using ReactiveUI;
using Path = System.IO.Path;

namespace LuckyFish.FileManager.ViewModels;

public class FinderViewBase : ViewModelBase
{
    
    #region Pror

    private string? _addImagePath;
    public string? AddImagePath
    {
        get => _addImagePath;
        set => SetField(ref _addImagePath, value);
    }
    private string? _addName;
    public string? AddName
    {
        get => _addName;
        set => SetField(ref _addName, value);
    }
    private string? Last { get; set; }
    private ValueTuple<string, string> Mark { get; set; }

    private IFileSystem? _selectedItem;

    public IFileSystem? SelectedItem
    {
        get => _selectedItem;
        set => SetField(ref _selectedItem, value);
    }

    public ObservableCollection<IFileSystem> Files { get; set; } = new();
    private string? _filePath;

    public string? FilePath
    {
        get => _filePath;
        set
        {
            SetField(ref _filePath, value);
            if (value == null) return;
            var a = new DirectoryOperation(value);
            Files.Clear();
            Files.Add(a.GetFileSystems());
        }
    }

    #endregion

    #region Command

    private ReactiveCommand<string, Unit>? _fileOperationCommand;
    public  ReactiveCommand<string,Unit>? FileOperationCommand
    {
        get => _fileOperationCommand;
        set => SetField(ref _fileOperationCommand, value);
    }

    private ReactiveCommand<Unit, Unit>? _goToLastCommand;
    public ReactiveCommand<Unit,Unit>? GoToLastCommand
    {
        get => _goToLastCommand;
        set => SetField(ref _goToLastCommand, value);
    }
    
    private ReactiveCommand<Unit, Unit>? _goToParentCommand;
    public ReactiveCommand<Unit,Unit>? GoToParentCommand
    {
        get => _goToParentCommand;
        set => SetField(ref _goToParentCommand, value);
    }

    private ReactiveCommand<Unit, Unit>? _openTerminalCommand;
    public ReactiveCommand<Unit,Unit>? OpenTerminalCommand
    {
        get => _openTerminalCommand;
        set => SetField(ref _openTerminalCommand, value);
    }

    #endregion

    #region Function

    private void Init()
    {
        if(string.IsNullOrEmpty(FilePath))return;
        Files.Clear();
        var a = new DirectoryOperation(FilePath);
        Files.Add(a.GetFileSystems());
    }
    
    public void FileOperation(string? opera)
    {
        if(string.IsNullOrEmpty(opera) ||
           string.IsNullOrEmpty(FilePath))return;
        if (opera == "Paste")
        {
            if(string.IsNullOrEmpty(Mark.Item1) || string.IsNullOrEmpty(Mark.Item2))return;
            if (Mark.Item1 == "Copy")
                FileServer.Copy(Mark.Item2,FilePath);
            if (Mark.Item1 == "Cut")
                FileServer.Cut(Mark.Item2,FilePath);
        }

        if (SelectedItem != null)
        {
            if (opera == "Delete")
                FileServer.Delete(SelectedItem.Path);
            else
                Mark = (opera, SelectedItem.Path);
        }
        Init();
    }

    public void Open(IFileSystem system)
    {
        Last = FilePath;
        if (system is FileOperation)
            Process.Start(new ProcessStartInfo() { FileName = system.Path, UseShellExecute = true });
        else
            FilePath = system.Path;
    }

    public void Open(string? path)
    {
        if (string.IsNullOrEmpty(path)) return;
        var a = FileServer.Get(path);
        if (a == null) return;
        Open(a);
    }

    private void GoToParent() => FilePath = Path.GetDirectoryName(FilePath);

    private void GoToLast()
    {
        if (string.IsNullOrEmpty(Last)) return;
        FilePath = Last;
    }

    private void OpenTerminal()
    {
        if(string.IsNullOrEmpty(FilePath))return;
        Process.Start(new ProcessStartInfo(OSInfo.TerminalPath,$"cd {FilePath}"));
    }

    #endregion

    public FinderViewBase()
    {
        FileOperationCommand = ReactiveCommand.Create((string s) => FileOperation(s));
        GoToParentCommand = ReactiveCommand.Create(GoToParent);
        GoToLastCommand = ReactiveCommand.Create(GoToLast);
        OpenTerminalCommand = ReactiveCommand.Create(OpenTerminal);
    }
}

#region FinderViewModel's Children

public class ColumnFinderViewModel : FinderViewBase
{
    
}

public class ImageFinderViewModel : FinderViewBase
{
    
}

#endregion