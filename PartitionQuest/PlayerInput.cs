using PartitionQuest.Input;
using PartitionQuest.Models;

namespace PartitionQuest;


public class PlayerInput
{
    private readonly IInputProvider _inputProvider;

    public PlayerInput(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }
    
    public List<Partition> GetManualPartition(int targetNumber)
    {
        Console.WriteLine($"\nСобираем разбиение для числа {targetNumber}. Вводите числа по одному, 0 для завершения:");

        var numbers = new List<int>();
        int sum = 0;

        while (sum < targetNumber)
        {
            int num = _inputProvider.ReadNumber($"Текущая сумма: {sum}. Осталось: {targetNumber - sum}. Введите число: ");
            if (num == 0)
            {
                if (sum == targetNumber)
                    break;
                Console.WriteLine("Сумма не совпадает с целевым числом!");
                continue;
            }
            if (num < 0 || num > targetNumber)
            {
                Console.WriteLine("Число должно быть положительным и не превышать целевое число!");
                continue;
            }
            if (sum + num > targetNumber)
            {
                Console.WriteLine("Превышение суммы! Попробуйте другое число.");
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
            Console.WriteLine($"\nРазбиение #{i + 1}:");
            var partition = GetManualPartition(targetNumber)[0];
            if (partitions.Contains(partition))
            {
                Console.WriteLine("Такое разбиение уже было введено! Введите другое.");
                continue;
            }
            partitions.Add(partition);
            i++;
        }
        return partitions;
    }
}