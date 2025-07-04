using PartitionQuest;
using PartitionQuest.Display;
using PartitionQuest.Input;

var display = new ConsoleDisplay();
var input = new ConsoleInput(display);
var gameManager = new GameManager(input, display);

gameManager.AddPuzzle(new Puzzle(5, PuzzleType.Basic));
gameManager.AddPuzzle(new Puzzle(6, PuzzleType.OddOnly));
gameManager.AddPuzzle(new Puzzle(7, PuzzleType.DistinctNumbers));
gameManager.AddPuzzle(new Puzzle(8, PuzzleType.FixedLength, requiredCount: 3));
gameManager.AddPuzzle(new Puzzle(9, PuzzleType.ExcludeNumber, excludedNumber: 2));
gameManager.AddPuzzle(new Puzzle(10, PuzzleType.Combination, requiredCount: 3, excludedNumber: 1, distinctNumbers: true));

gameManager.StartGame();
display.ShowPressAnyKey();