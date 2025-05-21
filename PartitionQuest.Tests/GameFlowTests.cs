using PartitionQuest.Models;

namespace PartitionQuest.Tests;

[TestClass]
public class GameFlowTests
{
    [TestMethod]
    public void PlayerInput_ValidatePartition_WithValidPartition_ReturnsTrue()
    {
        var puzzle = new Puzzle(5, PuzzleType.Basic);
        var validPartition = new Partition(new List<int> { 3, 2 });

        Assert.IsTrue(PlayerInput.ValidatePartition(validPartition, puzzle));
    }

    [TestMethod]
    public void PlayerInput_ValidatePartition_WithInvalidPartition_ReturnsFalse()
    {
        var puzzle = new Puzzle(5, PuzzleType.DistinctNumbers);
        
        var invalid1 = new Partition(new List<int> { 2, 2, 1 });
        var invalid2 = new Partition(new List<int> { 1, 1, 3 });
        
        var valid1 = new Partition(new List<int> { 4, 1 });
        var valid2 = new Partition(new List<int> { 3, 2 });
        var valid3 = new Partition(new List<int> { 5 });
        
        Assert.IsFalse(PlayerInput.ValidatePartition(invalid1, puzzle), 
            "Разбиение [2,2,1] должно быть невалидным из-за дубликатов");
        Assert.IsFalse(PlayerInput.ValidatePartition(invalid2, puzzle),
            "Разбиение [1,1,3] должно быть невалидным из-за дубликатов");
        
        Assert.IsTrue(PlayerInput.ValidatePartition(valid1, puzzle),
            "Разбиение [4,1] должно быть валидным");
        Assert.IsTrue(PlayerInput.ValidatePartition(valid2, puzzle),
            "Разбиение [3,2] должно быть валидным");
        Assert.IsTrue(PlayerInput.ValidatePartition(valid3, puzzle),
            "Разбиение [5] должно быть валидным");
    }
}