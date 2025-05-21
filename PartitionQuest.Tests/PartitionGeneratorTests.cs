namespace PartitionQuest.Tests;

[TestClass]
public class PartitionGeneratorTests
{
    [TestMethod]
    public void GeneratePartitions_For5_Returns7Partitions()
    {
        var partitions = PartitionGenerator.GeneratePartitions(5);
        Assert.AreEqual(7, partitions.Count);
    }

    [TestMethod]
    public void GenerateOddPartitions_For8_ReturnsCorrectPartitions()
    {
        var partitions = PartitionGenerator.GenerateOddPartitions(8);
        Assert.AreEqual(6, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.All(n => n % 2 != 0)));
    }

    [TestMethod]
    public void GenerateDistinctPartitions_For6_ReturnsCorrectPartitions()
    {
        var partitions = PartitionGenerator.GenerateDistinctPartitions(6);
        Assert.AreEqual(4, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.Distinct().Count() == p.Numbers.Count));
    }

    [TestMethod]
    public void GeneratePartitionsWithFixedCount_For7And3_ReturnsCorrectPartitions()
    {
        var partitions = PartitionGenerator.GeneratePartitionsWithFixed(7, 3);
        Assert.AreEqual(4, partitions.Count);
        Assert.IsTrue(partitions.All(p => p.Numbers.Count == 3));
    }

    [TestMethod]
    public void GeneratePartitionsWithoutNumber_For9Without2_ReturnsCorrectPartitions()
    {
        var partitions = PartitionGenerator.GeneratePartitionsWithout(9, 2);
        Assert.AreEqual(15, partitions.Count);
        Assert.IsTrue(partitions.All(p => !p.Numbers.Contains(2)));
    }
}