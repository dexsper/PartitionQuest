using PartitionQuest.Display;

namespace PartitionQuest.Input;

public class ConsoleInput : IInput
{
    private readonly IDisplay _display;
    public ConsoleInput(IDisplay display)
    {
        _display = display;
    }
    public int ReadNumber(string prompt)
    {
        int num;
        _display.ShowPartitionPrompt(0, 0, 0);
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            _display.ShowErrorInput();
            _display.ShowPartitionPrompt(0, 0, 0);
        }
        return num;
    }
}