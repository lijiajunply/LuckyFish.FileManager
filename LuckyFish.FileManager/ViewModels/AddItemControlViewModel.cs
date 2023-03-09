namespace LuckyFish.FileManager.ViewModels;

public class AddItemControlViewModel : ViewModelBase
{
    private string _text;
    private string _context;
    public string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    public string Context
    {
        get => _context;
        set => SetField(ref _context, value);
    }
    public AddItemControlViewModel(){}
    public AddItemControlViewModel(string text) => Text = text;
    public string Done() => Context;
}