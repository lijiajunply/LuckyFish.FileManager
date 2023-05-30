using System.Collections.ObjectModel;
using System.IO;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    #region Left

    public ObservableCollection<IFileSystem> Common { get; set; }
    public ObservableCollection<DriveSimpleOperation> Root { get; set; }

    #endregion
    
    private ViewModelBase? _finder = new ColumnFinderViewModel();
    public ViewModelBase? Finder
    {
        get => _finder;
        set => SetField(ref _finder, value);
    }

    public void FilePath(string path)
    {
        if (_finder is FinderViewBase model)
        {
            model.FilePath = path;
        }
        else
        {
            
        }
    }
    
    public ManagerViewModel()
    {
        Common = new ObservableCollection<IFileSystem>(FileServer.GetCommonPath());
        Root = new ObservableCollection<DriveSimpleOperation>();
        var a = DriveInfo.GetDrives();
        foreach (var info in a)
            Root.Add(new DriveSimpleOperation(info.Name));
    }
}