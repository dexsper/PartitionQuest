using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class DistinctNumbersPuzzleDescription : PuzzleDescription
{
}

public class DistinctNumbersPuzzle : Puzzle
{
    public DistinctNumbersPuzzle(int targetNumber) : base(targetNumber)
    {
    }

    protected override void GenerateCorrectPartitions()
    {
        CorrectPartitions = PartitionGenerator.GenerateDistinctPartitions(TargetNumber);
    }

    public override bool ValidatePartition(Partition partition)
    {
        return partition.Numbers.Sum() == TargetNumber &&
               partition.Numbers.Distinct().Count() == partition.Numbers.Count;
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new DistinctNumbersPuzzleDescription { TargetNumber = TargetNumber };
    }
}