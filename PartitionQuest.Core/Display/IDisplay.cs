using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Core.Display;

public interface IDisplay
{
    void ShowPuzzle(int number, PuzzleDescription model, int total);
    void ShowPartitionPrompt(int targetNumber, int sum, int remaining);
    void ShowInputError();
    void ShowDuplicatePartition();
    void ShowPartitionNumber(int number);
    void ShowPartitionInvalid(string partition);
    void ShowFinalScore(int score, int total);
} 