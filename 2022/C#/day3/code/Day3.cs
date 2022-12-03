using System.Text;

public class Day3 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        input.Select(GetBytes)
        .Select(line => GetIncorrectItem(GetFirstCompartment(line), GetSecondCompartment(line)))
        .Select(GetItemPriority)
        .Sum();
    

    public IEnumerable<byte> GetBytes(string line) => 
        Encoding.ASCII.GetBytes(line);

    public IEnumerable<byte> GetFirstCompartment(IEnumerable<byte> rucksack) => 
        rucksack.Take(rucksack.Count() / 2).ToArray();

    public IEnumerable<byte> GetSecondCompartment(IEnumerable<byte> rucksack) =>
        rucksack.Skip(rucksack.Count() / 2).ToArray();

    public int GetIncorrectItem(IEnumerable<byte> first, IEnumerable<byte> second) =>
        Convert.ToInt32(first.First(item => second.Contains(item)));

    public int GetItemPriority(int item) => item switch {
        > 90 => item - 96, // start at 1
        > 65 => item - 38, // start at 27
        _ => 0
    };

    public int GetBadge(IEnumerable<IEnumerable<byte>> group)
    {
        var first = group.First();
        var second = group.Skip(1).First();
        var third = group.Last();

        return first.Intersect(second)
            .Intersect(third)
            .First();
    }

    public int GetPart2Answer(IEnumerable<string> input) =>
        input.Select(line => GetBytes(line).Distinct())
        .Chunk(3)
        .Select(GetBadge)
        .Select(GetItemPriority)
        .Sum();
}
