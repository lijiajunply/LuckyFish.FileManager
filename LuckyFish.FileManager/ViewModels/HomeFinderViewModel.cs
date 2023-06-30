using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileManager.Lib.FileModels;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.ViewModels;

public class HomeFinderViewModel : ViewModelBase
{
    public ObservableCollection<CommonModel>? Commons { get; set; }
    public ObservableCollection<DriveSimpleOperation>? Drives { get; set; }

    public HomeFinderViewModel(IEnumerable<DriveSimpleOperation> a,IEnumerable<CommonModel> b)
    {
        Drives = new ObservableCollection<DriveSimpleOperation>(a);
        Commons = new ObservableCollection<CommonModel>(b);
    }

    public HomeFinderViewModel()
    {
        
    }
}