using PartitionQuest.Core.Input;

namespace PartitionQuest.Tests.Mocks;

public class MockInputProvider : IInputProvider
{
    private readonly Queue<int> _inputs;

    public MockInputProvider(IEnumerable<int> inputs)
    {
        _inputs = new Queue<int>(inputs);
    }

    public ValueTask<int> ReadNumberAsync(CancellationToken cancellationToken = default)
    {
        if (_inputs.Count == 0)
            throw new InvalidOperationException("No more test data to enter");
        
        return new ValueTask<int>(_inputs.Dequeue());
    }
}