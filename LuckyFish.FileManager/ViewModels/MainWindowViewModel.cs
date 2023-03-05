using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LuckyFish.FileManager.Models;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private List<DirectoryInfo> _common;
    public List<DirectoryInfo> Common
    {
        get => _common;
        set => SetField(ref _common, value);
    }
    private Dictionary<string, string> _fileRoot = new ();
    public Dictionary<string, string> FileRoot
    {
        get => _fileRoot;
        set => SetField(ref _fileRoot, value);
    }

    private ManagerViewModel _model = new ();
    public ManagerViewModel Model
    {
        get => _model;
        set => SetField(ref _model, value);
    }

    public void CommonToJson()
    {
       
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
    }

    private List<DirectoryInfo> FromJson()
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        List<DirectoryInfo> infos = new List<DirectoryInfo>();
        FileSystemServer.GetCommonPath().ToList().ForEach(x => infos.Add(x));
        if (model.Commons != null) infos.AddRange(model.Commons);
        return infos;
    }
    public MainWindowViewModel()
    {
        Common = FromJson();
        var drivers = DriveInfo.GetDrives();
        foreach (var driver in drivers)
            FileRoot.Add(driver.Name,driver.RootDirectory.FullName);
    }
}