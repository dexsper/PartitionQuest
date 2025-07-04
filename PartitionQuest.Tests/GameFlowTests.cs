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

        Assert.IsTrue(puzzle.ValidatePartition(validPartition));
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

        Assert.IsFalse(puzzle.ValidatePartition(invalid1),
            "Разбиение [2,2,1] должно быть невалидным из-за дубликатов");
        Assert.IsFalse(puzzle.ValidatePartition(invalid2),
            "Разбиение [1,1,3] должно быть невалидным из-за дубликатов");

        Assert.IsTrue(puzzle.ValidatePartition(valid1),
            "Разбиение [4,1] должно быть валидным");
        Assert.IsTrue(puzzle.ValidatePartition(valid2),
            "Разбиение [3,2] должно быть валидным");
        Assert.IsTrue(puzzle.ValidatePartition(valid3),
            "Разбиение [5] должно быть валидным");
    }

    [TestMethod]
    public void GameFlow_DoesNotAcceptDuplicatePartitions()
    {
        var puzzle = new Puzzle(4, PuzzleType.Basic);
        var testInput = new TestInput(
        [
            4, // #1
            3, 1, // #2
            2, 2, // #3
            2, 1, 1, //#4
            2, 2, // duplicate of #3
            1, 1, 1, 1, // #5
        ]);

        var mockDisplay = new MockDisplay();
        var gm2 = new GameManager(testInput, mockDisplay);
        gm2.AddPuzzle(puzzle);
        gm2.StartGame();

        Assert.IsTrue(mockDisplay.Messages.Contains("duplicate"),
            "Дубликат должен быть отклонён");
        Assert.IsTrue(mockDisplay.Messages.Contains("success"),
            "Должно быть поздравление об успешном завершении");
    }
}