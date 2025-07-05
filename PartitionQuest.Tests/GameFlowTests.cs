using PartitionQuest.Core;
using PartitionQuest.Core.Puzzles;
using PartitionQuest.Tests.Mocks;

namespace PartitionQuest.Tests;

[TestClass]
public class GameFlowTests
{
    [DataTestMethod]
    [DynamicData(nameof(GetDuplicatePartitionData), DynamicDataSourceType.Method)]
    public void DoesNotAcceptDuplicatePartitions(int target, int[] values)
    {
        var puzzle = new BasicPuzzle(target);
        var testInput = new MockInputProvider(values);

        var mockDisplay = new MockDisplay();
        var gameManager = new GameManager(testInput, mockDisplay);

        gameManager.AddPuzzle(puzzle);
        gameManager.StartGame();

        Assert.IsTrue(mockDisplay.Messages.Contains("duplicate"),
            "The duplicate must be rejected.");

        Assert.IsTrue(mockDisplay.Messages.Contains("success"),
            "There should be congratulations on the successful completion");
    }

    private static IEnumerable<object[]> GetDuplicatePartitionData()
    {
        yield return
        [
            2, new[]
            {
                2, // #1
                2, // duplicate of #1
                1, // #2
                1 // #3
            }
        ];

        yield return
        [
            4, new[]
            {
                4, // #1
                3, 1, // #2
                2, 2, // #3
                2, 1, 1, //#4
                2, 2, // duplicate of #3
                1, 1, 1, 1, // #5
            }
        ];
    }
}