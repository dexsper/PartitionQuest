using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PartitionQuest.UI;

public partial class GameFinishWindow : Window
{
    public GameFinishWindow(string message)
    {
        InitializeComponent();
        MessageTextBlock.Text = message;
    }
    
    private void NewGame_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }
}