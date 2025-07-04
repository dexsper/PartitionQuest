namespace PartitionQuest.Display;

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

    public void ShowPuzzleBasic(int targetNumber)
    {
        Console.WriteLine($"Разбейте число {targetNumber} на сумму положительных целых чисел.");
    }

    public void ShowPuzzleOddOnly(int targetNumber)
    {
        Console.WriteLine($"Разбейте число {targetNumber} на сумму нечетных чисел.");
    }

    public void ShowPuzzleDistinct(int targetNumber)
    {
        Console.WriteLine($"Разбейте число {targetNumber} на сумму различных чисел.");
    }

    public void ShowPuzzleFixedLength(int targetNumber, int count)
    {
        Console.WriteLine($"Разбейте число {targetNumber} на {count} чисел.");
    }

    public void ShowPuzzleExcludeNumber(int targetNumber, int excluded)
    {
        Console.WriteLine($"Разбейте число {targetNumber} без использования числа {excluded}.");
    }

    public void ShowPuzzleCombination(int targetNumber, bool odd, bool distinct, int? count, int? excluded)
    {
        var desc = $"Разбейте число {targetNumber} на сумму чисел с условиями:";
        if (odd)
            desc += "\n- Только нечетные числа";
        if (distinct)
            desc += "\n- Все числа должны быть разными";
        if (count.HasValue)
            desc += $"\n- Ровно {count.Value} чисел";
        if (excluded.HasValue)
            desc += $"\n- Без использования числа {excluded.Value}";
        Console.WriteLine(desc);
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

    public void ShowPressAnyKey()
    {
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
    }

    public void ShowManualPartitionIntro(int targetNumber)
    {
        Console.WriteLine($"\nСобираем разбиение для числа {targetNumber}. Вводите числа по одному, 0 для завершения:");
    }
} 