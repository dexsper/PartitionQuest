using PartitionQuest.Core.Display;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest;

public class ConsoleDisplay : IDisplay
{
    public void ShowWelcome()
    {
        Console.WriteLine("Добро пожаловать в Partition Quest!");
    }

    public void ShowGameRules()
    {
        Console.WriteLine("Ваша задача - находить разбиения чисел согласно условиям.\n");
    }

    public void ShowPuzzleHeader(int puzzleNumber)
    {
        Console.WriteLine($"=== Задание {puzzleNumber} ===");
    }

    public void ShowPuzzleDescription(PuzzleDescription model)
    {
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
                throw new NotImplementedException($"Неизвестный тип описания: {model.GetType().Name}");
        }
    }

    public void ShowNeedAllPartitions(int count)
    {
        Console.WriteLine($"\nВам нужно найти все {count} разбиений.");
    }

    public void ShowTotalPartitions(int count)
    {
        Console.WriteLine($"Всего возможных разбиений: {count}");
    }

    public void ShowPartitionPrompt(int targetNumber, int sum, int remaining)
    {
        Console.Write($"Текущая сумма: {sum}. Осталось: {remaining}. Введите число: ");
    }

    public void ShowErrorInput()
    {
        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
    }

    public void ShowErrorSumMismatch()
    {
        Console.WriteLine("Сумма не совпадает с целевым числом!");
    }

    public void ShowErrorOutOfRange()
    {
        Console.WriteLine("Число должно быть положительным и не превышать целевое число!");
    }

    public void ShowErrorOverflow()
    {
        Console.WriteLine("Превышение суммы! Попробуйте другое число.");
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

    public void ShowSuccess()
    {
        Console.WriteLine("\nПоздравляем! Все разбиения верны.");
    }

    public void ShowFailure(IEnumerable<string> correctPartitions)
    {
        Console.WriteLine("\nК сожалению, есть ошибки. Вот правильные разбиения:");
        
        foreach (var part in correctPartitions)
            Console.WriteLine(part);
    }

    public void ShowNextPuzzle()
    {
        Console.WriteLine("\nПереходим к следующему заданию...\n");
    }

    public void ShowFinalScore(int score, int total)
    {
        Console.WriteLine($"\nИгра завершена! Ваш итоговый счет: {score} из {total}");
    }

    public void ShowManualPartitionIntro(int targetNumber)
    {
        Console.WriteLine($"\nСобираем разбиение для числа {targetNumber}. Вводите числа по одному, 0 для завершения:");
    }
}