using PartitionQuest.Models;

namespace PartitionQuest;

public class Puzzle
{
    public int TargetNumber { get; }
    public PuzzleType Type { get; }
    public int? RequiredCount { get; }
    public int? ExcludedNumber { get; }
    public bool DistinctNumbers { get; }
    public bool OddNumbersOnly { get; }
    public List<Partition> CorrectPartitions { get; private set; } = null!;

    public Puzzle(int targetNumber, PuzzleType type, int? requiredCount = null,
        int? excludedNumber = null, bool distinctNumbers = false,
        bool oddNumbersOnly = false)
    {
        TargetNumber = targetNumber;
        Type = type;
        RequiredCount = requiredCount;
        ExcludedNumber = excludedNumber;
        DistinctNumbers = type == PuzzleType.DistinctNumbers || distinctNumbers;
        OddNumbersOnly = type == PuzzleType.OddOnly || oddNumbersOnly;

        GenerateCorrectPartitions();
    }

    private void GenerateCorrectPartitions()
    {
        switch (Type)
        {
            case PuzzleType.Basic:
            {
                CorrectPartitions = PartitionGenerator.GeneratePartitions(TargetNumber);
                break;
            }
            case PuzzleType.OddOnly:
            {
                CorrectPartitions = PartitionGenerator.GenerateOddPartitions(TargetNumber);
                break;
            }
            case PuzzleType.DistinctNumbers:
            {
                CorrectPartitions = PartitionGenerator.GenerateDistinctPartitions(TargetNumber);
                break;
            }
            case PuzzleType.FixedLength:
            {
                if (!RequiredCount.HasValue)
                    throw new Exception("RequiredCount must be set for FixedLength puzzle type");

                CorrectPartitions = PartitionGenerator.GeneratePartitionsWithFixed(TargetNumber, RequiredCount.Value);
                break;
            }
            case PuzzleType.ExcludeNumber:
            {
                if (!ExcludedNumber.HasValue)
                    throw new Exception("ExcludedNumber must be set for ExcludeNumber puzzle type");

                CorrectPartitions = PartitionGenerator.GeneratePartitionsWithout(TargetNumber, ExcludedNumber.Value);
                break;
            }
            case PuzzleType.Combination:
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
                break;
            }
        }
    }

    public bool CheckSolution(List<Partition> playerPartitions)
    {
        if (playerPartitions.Count != CorrectPartitions.Count)
            return false;

        foreach (var partition in playerPartitions)
        {
            if (!CorrectPartitions.Contains(partition))
                return false;
        }

        return true;
    }
    
    public bool ValidatePartition(Partition partition)
    {
        if (partition.Numbers.Sum() != TargetNumber)
            return false;

        var hasDuplicates = partition.Numbers.Count != partition.Numbers.Distinct().Count();
        if (DistinctNumbers && hasDuplicates)
            return false;

        if (OddNumbersOnly && partition.Numbers.Any(num => num % 2 == 0))
            return false;

        if (RequiredCount.HasValue && partition.Numbers.Count != RequiredCount.Value)
            return false;

        return !ExcludedNumber.HasValue || !partition.Numbers.Contains(ExcludedNumber.Value);
    }
}