using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task StartGame()
    {
        foreach (var puzzle in _puzzles)
        {
            await PlayPuzzle(puzzle);
            _currentPuzzleIndex++;
        }

        _display.ShowFinalScore(_score, _puzzles.Count);
    }

    private async Task PlayPuzzle(Puzzle puzzle)
    {
        var current = _currentPuzzleIndex + 1;
        var total = puzzle.CorrectPartitions.Count;

        _display.ShowPuzzle(current, puzzle.GetDescriptionModel(), total);

        List<Partition> playerPartitions;
        switch (puzzle.InputMode)
        {
            case InputMode.AllPartitions:
            {
                int requiredCount = puzzle.CorrectPartitions.Count;
                playerPartitions = await _playerInput.GetMultiplePartitions(puzzle.TargetNumber, requiredCount);
                break;
            }
            case InputMode.SinglePartition:
            {
                playerPartitions = await _playerInput.GetManualPartition(puzzle.TargetNumber);
                break;
            }
            default:
                throw new NotImplementedException($"Unknown input mode: {puzzle.InputMode}");
        }

        bool isValid = true;
        foreach (var partition in playerPartitions)
        {
            if (puzzle.ValidatePartition(partition))
                continue;

            isValid = false;
            _display.ShowPartitionInvalid(partition.ToString());
            break;
        }

        if (!isValid || !puzzle.CheckSolution(playerPartitions))
            return;
        
        _score++;
    }
}