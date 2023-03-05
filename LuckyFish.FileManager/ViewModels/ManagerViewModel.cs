using System.Collections.Generic;
using System.IO;
using LuckyFish.FileManager.Models;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    #region Left

    private List<DirectoryInfo> _common;

    public List<DirectoryInfo> Common
    {
        get => _common;
        set => SetField(ref _common, value);
    }

    private Dictionary<string, string> _fileRoot = new();

    public Dictionary<string, string> FileRoot
    {
        get => _fileRoot;
        set => SetField(ref _fileRoot, value);
    }

    #endregion

    #region Basic

    public (string operation, string path) Mark { get; set; }
    public FileSystemInfo Selection { get; set; }
    public DirectoryInfo This { get; set; }
    public DirectoryInfo Last { get; set; }

    #endregion

    #region Right

    private string _filePath;

    public string FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }

    private FileSystemInfo[] _files;

    public FileSystemInfo[] Files
    {
        get => _files;
        set => SetField(ref _files, value);
    }

    #endregion


    public ManagerViewModel()
    {
        FilePath = "";
        Last = new DirectoryInfo(CodeServer.Root);
        This = new DirectoryInfo(CodeServer.Root);
        Init();
    }

    private void Init()
    {
        //Left
        Common = CommonModel.FromJson();
        var drivers = DriveInfo.GetDrives();
        foreach (var driver in drivers)
            FileRoot.Add(driver.Name, driver.RootDirectory.FullName);
        //Right
        List<FileSystemInfo> systemInfos = new List<FileSystemInfo>();
        foreach (var drive in DriveInfo.GetDrives())
            systemInfos.Add(drive.RootDirectory);
        Files = systemInfos.ToArray();
    }

    public ManagerViewModel(string path)
    {
        FilePath = path;
        Last = new DirectoryInfo(FilePath);
        This = new DirectoryInfo(FilePath);
        Init();
    }
    
    public void PathManage(string path)
    {
        Last = new DirectoryInfo(FilePath == "" ? Path.GetPathRoot(GetType().Assembly.Location) : FilePath);
        FilePath = path;
        This = new DirectoryInfo(FilePath);
        ReInit();
    }

    public void ReInit() => Files = new DirectoryInfo(FilePath).GetFileSystemInfos();
}