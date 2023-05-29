using System.Collections.ObjectModel;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    #region Left

    public ObservableCollection<IFileSystem> Common { get; set; }
    public ObservableCollection<DriveOperation> Root { get; set; } = new ();

    #endregion
    
    private FinderViewBase _finder = new ColumnFinderViewModel();
    public FinderViewBase Finder
    {
        get => _finder;
        set => SetField(ref _finder, value);
    }
    
    public ManagerViewModel()
    {
        Common = new ObservableCollection<IFileSystem>(FileServer.GetCommonPath());
    }
}