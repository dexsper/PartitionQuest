using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class OddOnlyPuzzleDescription : PuzzleDescription
{
}

public class OddOnlyPuzzle : Puzzle
{
    public OddOnlyPuzzle(int targetNumber) : base(targetNumber)
    {
    }

    protected override void GenerateCorrectPartitions()
    {
        CorrectPartitions = PartitionGenerator.GenerateOddPartitions(TargetNumber);
    }

    public override bool ValidatePartition(Partition partition)
    {
        return partition.Numbers.Sum() == TargetNumber && partition.Numbers.All(n => n % 2 != 0);
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new OddOnlyPuzzleDescription { TargetNumber = TargetNumber };
    }
}