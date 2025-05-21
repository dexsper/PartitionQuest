using PartitionQuest.Models;

namespace PartitionQuest;

public class GameManager
{
    private readonly List<Puzzle> _puzzles = new();
    private int _currentPuzzleIndex;
    private int _score;

    public void AddPuzzle(Puzzle puzzle)
    {
        _puzzles.Add(puzzle);
    }

    public void StartGame()
    {
        Console.WriteLine("Добро пожаловать в Partition Quest!");
        Console.WriteLine("Ваша задача - находить разбиения чисел согласно условиям.\n");

        foreach (var puzzle in _puzzles)
        {
            PlayPuzzle(puzzle);
            _currentPuzzleIndex++;

            if (_currentPuzzleIndex < _puzzles.Count)
            {
                Console.WriteLine("\nПереходим к следующему заданию...\n");
            }
        }

        Console.WriteLine($"\nИгра завершена! Ваш итоговый счет: {_score} из {_puzzles.Count}");
    }

    private void PlayPuzzle(Puzzle puzzle)
    {
        Console.WriteLine($"=== Задание {_currentPuzzleIndex + 1} ===");
        Console.WriteLine(GetPuzzleDescription(puzzle));
        Console.WriteLine($"Всего возможных разбиений: {puzzle.CorrectPartitions.Count}");

        List<Partition> playerPartitions;

        if (puzzle.Type is PuzzleType.Basic or PuzzleType.OddOnly or PuzzleType.DistinctNumbers or PuzzleType.ExcludeNumber)
        {
            int requiredCount = puzzle.CorrectPartitions.Count;
            Console.WriteLine($"\nВам нужно найти все {requiredCount} разбиений.");
            playerPartitions = PlayerInput.GetMultiplePartitions(puzzle.TargetNumber, requiredCount);
        }
        else
        {
            playerPartitions = PlayerInput.GetManualPartition(puzzle.TargetNumber);
        }

        bool isValid = true;
        foreach (var partition in playerPartitions)
        {
            if (PlayerInput.ValidatePartition(partition, puzzle))
                continue;
            
            Console.WriteLine($"Разбиение {partition} не соответствует условиям задачи!");
            isValid = false;
            break;
        }

        if (isValid && puzzle.CheckSolution(playerPartitions))
        {
            Console.WriteLine("\nПоздравляем! Все разбиения верны.");
            _score++;
        }
        else
        {
            Console.WriteLine("\nК сожалению, есть ошибки. Вот правильные разбиения:");
            foreach (var correctPart in puzzle.CorrectPartitions)
            {
                Console.WriteLine(correctPart);
            }
        }
    }

    private string GetPuzzleDescription(Puzzle puzzle)
    {
        return puzzle.Type switch
        {
            PuzzleType.Basic => $"Разбейте число {puzzle.TargetNumber} на сумму положительных целых чисел.",
            PuzzleType.OddOnly => $"Разбейте число {puzzle.TargetNumber} на сумму нечетных чисел.",
            PuzzleType.DistinctNumbers => $"Разбейте число {puzzle.TargetNumber} на сумму различных чисел.",
            PuzzleType.FixedLength => $"Разбейте число {puzzle.TargetNumber} на {puzzle.RequiredCount} чисел.",
            PuzzleType.ExcludeNumber => $"Разбейте число {puzzle.TargetNumber} без использования числа {puzzle.ExcludedNumber}.",
            PuzzleType.Combination => GetCombinedDescription(puzzle),
            _ => "Неизвестный тип головоломки."
        };
    }

    private string GetCombinedDescription(Puzzle puzzle)
    {
        var desc = $"Разбейте число {puzzle.TargetNumber} на сумму чисел с условиями:";

        if (puzzle.OddNumbersOnly)
            desc += "\n- Только нечетные числа";

        if (puzzle.DistinctNumbers)
            desc += "\n- Все числа должны быть разными";

        if (puzzle.RequiredCount.HasValue)
            desc += $"\n- Ровно {puzzle.RequiredCount} чисел";

        if (puzzle.ExcludedNumber.HasValue)
            desc += $"\n- Без использования числа {puzzle.ExcludedNumber}";

        return desc;
    }
}