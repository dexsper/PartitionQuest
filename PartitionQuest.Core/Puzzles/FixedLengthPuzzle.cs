using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class FixedLengthPuzzleDescription : PuzzleDescription
{
    public int RequiredCount { get; internal set; }
}

public class FixedLengthPuzzle : Puzzle
{
    public int RequiredCount { get; }

    public FixedLengthPuzzle(int targetNumber, int requiredCount) : base(targetNumber)
    {
        RequiredCount = requiredCount;
    }

    protected override void GenerateCorrectPartitions()
    {
        CorrectPartitions = PartitionGenerator.GeneratePartitionsWithFixed(TargetNumber, RequiredCount);
    }

    public override bool ValidatePartition(Partition partition)
    {
        return partition.Numbers.Sum() == TargetNumber && partition.Numbers.Count == RequiredCount;
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new FixedLengthPuzzleDescription { TargetNumber = TargetNumber, RequiredCount = RequiredCount };
    }
}