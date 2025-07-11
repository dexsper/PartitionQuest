using System.Collections.Generic;
using System.Threading.Tasks;
using PartitionQuest.Core.Display;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Input;

public class PlayerInput
{
    private readonly IInputProvider _inputProvider;
    private readonly IDisplay _display;

    public PlayerInput(IInputProvider inputProvider, IDisplay display)
    {
        _inputProvider = inputProvider;
        _display = display;
    }

    public async Task<List<Partition>> GetManualPartition(int targetNumber)
    {
        var numbers = new List<int>();
        int sum = 0;

        while (sum < targetNumber)
        {
            _display.ShowPartitionPrompt(targetNumber, sum, targetNumber - sum);
            int? num = await _inputProvider.ReadNumberAsync();

            if (!num.HasValue)
            {
                _display.ShowInputError();
                continue;
            }

            if (num < 0 || num > targetNumber)
            {
                _display.ShowInputError();
                continue;
            }

            if (sum + num > targetNumber)
            {
                _display.ShowInputError();
                continue;
            }

            numbers.Add(num.Value);
            sum += num.Value;
        }

        return new List<Partition> { new(numbers) };
    }

    public async Task<List<Partition>> GetMultiplePartitions(int targetNumber, int count)
    {
        var partitions = new List<Partition>();
        int i = 0;

        while (i < count)
        {
            _display.ShowPartitionNumber(i + 1);

            var manualPartitions = await GetManualPartition(targetNumber);
            if (partitions.Contains(manualPartitions[0]))
            {
                _display.ShowDuplicatePartition();
                continue;
            }

            partitions.Add(manualPartitions[0]);
            i++;
        }

        return partitions;
    }
}