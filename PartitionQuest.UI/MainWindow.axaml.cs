using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using PartitionQuest.Core;
using PartitionQuest.Core.Puzzles;
using PartitionQuest.UI.Services;

namespace PartitionQuest.UI;

public partial class MainWindow : Window
{
    private readonly UiDisplay _display;
    private readonly UiInputProvider _inputProvider;

    public MainWindow()
    {
        InitializeComponent();

        _display = new UiDisplay();
        _inputProvider = new UiInputProvider();

        _display.OnPuzzleChanged += value => PuzzleText.Text = value;
        _display.OnPuzzleDescChanged += value => PuzzleDescText.Text = value;
        _display.OnPromptChanged += value => PromptText.Text = value;
        _display.OnPartitionChanged += value => PartitionText.Text = value;
        _display.OnInputError += value => ErrorText.Text = value;
        _display.OnGameFinished += OnGameFinished;
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        RunGame();
    }

    private async void RunGame()
    {
        var gameManager = new GameManager(_inputProvider, _display);

        gameManager.AddPuzzle(new BasicPuzzle(5));
        gameManager.AddPuzzle(new OddOnlyPuzzle(6));
        gameManager.AddPuzzle(new DistinctNumbersPuzzle(7));
        gameManager.AddPuzzle(new FixedLengthPuzzle(8, 3));
        gameManager.AddPuzzle(new ExcludeNumberPuzzle(9, 2));
        gameManager.AddPuzzle(new CombinationPuzzle(10, distinctNumbers: true, requiredCount: 3, excludedNumber: 1));

        await gameManager.StartGame();
    }

    private async void OnGameFinished(string message)
    {
        var gameOverWindow = new GameFinishWindow(message);
        var restart = await gameOverWindow.ShowDialog<bool>(this);

        if (!restart)
            return;

        RunGame();
    }

    private void SubmitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SubmitValue();
    }

    private void InputBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        SubmitValue();
    }

    private void SubmitValue()
    {
        _inputProvider.SubmitInput(InputBox.Text ?? string.Empty);
        InputBox.Text = string.Empty;
    }
}