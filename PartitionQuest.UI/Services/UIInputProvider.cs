using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using PartitionQuest.Core.Input;

namespace PartitionQuest.UI.Services;

public class UiInputProvider : IInputProvider, INotifyPropertyChanged
{
    private readonly ConcurrentQueue<string> _inputQueue = new();
    private readonly SemaphoreSlim _inputSignal = new(0);
    private bool _canAcceptInput;

    public bool CanAcceptInput
    {
        get => _canAcceptInput;
        private set
        {
            if (_canAcceptInput == value)
                return;

            _canAcceptInput = value;
            OnPropertyChanged(nameof(CanAcceptInput));
        }
    }

    public async Task<int?> ReadNumberAsync()
    {
        CanAcceptInput = true;
        await _inputSignal.WaitAsync();

        if (!_inputQueue.TryDequeue(out var input))
            throw new InvalidOperationException("Expected input value was not available in the queue.");

        if (!int.TryParse(input, out int num)) 
            return null;
        
        CanAcceptInput = false;
        return num;
    }

    public void SubmitInput(string value)
    {
        if (!CanAcceptInput)
            return;

        _inputQueue.Enqueue(value);
        _inputSignal.Release();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}