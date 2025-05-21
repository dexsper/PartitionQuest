using PartitionQuest.Models;

namespace PartitionQuest.Tests;

[TestClass]
public class PuzzleTests
{
    [TestMethod]
    public void Puzzle_WithOddOnlyCondition_GeneratesCorrectPartitions()
    {
        var puzzle = new Puzzle(6, PuzzleType.OddOnly);
        Assert.AreEqual(4, puzzle.CorrectPartitions.Count);
        Assert.IsTrue(puzzle.CorrectPartitions.All(p => p.Numbers.All(n => n % 2 != 0)));
    }

    [TestMethod]
    public void CheckSolution_WithCorrectAnswer_ReturnsTrue()
    {
        var puzzle = new Puzzle(4, PuzzleType.DistinctNumbers);
        var correctAnswer = new List<Partition>
        {
            new(new List<int> { 3, 1 }),
            new(new List<int> { 4 })
        };

        Assert.IsTrue(puzzle.CheckSolution(correctAnswer));
    }

    [TestMethod]
    public void CheckSolution_WithIncorrectAnswer_ReturnsFalse()
    {
        var puzzle = new Puzzle(4, PuzzleType.DistinctNumbers);
        var incorrectAnswer = new List<Partition>
        {
            new(new List<int> { 2, 2 }),
            new(new List<int> { 4 })
        };

        Assert.IsFalse(puzzle.CheckSolution(incorrectAnswer));
    }
}