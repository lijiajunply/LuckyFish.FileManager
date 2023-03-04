using System.Collections.Generic;
using System.IO;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    public (string operation,string path) Mark { get; set; }
    public FileSystemInfo Selection { get; set; }
    public DirectoryInfo This { get; set; }
    public DirectoryInfo Last { get; set; }
    private string _filePath;
    public string FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }
    public ManagerViewModel()
    {
        FilePath = "";
        Last = new DirectoryInfo(Path.GetPathRoot(GetType().Assembly.Location));
        This = new DirectoryInfo(Path.GetPathRoot(GetType().Assembly.Location));
        Init();
    }

    private void Init()
    {
        List<FileSystemInfo> systemInfos = new List<FileSystemInfo>();
        foreach (var drive in DriveInfo.GetDrives())
            systemInfos.Add(drive.RootDirectory);
        Files = systemInfos.ToArray();
    }

    public ManagerViewModel(string path)
    {
        FilePath = path;
        Init();
    }

    private FileSystemInfo[] _files;
    public FileSystemInfo[] Files
    {
        get => _files;
        set => SetField(ref _files, value);
    }

    
    public void PathManage(string path)
    {
        Last = new DirectoryInfo(FilePath == ""?Path.GetPathRoot(GetType().Assembly.Location):FilePath);
        FilePath = path;
        This = new DirectoryInfo(FilePath);
        Files = new DirectoryInfo(path).GetFileSystemInfos();
    }
}