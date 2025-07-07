using PartitionQuest.Core.Display;
using PartitionQuest.Core.Input;

namespace PartitionQuest;

public class ConsoleInputProvider : IInputProvider, IDisposable
{
    private readonly IDisplay _display;
    private readonly StreamReader _reader;
    
    public ConsoleInputProvider(IDisplay display)
    {
        _display = display;
        _reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
    }
    
    public async ValueTask<int> ReadNumberAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            string? line = await _reader.ReadLineAsync(cancellationToken);

            if (int.TryParse(line, out int num))
                return num;

            _display.ShowErrorInput();
        }
        
        throw new OperationCanceledException("Input canceled");
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}