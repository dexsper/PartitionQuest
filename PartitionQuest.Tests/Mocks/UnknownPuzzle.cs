using PartitionQuest.Core.Models;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Tests.Mocks;

public class UnknownPuzzleDescription : PuzzleDescription;

public class UnknownPuzzle : Puzzle
{
    public UnknownPuzzle(int targetNumber) : base(targetNumber)
    {
    }

    public override InputMode InputMode => InputMode.SinglePartition;

    protected override void GenerateCorrectPartitions()
    {
    }

    public override bool ValidatePartition(Partition partition)
    {
        return true;
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new UnknownPuzzleDescription();
    }
}