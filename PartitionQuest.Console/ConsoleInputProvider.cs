using PartitionQuest.Core.Input;

namespace PartitionQuest;

public class ConsoleInputProvider : IInputProvider, IDisposable
{
    private readonly StreamReader _reader = new(Console.OpenStandardInput(), Console.InputEncoding);

    public async Task<int?> ReadNumberAsync()
    {
        string? line = await _reader.ReadLineAsync();
        if (!int.TryParse(line, out int num))
            return null;

        return num;
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}