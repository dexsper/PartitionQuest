using PartitionQuest.Core.Display;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Tests.Mocks;

public class MockDisplay : IDisplay
{
    public List<string> Messages { get; } = new();
    
    public void ShowPuzzle(int number, PuzzleDescription model, int total)
    {
        Messages.Add($"header:{number}");

        switch (model)
        {
            case BasicPuzzleDescription b:
            {
                Messages.Add($"desc:basic:{b.TargetNumber}");
                break;
            }
            case OddOnlyPuzzleDescription o:
            {
                Messages.Add($"desc:odd:{o.TargetNumber}");
                break;
            }
            case DistinctNumbersPuzzleDescription d:
            {
                Messages.Add($"desc:distinct:{d.TargetNumber}");
                break;
            }
            case FixedLengthPuzzleDescription f:
            {
                Messages.Add($"desc:fixed:{f.TargetNumber}:count={f.RequiredCount}");
                break;
            }
            case ExcludeNumberPuzzleDescription e:
            {
                Messages.Add($"desc:exclude:{e.TargetNumber}:exclude={e.ExcludedNumber}");
                break;
            }
            case CombinationPuzzleDescription c:
            {
                var msg = $"desc:comb:{c.TargetNumber}";
                if (c.OddOnly) msg += ":odd=true";
                if (c.Distinct) msg += ":distinct=true";
                if (c.RequiredCount.HasValue) msg += $":count={c.RequiredCount}";
                if (c.ExcludedNumber.HasValue) msg += $":exclude={c.ExcludedNumber}";
                
                Messages.Add(msg);
                break;
            }
            default:
                throw new NotImplementedException($"Unknown type of description: {model.GetType().Name}");
        }

        Messages.Add($"total:{total}");
    }
    
    public void ShowPartitionPrompt(int targetNumber, int sum, int remaining) =>
        Messages.Add($"prompt:{targetNumber}:{sum}:{remaining}");

    public void ShowInputError() => Messages.Add("errorinput");
    public void ShowDuplicatePartition() => Messages.Add("duplicate");
    public void ShowPartitionNumber(int number) => Messages.Add($"partnum:{number}");
    public void ShowPartitionInvalid(string partition) => Messages.Add($"invalid:{partition}");
    public void ShowFinalScore(int score, int total) => Messages.Add($"final:{score}:{total}");
}