using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using ReactiveUI;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViemModel : ViewModelBase
{
    private string _filePath;
    public string FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }

    public ManagerViemModel() => FilePath = "";

    public ManagerViemModel(string path)
    {
        FilePath = path;
    }

    private FileSystemInfo[] _files = { new DirectoryInfo(@"C:\") };
    public FileSystemInfo[] Files
    {
        get => _files;
        set => SetField(ref _files, value);
    }
}