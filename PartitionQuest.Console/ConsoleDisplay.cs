using PartitionQuest.Core.Display;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest;

public class ConsoleDisplay : IDisplay
{
    public void ShowPuzzle(int number, PuzzleDescription model, int total)
    {
        Console.WriteLine($"=== Задание {number} ===");
        
        switch (model)
        {
            case BasicPuzzleDescription b:
            {
                Console.WriteLine($"Разбейте число {b.TargetNumber} на сумму положительных целых чисел.");
                break;
            }
            case OddOnlyPuzzleDescription o:
            {
                Console.WriteLine($"Разбейте число {o.TargetNumber} на сумму нечетных чисел.");
                break;
            }
            case DistinctNumbersPuzzleDescription d:
            {
                Console.WriteLine($"Разбейте число {d.TargetNumber} на сумму различных чисел.");
                break;
            }
            case FixedLengthPuzzleDescription f:
            {
                Console.WriteLine($"Разбейте число {f.TargetNumber} на {f.RequiredCount} чисел.");
                break;
            }
            case ExcludeNumberPuzzleDescription e:
            {
                Console.WriteLine($"Разбейте число {e.TargetNumber} без использования числа {e.ExcludedNumber}.");
                break;
            }
            case CombinationPuzzleDescription c:
            {
                var desc = $"Разбейте число {c.TargetNumber} на сумму чисел с условиями:";
                
                if (c.OddOnly)
                    desc += "\n- Только нечетные числа";
                
                if (c.Distinct)
                    desc += "\n- Все числа должны быть разными";
                
                if (c.RequiredCount.HasValue)
                    desc += $"\n- Ровно {c.RequiredCount.Value} чисел";
                
                if (c.ExcludedNumber.HasValue)
                    desc += $"\n- Без использования числа {c.ExcludedNumber.Value}";
                
                Console.WriteLine(desc);
                break;
            }
            default:
                throw new NotImplementedException($"Unknown type of description: {model.GetType().Name}");
        }
        
        Console.WriteLine($"Всего возможных разбиений: {total}");
    }

    public void ShowPartitionPrompt(int targetNumber, int sum, int remaining)
    {
        Console.Write($"Текущая сумма: {sum}. Осталось: {remaining}. Введите число: ");
    }

    public void ShowInputError()
    {
        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
    }
    
    public void ShowDuplicatePartition()
    {
        Console.WriteLine("Такое разбиение уже было введено! Введите другое.");
    }

    public void ShowPartitionNumber(int number)
    {
        Console.WriteLine($"\nРазбиение #{number}:");
    }

    public void ShowPartitionInvalid(string partition)
    {
        Console.WriteLine($"Разбиение {partition} не соответствует условиям задачи!");
    }

    public void ShowFinalScore(int score, int total)
    {
        Console.WriteLine($"\nИгра завершена! Ваш итоговый счет: {score} из {total}");
    }
}