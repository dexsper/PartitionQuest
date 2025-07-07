using System.Threading;
using System.Threading.Tasks;

namespace PartitionQuest.Core.Input;

public interface IInputProvider
{
    ValueTask<int> ReadNumberAsync(CancellationToken cancellationToken = default);
}