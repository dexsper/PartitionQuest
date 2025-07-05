using PartitionQuest.Core.Models;

namespace PartitionQuest.Tests;

[TestClass]
public class PartitionTests
{
    [DataTestMethod]
    [DataRow(new[] { 3, 2, 1 })]
    public void ToString_ReturnsCorrectFormat(int[] values)
    {
        var p = new Partition(values);
        Assert.AreEqual(string.Join(" + ", values), p.ToString());
    }

    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
    public void Equals_SameNumbersDifferentOrder_ReturnsTrue(int[] first, int[] second)
    {
        var p1 = new Partition(first);
        var p2 = new Partition(second);
        
        Assert.IsTrue(p1.Equals(p2));
    }

    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 4 })]
    public void Equals_DifferentNumbers_ReturnsFalse(int[] first, int[] second)
    {
        var p1 = new Partition(first);
        var p2 = new Partition(second);
        
        Assert.IsFalse(p1.Equals(p2));
    }

    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
    public void GetHashCode_SameNumbersDifferentOrder_SameHash(int[] first, int[] second)
    {
        var p1 = new Partition(first);
        var p2 = new Partition(second);
        
        Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
    }

    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 4 })]
    public void GetHashCode_DifferentNumbers_DifferentHash(int[] first, int[] second)
    {
        var p1 = new Partition(first);
        var p2 = new Partition(second);

        Assert.AreNotEqual(p1.GetHashCode(), p2.GetHashCode());
    }

    [TestMethod]
    public void EmptyPartition_ToStringAndEquality()
    {
        var p1 = new Partition(Array.Empty<int>());
        var p2 = new Partition(Array.Empty<int>());
        
        Assert.AreEqual("", p1.ToString());
        Assert.IsTrue(p1.Equals(p2));
        Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
    }
}