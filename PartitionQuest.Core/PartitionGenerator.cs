using System;
using System.Collections.Generic;
using System.Linq;
using PartitionQuest.Core.Models;

namespace PartitionQuest.Core;

public class PartitionGenerator
{
    public static List<Partition> GeneratePartitions(int n)
    {
        return GeneratePartitions(n, n, new List<int>(), new List<Partition>());
    }

    public static List<Partition> GeneratePartitions(int n, int max, List<int> current, List<Partition> partitions)
    {
        if (n == 0)
        {
            partitions.Add(new Partition(current));
            return partitions;
        }

        for (int i = Math.Min(max, n); i >= 1; i--)
        {
            current.Add(i);
            GeneratePartitions(n - i, i, current, partitions);
            current.RemoveAt(current.Count - 1);
        }

        return partitions;
    }

    public static List<Partition> GenerateOddPartitions(int n)
    {
        var allPartitions = GeneratePartitions(n);
        return allPartitions.Where(p => p.Numbers.All(num => num % 2 != 0)).ToList();
    }

    public static List<Partition> GenerateDistinctPartitions(int n)
    {
        var allPartitions = GeneratePartitions(n);
        return allPartitions.Where(p => p.Numbers.Distinct().Count() == p.Numbers.Count).ToList();
    }

    public static List<Partition> GeneratePartitionsWithFixed(int n, int count)
    {
        var allPartitions = GeneratePartitions(n);
        return allPartitions.Where(p => p.Numbers.Count == count).ToList();
    }

    public static List<Partition> GeneratePartitionsWithout(int n, int excludedNumber)
    {
        var allPartitions = GeneratePartitions(n);
        return allPartitions.Where(p => !p.Numbers.Contains(excludedNumber)).ToList();
    }
}
