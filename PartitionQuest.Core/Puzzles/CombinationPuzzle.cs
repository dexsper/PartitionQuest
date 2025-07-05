using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public class CombinationPuzzleDescription : PuzzleDescription
{
    public bool OddOnly { get; internal set; }
    public bool Distinct { get; internal set; }
    public int? RequiredCount { get; internal set; }
    public int? ExcludedNumber { get; internal set; }
}

public class CombinationPuzzle : Puzzle
{
    public bool OddNumbersOnly { get; }
    public bool DistinctNumbers { get; }
    public int? RequiredCount { get; }
    public int? ExcludedNumber { get; }

    public override InputMode InputMode => InputMode.SinglePartition;

    public CombinationPuzzle(
        int targetNumber,
        bool oddNumbersOnly = false,
        bool distinctNumbers = false,
        int? requiredCount = null,
        int? excludedNumber = null)
        : base(targetNumber)
    {
        OddNumbersOnly = oddNumbersOnly;
        DistinctNumbers = distinctNumbers;
        RequiredCount = requiredCount;
        ExcludedNumber = excludedNumber;
    }

    protected override void GenerateCorrectPartitions()
    {
        var partitions = PartitionGenerator.GeneratePartitions(TargetNumber);

        if (OddNumbersOnly)
            partitions = partitions.Where(p => p.Numbers.All(num => num % 2 != 0)).ToList();

        if (DistinctNumbers)
            partitions = partitions.Where(p => p.Numbers.Distinct().Count() == p.Numbers.Count).ToList();

        if (RequiredCount.HasValue)
            partitions = partitions.Where(p => p.Numbers.Count == RequiredCount.Value).ToList();

        if (ExcludedNumber.HasValue)
            partitions = partitions.Where(p => !p.Numbers.Contains(ExcludedNumber.Value)).ToList();

        CorrectPartitions = partitions;
    }

    public override bool ValidatePartition(Partition partition)
    {
        if (partition.Numbers.Sum() != TargetNumber)
            return false;

        if (OddNumbersOnly && partition.Numbers.Any(num => num % 2 == 0))
            return false;

        if (DistinctNumbers && partition.Numbers.Count != partition.Numbers.Distinct().Count())
            return false;

        if (RequiredCount.HasValue && partition.Numbers.Count != RequiredCount.Value)
            return false;

        return !ExcludedNumber.HasValue || !partition.Numbers.Contains(ExcludedNumber.Value);
    }

    public override PuzzleDescription GetDescriptionModel()
    {
        return new CombinationPuzzleDescription
        {
            TargetNumber = TargetNumber,
            OddOnly = OddNumbersOnly,
            Distinct = DistinctNumbers,
            RequiredCount = RequiredCount,
            ExcludedNumber = ExcludedNumber
        };
    }
}