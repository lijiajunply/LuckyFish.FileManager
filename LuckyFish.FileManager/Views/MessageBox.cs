using Avalonia.Controls;

namespace LuckyFish.FileManager.Views;

public class MessageBox : Window
{
    public MessageBox(string message)
    {
        var grid = new Grid();
        var buttonGrid = new Grid();
        var text = new TextBlock(){Text = message};
        var ok = new Button();
        ok.Click += (sender, args) => Close(true);
        var cancel = new Button();
        cancel.Click += (sender, args) => Close(false);
        Content = grid;
        grid.Children.Add(text);
        grid.Children.Add(buttonGrid);
        buttonGrid.Children.Add(ok);
        buttonGrid.Children.Add(cancel);
        Grid.SetRow(text,0);
        Grid.SetRow(buttonGrid,1);
        Grid.SetColumn(ok,0);
        Grid.SetColumn(cancel,1);
    }
}