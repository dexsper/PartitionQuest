using System.Collections.Generic;
using System.Linq;
using PartitionQuest.Core.Display;
using PartitionQuest.Core.Input;
using PartitionQuest.Core.Models;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Core;

public class GameManager
{
    private readonly PlayerInput _playerInput;
    private readonly IDisplay _display;
    private readonly List<Puzzle> _puzzles = new();
    private int _currentPuzzleIndex;
    private int _score;

    public GameManager(IInputProvider inputProvider, IDisplay display)
    {
        _display = display;
        _playerInput = new PlayerInput(inputProvider, display);
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
        _display.ShowPuzzleDescription(puzzle.GetDescriptionModel());
        _display.ShowTotalPartitions(puzzle.CorrectPartitions.Count);

        List<Partition> playerPartitions;
        if (puzzle is BasicPuzzle or OddOnlyPuzzle or DistinctNumbersPuzzle or ExcludeNumberPuzzle)
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