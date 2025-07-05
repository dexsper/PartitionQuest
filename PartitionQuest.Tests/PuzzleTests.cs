using PartitionQuest.Core.Models;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Tests;

[TestClass]
public class PuzzleTests
{
    [DataTestMethod]
    [DataRow(typeof(BasicPuzzle), 5, new[] { 3, 2 })]
    public void ValidatePartition_WithValid_ReturnsTrue(Type puzzleType, int target, int[] partition)
    {
        var puzzle = (Puzzle)Activator.CreateInstance(puzzleType, target)!;
        var validPartition = new Partition(partition);

        Assert.IsTrue(puzzle.ValidatePartition(validPartition));
    }

    [DataTestMethod]
    [DataRow(typeof(DistinctNumbersPuzzle), 5, new[] { 2, 2, 1 })]
    [DataRow(typeof(DistinctNumbersPuzzle), 5, new[] { 1, 1, 3 })]
    public void ValidatePartition_WithInvalid_ReturnsFalse(Type puzzleType, int target, int[] partition)
    {
        var puzzle = (Puzzle)Activator.CreateInstance(puzzleType, target)!;
        var invalidPartition = new Partition(partition);

        Assert.IsFalse(puzzle.ValidatePartition(invalidPartition));
    }

    [DataTestMethod]
    [DataRow(typeof(DistinctNumbersPuzzle), 5, new[] { 4, 1 })]
    [DataRow(typeof(DistinctNumbersPuzzle), 5, new[] { 3, 2 })]
    [DataRow(typeof(DistinctNumbersPuzzle), 5, new[] { 5 })]
    public void ValidatePartition_WithValidDistinct_ReturnsTrue(Type puzzleType, int target, int[] partition)
    {
        var puzzle = (Puzzle)Activator.CreateInstance(puzzleType, target)!;
        var validPartition = new Partition(partition);

        Assert.IsTrue(puzzle.ValidatePartition(validPartition));
    }

    [DataTestMethod]
    [DataRow(6, 4)]
    public void OddOnly_GeneratesCorrectPartitions(int target, int excepted)
    {
        var puzzle = new OddOnlyPuzzle(target);

        Assert.AreEqual(excepted, puzzle.CorrectPartitions.Count);
        Assert.IsTrue(puzzle.CorrectPartitions.All(p => p.Numbers.All(n => n % 2 != 0)));
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCorrectAnswers), DynamicDataSourceType.Method)]
    public void CheckSolution_WithCorrect_ReturnsTrue(int target, List<List<int>> values)
    {
        var puzzle = new DistinctNumbersPuzzle(target);
        var correctAnswer = values.Select(v => new Partition(v)).ToList();

        Assert.IsTrue(puzzle.CheckSolution(correctAnswer));
    }

    [TestMethod]
    [DynamicData(nameof(GetIncorrectAnswers), DynamicDataSourceType.Method)]
    public void CheckSolution_WithIncorrect_ReturnsFalse(int target, List<List<int>> values)
    {
        var puzzle = new DistinctNumbersPuzzle(target);
        var incorrectAnswer = values.Select(v => new Partition(v)).ToList();

        Assert.IsFalse(puzzle.CheckSolution(incorrectAnswer));
    }
    
    private static IEnumerable<object[]> GetCorrectAnswers()
    {
        yield return
        [
            4,
            new List<List<int>>
            {
                new() { 3, 1 },
                new() { 4 }
            }
        ];
    }

    private static IEnumerable<object[]> GetIncorrectAnswers()
    {
        yield return
        [
            4,
            new List<List<int>>
            {
                new() { 2, 2 },
                new() { 4 }
            }
        ];
    }
}