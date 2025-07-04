using PartitionQuest.Display;
using PartitionQuest.Input;
using PartitionQuest.Models;

namespace PartitionQuest;

public class GameManager
{
    private readonly PlayerInput _playerInput;
    private readonly IDisplay _display;
    private readonly List<Puzzle> _puzzles = new();
    private int _currentPuzzleIndex;
    private int _score;

    public GameManager(IInput input, IDisplay display)
    {
        _display = display;
        _playerInput = new PlayerInput(input, display);
    }

    public void AddPuzzle(Puzzle puzzle)
    {
        _puzzles.Add(puzzle);
    }

    public void StartGame()
    {
        _display.ShowWelcome();
        _display.ShowGameRules();

        foreach (var puzzle in _puzzles)
        {
            PlayPuzzle(puzzle);
            _currentPuzzleIndex++;

            if (_currentPuzzleIndex < _puzzles.Count)
            {
                _display.ShowNextPuzzle();
            }
        }

        _display.ShowFinalScore(_score, _puzzles.Count);
    }

    private void PlayPuzzle(Puzzle puzzle)
    {
        _display.ShowPuzzleHeader(_currentPuzzleIndex + 1);
        switch (puzzle.Type)
        {
            case PuzzleType.Basic:
            {
                _display.ShowPuzzleBasic(puzzle.TargetNumber);
                break;
            }
            case PuzzleType.OddOnly:
            {
                _display.ShowPuzzleOddOnly(puzzle.TargetNumber);
                break;
            }
            case PuzzleType.DistinctNumbers:
            {
                _display.ShowPuzzleDistinct(puzzle.TargetNumber);
                break;
            }
            case PuzzleType.FixedLength:
            {
                _display.ShowPuzzleFixedLength(puzzle.TargetNumber, puzzle.RequiredCount!.Value);
                break;
            }
            case PuzzleType.ExcludeNumber:
            {
                _display.ShowPuzzleExcludeNumber(puzzle.TargetNumber, puzzle.ExcludedNumber!.Value);
                break;
            }
            case PuzzleType.Combination:
            {
                _display.ShowPuzzleCombination(
                    puzzle.TargetNumber,
                    puzzle.OddNumbersOnly,
                    puzzle.DistinctNumbers,
                    puzzle.RequiredCount,
                    puzzle.ExcludedNumber
                );
                break;
            }
        }

        _display.ShowTotalPartitions(puzzle.CorrectPartitions.Count);

        List<Partition> playerPartitions;
        if (puzzle.Type is PuzzleType.Basic or PuzzleType.OddOnly or PuzzleType.DistinctNumbers or PuzzleType.ExcludeNumber)
        {
            int requiredCount = puzzle.CorrectPartitions.Count;
            _display.ShowNeedAllPartitions(requiredCount);
            playerPartitions = _playerInput.GetMultiplePartitions(puzzle.TargetNumber, requiredCount);
        }
        else
        {
            playerPartitions = _playerInput.GetManualPartition(puzzle.TargetNumber);
        }

        bool isValid = true;
        foreach (var partition in playerPartitions)
        {
            if (puzzle.ValidatePartition(partition))
                continue;

            _display.ShowPartitionInvalid(partition.ToString());
            isValid = false;
            break;
        }

        if (isValid && puzzle.CheckSolution(playerPartitions))
        {
            _display.ShowSuccess();
            _score++;
        }
        else
        {
            _display.ShowFailure(puzzle.CorrectPartitions.Select(p => p.ToString()));
        }
    }
}