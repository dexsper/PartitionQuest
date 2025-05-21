namespace PartitionQuest.Models;

public class Partition
{
    public List<int> Numbers { get; }

    public Partition()
    {
        Numbers = new List<int>();
    }

    public Partition(IEnumerable<int> numbers)
    {
        Numbers = new List<int>(numbers);
    }

    public override string ToString()
    {
        return string.Join(" + ", Numbers);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Partition other) 
            return false;
        
        if (Numbers.Count != other.Numbers.Count)
            return false;

        var sortedThis = new List<int>(Numbers).OrderBy(x => x).ToList();
        var sortedOther = new List<int>(other.Numbers).OrderBy(x => x).ToList();

        return sortedThis.SequenceEqual(sortedOther);
    }

    public override int GetHashCode()
    {
        return Numbers.OrderBy(x => x).Aggregate(0, (a, b) => a ^ b.GetHashCode());
    }
}