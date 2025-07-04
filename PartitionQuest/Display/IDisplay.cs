namespace PartitionQuest.Display;

public interface IDisplay
{
    void ShowWelcome();
    void ShowGameRules();
    void ShowPuzzleHeader(int puzzleNumber);
    void ShowPuzzleBasic(int targetNumber);
    void ShowPuzzleOddOnly(int targetNumber);
    void ShowPuzzleDistinct(int targetNumber);
    void ShowPuzzleFixedLength(int targetNumber, int count);
    void ShowPuzzleExcludeNumber(int targetNumber, int excluded);
    void ShowPuzzleCombination(int targetNumber, bool odd, bool distinct, int? count, int? excluded);
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
    void ShowPressAnyKey();
    void ShowManualPartitionIntro(int targetNumber);
} 