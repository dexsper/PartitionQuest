using System;
using System.Collections.Generic;
using PartitionQuest.Core.Display;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.UI.Services;

public class UiDisplay : IDisplay
{
    public event Action<string>? OnPuzzleChanged;
    public event Action<string>? OnPuzzleDescChanged;
    
    public event Action<string>? OnPromptChanged;
    public event Action<string>? OnPartitionChanged;
    public event Action<string>? OnInputError;

    public event Action<string>? OnGameFinished;
    
    public void ShowPuzzle(int number, PuzzleDescription model, int total)
    {
        OnPuzzleChanged?.Invoke(string.Format(Resources.Display.PuzzleHeader, number, total));

        switch (model)
        {
            case BasicPuzzleDescription:
            {
                OnPuzzleDescChanged?.Invoke(Resources.Display.BasicPuzzle);
                break;
            }
            case OddOnlyPuzzleDescription:
            {
                OnPuzzleDescChanged?.Invoke(Resources.Display.OddOnlyPuzzle);
                break;
            }
            case DistinctNumbersPuzzleDescription:
            {
                OnPuzzleDescChanged?.Invoke(Resources.Display.DistinctPuzzle);
                break;
            }
            case FixedLengthPuzzleDescription f:
            {
                OnPuzzleDescChanged?.Invoke(string.Format(Resources.Display.FixedLengthPuzzle, f.RequiredCount));
                break;
            }
            case ExcludeNumberPuzzleDescription e:
            {
                OnPuzzleDescChanged?.Invoke(string.Format(Resources.Display.ExcludePuzzle, e.ExcludedNumber));
                break;
            }
            case CombinationPuzzleDescription c:
            {
                var desc = new List<string>();
                
                if (c.OddOnly)
                    desc.Add(Resources.Display.CombinationOdd);
                
                if (c.Distinct)
                    desc.Add(Resources.Display.DistinctPuzzle);
                
                if (c.RequiredCount.HasValue)
                    desc.Add(string.Format(Resources.Display.FixedLengthPuzzle, c.RequiredCount));
                
                if (c.ExcludedNumber.HasValue)
                    desc.Add(string.Format(Resources.Display.ExcludePuzzle, c.ExcludedNumber));
                
                OnPuzzleDescChanged?.Invoke(string.Join(Environment.NewLine, desc));
                break;
            }
            default:
                throw new NotImplementedException($"Unknown puzzle type: {model.GetType().Name}");
        }
    }

    public void ShowPartitionPrompt(int targetNumber, int sum, int remaining)
    {
        OnPromptChanged?.Invoke(string.Format(Resources.Display.PartitionPrompt, sum, remaining));
    }

    public void ShowInputError()
    {
        OnInputError?.Invoke(Resources.Display.ErrorInput);
    }

    public void ShowDuplicatePartition()
    {
        OnInputError?.Invoke(Resources.Display.DuplicatePartition);
    }

    public void ShowPartitionNumber(int number)
    {
        OnPartitionChanged?.Invoke(string.Format(Resources.Display.PartitionNumber, number));
    }

    public void ShowPartitionInvalid(string partition)
    {
        OnInputError?.Invoke(string.Format(Resources.Display.PartitionInvalid, partition));
    }

    public void ShowFinalScore(int score, int total)
    {
        OnGameFinished?.Invoke(string.Format(Resources.Display.FinalScore, score, total));
    }
}