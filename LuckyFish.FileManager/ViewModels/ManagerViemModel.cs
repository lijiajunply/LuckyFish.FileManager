using System.Collections.Generic;
using System.IO;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViemModel : ViewModelBase
{
    private string _filePath;
    public string FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }

    public ManagerViemModel()
    {
        FilePath = "";
        Init();
    }

    private void Init()
    {
        List<FileSystemInfo> systemInfos = new List<FileSystemInfo>();
        foreach (var drive in DriveInfo.GetDrives())
            systemInfos.Add(drive.RootDirectory);
        Files = systemInfos.ToArray();
    }

    public ManagerViemModel(string path)
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
        FilePath = path;
        Files = new DirectoryInfo(path).GetFileSystemInfos();
    }
}