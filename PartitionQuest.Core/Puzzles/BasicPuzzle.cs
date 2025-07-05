using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class BasicPuzzleDescription : PuzzleDescription
{
}

public class BasicPuzzle : Puzzle
{
    public override InputMode InputMode => InputMode.AllPartitions;

    public BasicPuzzle(int targetNumber) : base(targetNumber)
    {
    }

    protected override void GenerateCorrectPartitions()
    {
        CorrectPartitions = PartitionGenerator.GeneratePartitions(TargetNumber);
    }

    public override bool ValidatePartition(Partition partition)
    {
        return partition.Numbers.Sum() == TargetNumber;
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new BasicPuzzleDescription { TargetNumber = TargetNumber };
    }
}