using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class ExcludeNumberPuzzleDescription : PuzzleDescription
{
    public int ExcludedNumber { get; internal set; }
} 

public class ExcludeNumberPuzzle : Puzzle
{
    public int ExcludedNumber { get; }
    public ExcludeNumberPuzzle(int targetNumber, int excludedNumber) : base(targetNumber)
    {
        ExcludedNumber = excludedNumber;
    }

    protected override void GenerateCorrectPartitions()
    {
        CorrectPartitions = PartitionGenerator.GeneratePartitionsWithout(TargetNumber, ExcludedNumber);
    }

    public override bool ValidatePartition(Partition partition)
    {
        return partition.Numbers.Sum() == TargetNumber && !partition.Numbers.Contains(ExcludedNumber);
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new ExcludeNumberPuzzleDescription { TargetNumber = TargetNumber, ExcludedNumber = ExcludedNumber };
    }
}