using System.Collections.Generic;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core.Puzzles;

public abstract class PuzzleDescription
{
    public int TargetNumber { get; set; }
}

public abstract class Puzzle
{
    public int TargetNumber { get; }
    public List<Partition> CorrectPartitions { get; protected set; } = null!;

    protected Puzzle(int targetNumber)
    {
        TargetNumber = targetNumber;
        GenerateCorrectPartitions();
    }

    protected abstract void GenerateCorrectPartitions();

    public virtual bool CheckSolution(List<Partition> playerPartitions)
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

    public abstract bool ValidatePartition(Partition partition);

    public abstract PuzzleDescription GetDescriptionModel();
}