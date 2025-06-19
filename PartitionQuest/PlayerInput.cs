using PartitionQuest.Input;
using PartitionQuest.Models;

namespace PartitionQuest;


public class PlayerInput
{
    public static List<Partition> GetManualPartition(int targetNumber, IInputProvider inputProvider)
    {
        Console.WriteLine($"\nСобираем разбиение для числа {targetNumber}. Вводите числа по одному, 0 для завершения:");

        var numbers = new List<int>();
        int sum = 0;

        while (sum < targetNumber)
        {
            int num = inputProvider.ReadNumber($"Текущая сумма: {sum}. Осталось: {targetNumber - sum}. Введите число: ");
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

    public static List<Partition> GetMultiplePartitions(int targetNumber, int count, IInputProvider inputProvider)
    {
        var partitions = new List<Partition>();
        int i = 0;
        while (i < count)
        {
            Console.WriteLine($"\nРазбиение #{i + 1}:");
            var partition = GetManualPartition(targetNumber, inputProvider)[0];
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

    public static bool ValidatePartition(Partition partition, Puzzle puzzle)
    {
        if (partition.Numbers.Sum() != puzzle.TargetNumber)
            return false;

        if (puzzle.DistinctNumbers && HasDuplicates(partition.Numbers))
            return false;

        if (puzzle.OddNumbersOnly && partition.Numbers.Any(num => num % 2 == 0))
            return false;

        if (puzzle.RequiredCount.HasValue && partition.Numbers.Count != puzzle.RequiredCount.Value)
            return false;

        return !puzzle.ExcludedNumber.HasValue || !partition.Numbers.Contains(puzzle.ExcludedNumber.Value);
    }

    private static bool HasDuplicates(List<int> numbers)
    {
        return numbers.Count != numbers.Distinct().Count();
    }
}