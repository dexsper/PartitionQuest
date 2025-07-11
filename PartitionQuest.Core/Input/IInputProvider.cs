using System.Threading.Tasks;

namespace PartitionQuest.Core.Input;

public interface IInputProvider
{
    Task<int?> ReadNumberAsync();
}