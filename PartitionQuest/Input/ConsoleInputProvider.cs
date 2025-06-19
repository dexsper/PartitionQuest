namespace PartitionQuest.Input;

public class ConsoleInputProvider : IInputProvider
{
    public int ReadNumber(string prompt)
    {
        int num;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
            Console.Write(prompt);
        }
        return num;
    }
}