namespace PartitionQuest.Core.Input;

public interface IInputProvider
{
    int ReadNumber(string prompt);
}