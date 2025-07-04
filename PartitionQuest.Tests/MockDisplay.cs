using PartitionQuest.Display;

public class MockDisplay : IDisplay
{
    public List<string> Messages { get; } = new();

    public void ShowWelcome() => Messages.Add("welcome");
    public void ShowGameRules() => Messages.Add("rules");
    public void ShowPuzzleHeader(int puzzleNumber) => Messages.Add($"header:{puzzleNumber}");
    public void ShowPuzzleBasic(int targetNumber) => Messages.Add($"basic:{targetNumber}");
    public void ShowPuzzleOddOnly(int targetNumber) => Messages.Add($"odd:{targetNumber}");
    public void ShowPuzzleDistinct(int targetNumber) => Messages.Add($"distinct:{targetNumber}");
    public void ShowPuzzleFixedLength(int targetNumber, int count) => Messages.Add($"fixed:{targetNumber}:{count}");
    public void ShowPuzzleExcludeNumber(int targetNumber, int excluded) => Messages.Add($"exclude:{targetNumber}:{excluded}");
    public void ShowPuzzleCombination(int targetNumber, bool odd, bool distinct, int? count, int? excluded) => Messages.Add($"comb:{targetNumber}:{odd}:{distinct}:{count}:{excluded}");
    public void ShowNeedAllPartitions(int count) => Messages.Add($"needall:{count}");
    public void ShowTotalPartitions(int count) => Messages.Add($"total:{count}");
    public void ShowPartitionPrompt(int targetNumber, int sum, int remaining) => Messages.Add($"prompt:{targetNumber}:{sum}:{remaining}");
    public void ShowErrorInput() => Messages.Add("errorinput");
    public void ShowErrorSumMismatch() => Messages.Add("errormismatch");
    public void ShowErrorOutOfRange() => Messages.Add("errorrange");
    public void ShowErrorOverflow() => Messages.Add("erroroverflow");
    public void ShowDuplicatePartition() => Messages.Add("duplicate");
    public void ShowPartitionNumber(int number) => Messages.Add($"partnum:{number}");
    public void ShowPartitionInvalid(string partition) => Messages.Add($"invalid:{partition}");
    public void ShowSuccess() => Messages.Add("success");
    public void ShowFailure(IEnumerable<string> correctPartitions) => Messages.Add("failure");
    public void ShowNextPuzzle() => Messages.Add("next");
    public void ShowFinalScore(int score, int total) => Messages.Add($"final:{score}:{total}");
    public void ShowPressAnyKey() => Messages.Add("pressany");
    public void ShowManualPartitionIntro(int targetNumber) => Messages.Add($"manualintro:{targetNumber}");
} 