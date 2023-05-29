using System.Collections.ObjectModel;
using System.Windows.Input;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.ViewModels;

public class FinderViewBase : ViewModelBase
{
    #region Pror

    public ObservableCollection<IFileSystem> Files { get; set; } = new();
    private string? _filePath;

    public string? FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }

    #endregion

    #region Command

    public ICommand Command { get; set; }

    #endregion
    
}





#region FinderViewModel's Children

public class ColumnFinderViewModel : FinderViewBase
{
}



#endregion
