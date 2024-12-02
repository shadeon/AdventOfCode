public class Day1 : IDayPart1<int>, IDayPart2<int>
{
    public int Day { get; } = 1;

    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day1\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day1\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var (left, right) = GetLists(input);
        return left.Order()
            .Zip(
                right.Order(),
                (l, r) => Math.Abs(l - r))
            .Sum();
    }

    public int GetPart2Answer(IEnumerable<string> input)
    {
        var (left, right) = GetLists(input);
        var counts = right.GroupBy((n) => n)
            .ToDictionary((g) => g.Key, (g) => g.Count());
        return left.Select(n => counts.TryGetValue(n, out var c) ? n * c : 0)
            .Sum();
    }

    private (IEnumerable<int> left, IEnumerable<int> right) GetLists(IEnumerable<string> input) =>
        input.Aggregate((left: new List<int>(), right: new List<int>()), (acc, line) => {
            var pair = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            acc.left.Add(int.Parse(pair[0]));
            acc.right.Add(int.Parse(pair[1]));
            return acc;
    });
}
