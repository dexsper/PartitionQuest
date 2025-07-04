using System.Collections.Generic;
using PartitionQuest.Core.Puzzles;

namespace PartitionQuest.Core.Display;

public interface IDisplay
{
    void ShowWelcome();
    void ShowGameRules();
    void ShowPuzzleHeader(int puzzleNumber);
    void ShowPuzzleDescription(PuzzleDescription model);
    void ShowNeedAllPartitions(int count);
    void ShowTotalPartitions(int count);
    void ShowPartitionPrompt(int targetNumber, int sum, int remaining);
    void ShowErrorInput();
    void ShowErrorSumMismatch();
    void ShowErrorOutOfRange();
    void ShowErrorOverflow();
    void ShowDuplicatePartition();
    void ShowPartitionNumber(int number);
    void ShowPartitionInvalid(string partition);
    void ShowSuccess();
    void ShowFailure(IEnumerable<string> correctPartitions);
    void ShowNextPuzzle();
    void ShowFinalScore(int score, int total);
    void ShowManualPartitionIntro(int targetNumber);
} 