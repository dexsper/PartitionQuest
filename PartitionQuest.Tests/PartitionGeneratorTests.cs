using PartitionQuest.Core;

namespace PartitionQuest.Tests;

[TestClass]
public class PartitionGeneratorTests
{
    [DataTestMethod]
    [DataRow(5, 7)]
    public void GeneratePartitions_Returns7Partitions(int target, int excepted)
    {
        var partitions = PartitionGenerator.GeneratePartitions(target);
        
        Assert.AreEqual(excepted, partitions.Count);
    }

    [DataTestMethod]
    [DataRow(8, 6)]
    public void GeneratePartitions_Odd_ReturnsCorrectPartitions(int target, int excepted)
    {
        var partitions = PartitionGenerator.GenerateOddPartitions(target);
        
        Assert.AreEqual(excepted, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.All(n => n % 2 != 0)));
    }

    [DataTestMethod]
    [DataRow(6, 4)]
    public void GeneratePartitions_Distinct_ReturnsCorrectPartitions(int target, int excepted)
    {
        var partitions = PartitionGenerator.GenerateDistinctPartitions(target);
        
        Assert.AreEqual(excepted, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.Distinct().Count() == p.Numbers.Count));
    }

    [DataTestMethod]
    [DataRow(7, 3, 4)]
    public void GeneratePartitions_FixedCount_ReturnsCorrectPartitions(int target, int length, int excepted)
    {
        var partitions = PartitionGenerator.GeneratePartitionsWithFixed(target, length);
        
        Assert.AreEqual(excepted, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.Count == length));
    }

    [DataTestMethod]
    [DataRow(9, 2, 15)]
    public void GeneratePartitions_WithoutNumber_ReturnsCorrectPartitions(int target, int without, int excepted)
    {
        var partitions = PartitionGenerator.GeneratePartitionsWithout(target, without);
        
        Assert.AreEqual(excepted, partitions.Count);
        Assert.IsTrue(partitions.All(p => !p.Numbers.Contains(without)));
    }

    [TestMethod]
    public void GeneratePartitions_ZeroOrNegative_ReturnsEmpty()
    {
        var zero = PartitionGenerator.GeneratePartitions(0);
        
        Assert.AreEqual(1, zero.Count);
        Assert.IsTrue(zero[0].Numbers.Count == 0);
        Assert.AreEqual(0, PartitionGenerator.GeneratePartitions(-5).Count);
    }

    [TestMethod]
    public void GeneratePartitionsWithFixed_ImpossibleCount_ReturnsEmpty()
    {
        Assert.AreEqual(0, PartitionGenerator.GeneratePartitionsWithFixed(3, 5).Count);
        Assert.AreEqual(0, PartitionGenerator.GeneratePartitionsWithFixed(0, 1).Count);
        Assert.AreEqual(0, PartitionGenerator.GeneratePartitionsWithFixed(-1, 2).Count);
    }

    [TestMethod]
    public void GeneratePartitionsWithout_ExcludedNumberNotPresentOrInvalid()
    {
        var all = PartitionGenerator.GeneratePartitions(4);
        var without = PartitionGenerator.GeneratePartitionsWithout(4, 10);
        Assert.AreEqual(all.Count, without.Count);

        var withoutZero = PartitionGenerator.GeneratePartitionsWithout(4, 0);
        Assert.AreEqual(all.Count, withoutZero.Count);
    }
}