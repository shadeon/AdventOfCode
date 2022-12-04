public readonly record struct Assignment(int start, int end, int length);

public readonly record struct Pair(Assignment first, Assignment second);

public class Day4 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        input.Select(GetPair)
        .Where(IsFullyContainedPair)
        .Count();

    public Pair GetPair(string line)
    {
        var split = line.Split(",", StringSplitOptions.TrimEntries);
        return new Pair(GetAssignment(split[0]), GetAssignment(split[1]));
    }

    public Assignment GetAssignment(string assignment)
    {
        var split = assignment.Split("-", StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray();

        return new Assignment(split[0], split[1], split[1] - split[0]);
    }

    public bool IsFullyContainedPair(Pair pair) => pair switch
    {
        var p when p.first.length <= p.second.length => IsFullyContainedBy(pair.first, pair.second),
        _ => IsFullyContainedBy(pair.second, pair.first)
    };

    public bool IsFullyContainedBy(Assignment smaller, Assignment larger) =>
        smaller.start >= larger.start && smaller.end <= larger.end;

    public bool IsOverlappingPair(Pair pair) => 
        IsBetween(pair.first.start, pair.second.start, pair.second.end) ||
        IsBetween(pair.second.start, pair.first.start, pair.first.end);

    public bool IsBetween(int point, int start, int end) =>
        point >= start && point <= end;

    public int GetPart2Answer(IEnumerable<string> input) =>
        input.Select(GetPair)
        .Where(IsOverlappingPair)
        .Count();
}