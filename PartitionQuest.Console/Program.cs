using PartitionQuest;
using PartitionQuest.Core;
using PartitionQuest.Core.Puzzles;

var display = new ConsoleDisplay();
var input = new ConsoleInputProvider();
var gameManager = new GameManager(input, display);

gameManager.AddPuzzle(new BasicPuzzle(5));
gameManager.AddPuzzle(new OddOnlyPuzzle(6));
gameManager.AddPuzzle(new DistinctNumbersPuzzle(7));
gameManager.AddPuzzle(new FixedLengthPuzzle(8, 3));
gameManager.AddPuzzle(new ExcludeNumberPuzzle(9, 2));
gameManager.AddPuzzle(new CombinationPuzzle(10, distinctNumbers: true, requiredCount: 3, excludedNumber: 1));

Console.WriteLine("Добро пожаловать в Partition Quest!");
Console.WriteLine("Ваша задача - находить разбиения чисел согласно условиям.\n");

await gameManager.StartGame();