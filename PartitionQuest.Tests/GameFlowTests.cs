using PartitionQuest.Input;
using PartitionQuest.Models;

namespace PartitionQuest.Tests;

public class TestInputProvider : IInputProvider
{
    private readonly Queue<int> _inputs;

    public TestInputProvider(IEnumerable<int> inputs)
    {
        _inputs = new Queue<int>(inputs);
    }

    public int ReadNumber(string prompt)
    {
        if (_inputs.Count == 0)
            throw new InvalidOperationException("Нет больше тестовых данных для ввода");
        return _inputs.Dequeue();
    }
}

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

    [TestMethod]
    public void GameFlow_DoesNotAcceptDuplicatePartitions()
    {
        var puzzle = new Puzzle(4, PuzzleType.Basic);
        var testInput = new TestInputProvider(
        [
            4, // #1
            3, 1, // #2
            2, 2, // #3
            2, 1, 1, //#4
            2, 2, // duplicate of #3
            1, 1, 1, 1, // #5
        ]);

        var gm = new GameManager(testInput);
        gm.AddPuzzle(puzzle);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        gm.StartGame();
        var output = sw.ToString();

        Assert.IsTrue(output.Contains("Такое разбиение уже было введено! Введите другое."),
            "Дубликат должен быть отклонён");
        
        Assert.IsTrue(output.Contains("Поздравляем! Все разбиения верны."),
            "Должно быть поздравление об успешном завершении");
    }
}