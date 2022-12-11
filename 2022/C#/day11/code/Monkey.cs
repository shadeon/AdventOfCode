public readonly record struct ThrowTarget(long item, int Monkey);

public record class Monkey 
{
    private Queue<long> _items = new Queue<long>();

    public string Name => $"Monkey {Number}";

    public int Number { get; init; } = 0;

    public int ThrownItemCount { get; private set; } = 0;

    public IEnumerable<long> Items => _items;

    public Func<long, long> Operation { get; init;} = i => i;

    public Func<bool, int> Test { get; init; } = i => 0;

    public int TestDivisor { get; init; } = 1;

    public Monkey AddItem(long item)
    {
        _items.Enqueue(item);
        return this;
    }

    public ThrowTarget? ThrowNext(Func<long, long> worryManager)
    {
        if (_items.Count == 0)
        {
            return null;
        }

        var next = _items.Dequeue();

        var newWorryLevel = worryManager(Operation(next));

        ThrownItemCount += 1;

        return new ThrowTarget(newWorryLevel, Test(newWorryLevel % TestDivisor == 0));
    }

}
