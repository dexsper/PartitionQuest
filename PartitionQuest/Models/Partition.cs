namespace PartitionQuest.Models;

public class Partition
{
    private readonly List<int> _numbers;

    public IReadOnlyCollection<int> Numbers => _numbers;

    public Partition(IEnumerable<int> numbers)
    {
        _numbers = new List<int>(numbers);
    }

    public override string ToString()
    {
        return string.Join(" + ", _numbers);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Partition other) 
            return false;
        
        if (_numbers.Count != other._numbers.Count)
            return false;

        var sortedThis = new List<int>(_numbers).OrderBy(x => x).ToList();
        var sortedOther = new List<int>(other._numbers).OrderBy(x => x).ToList();

        return sortedThis.SequenceEqual(sortedOther);
    }

    public override int GetHashCode()
    {
        return _numbers.OrderBy(x => x).Aggregate(0, (a, b) => a ^ b.GetHashCode());
    }
}