using PartitionQuest.Display;
using PartitionQuest.Input;
using PartitionQuest.Models;

namespace PartitionQuest;


public class PlayerInput
{
    private readonly IInput _input;
    private readonly IDisplay _display;

    public PlayerInput(IInput input, IDisplay display)
    {
        _input = input;
        _display = display;
    }
    
    public List<Partition> GetManualPartition(int targetNumber)
    {
        _display.ShowManualPartitionIntro(targetNumber);

        var numbers = new List<int>();
        int sum = 0;

        while (sum < targetNumber)
        {
            _display.ShowPartitionPrompt(targetNumber, sum, targetNumber - sum);
            int num = _input.ReadNumber("");
            if (num == 0)
            {
                if (sum == targetNumber)
                    break;
                _display.ShowErrorSumMismatch();
                continue;
            }
            if (num < 0 || num > targetNumber)
            {
                _display.ShowErrorOutOfRange();
                continue;
            }
            if (sum + num > targetNumber)
            {
                _display.ShowErrorOverflow();
                continue;
            }
            numbers.Add(num);
            sum += num;
        }
        return [new(numbers)];
    }

    public List<Partition> GetMultiplePartitions(int targetNumber, int count)
    {
        var partitions = new List<Partition>();
        int i = 0;
        while (i < count)
        {
            _display.ShowPartitionNumber(i + 1);
            var partition = GetManualPartition(targetNumber)[0];
            if (partitions.Contains(partition))
            {
                _display.ShowDuplicatePartition();
                continue;
            }
            partitions.Add(partition);
            i++;
        }
        return partitions;
    }
}