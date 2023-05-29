using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    #region Left
    public ObservableCollection<DirectoryInfo> Common { get; set; }
    private Dictionary<string, string> _root = new();
    public Dictionary<string, string> Root
    {
        get => _root;
        set => SetField(ref _root, value);
    }

    #endregion
    

    public ManagerViewModel()
    {
       
    }
    

    public ManagerViewModel(string path)
    {
        
    }
    
    

    public void RemoveCommon(string path)
    {
        
    }

    public void AddCommon(string path)
    {
        CommonModel.AddCommon(path);
        Common.Add(new DirectoryInfo(path));
    }
}