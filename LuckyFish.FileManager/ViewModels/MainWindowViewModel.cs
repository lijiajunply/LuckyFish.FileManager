using System.Collections.Generic;
using System.IO;

namespace LuckyFish.FileManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Dictionary<string, string> _fileRoot = new Dictionary<string, string>();
    /// <summary>
    /// key is name ,value is path
    /// </summary>
    public Dictionary<string, string> FileRoot
    {
        get => _fileRoot;
        set => SetField(ref _fileRoot, value);
    }

    private ManagerViemModel _model = new (@"C:\");
    public ManagerViemModel Model
    {
        get => _model;
        set => SetField(ref _model, value);
    }

    public MainWindowViewModel()
    {
        var drivers = DriveInfo.GetDrives();
        foreach (var driver in drivers)
            FileRoot.Add(driver.Name,driver.RootDirectory.FullName);
    }
}