using PartitionQuest.Input;

namespace PartitionQuest.Tests;

public class TestInput : IInput
{
    private readonly Queue<int> _inputs;

    public TestInput(IEnumerable<int> inputs)
    {
        _inputs = new Queue<int>(inputs);
    }

    public int ReadNumber(string prompt)
    {
        if (_inputs.Count == 0)
            throw new InvalidOperationException("Нет больше тестовых данных для ввода");
        return _inputs.Dequeue();
    }
}