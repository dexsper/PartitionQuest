using System.Collections.Generic;
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
    
    public List<Partition> GetManualPartition(int targetNumber)
    {
        _display.ShowManualPartitionIntro(targetNumber);

        var numbers = new List<int>();
        int sum = 0;

        while (sum < targetNumber)
        {
            _display.ShowPartitionPrompt(targetNumber, sum, targetNumber - sum);
            int num = _inputProvider.ReadNumber("");
            
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
        return new List<Partition> { new(numbers) };
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