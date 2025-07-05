using PartitionQuest.Core.Puzzles;
using PartitionQuest.Tests.Mocks;
using PartitionQuest.Tests.Utils;

namespace PartitionQuest.Tests;

[TestClass]
public class DisplayTests
{
    [TestMethod]
    public void ShowPuzzle_HandleUnknownDescriptionType()
    {
        var mockDisplay = new MockDisplay();
        var consoleDisplay = new ConsoleDisplay();

        var puzzle = new UnknownPuzzle(0);

        Assert.ThrowsException<NotImplementedException>(() => mockDisplay.ShowPuzzle(0, puzzle.GetDescriptionModel(), 0),
            "Expected for mockDisplay");
        
        Assert.ThrowsException<NotImplementedException>(() => consoleDisplay.ShowPuzzle(0, puzzle.GetDescriptionModel(), 0),
            "Expected for consoleDisplay");
    }

    [TestMethod]
    public void ShowPuzzle_HandlesAllPuzzleDescriptionTypes()
    {
        var mockDisplay = new MockDisplay();
        var consoleDisplay = new ConsoleDisplay();

        var puzzles = typeof(Puzzle).Assembly.CreateAllDerivedInstances<Puzzle>();
        Assert.IsTrue(puzzles.Any(), "No Puzzle instances found to test.");

        int i = 1;
        foreach (var puzzle in puzzles)
        {
            try
            {
                mockDisplay.ShowPuzzle(i++, puzzle.GetDescriptionModel(), 42);
                consoleDisplay.ShowPuzzle(i++, puzzle.GetDescriptionModel(), 42);
            }
            catch (NotImplementedException ex)
            {
                Assert.Fail($"Puzzle {puzzle.GetType().Name} failed with NotImplementedException: {ex.Message}");
            }
        }
    }
}