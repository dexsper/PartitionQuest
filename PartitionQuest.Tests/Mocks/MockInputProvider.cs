using PartitionQuest.Core.Input;

namespace PartitionQuest.Tests.Mocks;

public class MockInputProvider : IInputProvider
{
    private readonly Queue<int> _inputs;

    public MockInputProvider(IEnumerable<int> inputs)
    {
        _inputs = new Queue<int>(inputs);
    }

    public Task<int?> ReadNumberAsync()
    {
        if (_inputs.Count == 0)
            throw new InvalidOperationException("No more test data to enter");

        return Task.FromResult((int?)_inputs.Dequeue());
    }
}