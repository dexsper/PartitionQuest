using PartitionQuest.Core;
using PartitionQuest.Core.Display;
using PartitionQuest.Core.Input;

namespace PartitionQuest;

public class ConsoleInputProvider : IInputProvider
{
    private readonly IDisplay _display;
    public ConsoleInputProvider(IDisplay display)
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