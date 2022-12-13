using System.Text.RegularExpressions;

public class Day13 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public Regex IntegerRegex { get; } = new Regex(@"^(?<digit>\d+),?(?<rest>.*)");
    
    public Regex IsInteger { get; } = new Regex(@"^(\d+)");

    public int GetPart1Answer(IEnumerable<string> input) => input
        .Chunk(3)
        .Select((c, pairIndex) => (index: pairIndex + 1, pair: c.Take(2)))
        .Where(pair => IsCorrectOrder(pair.pair.First(), pair.pair.Last()))
        .Select(pair => pair.index)
        .Sum();
        
    public int GetPart2Answer(IEnumerable<string> input)
    {
        var customIComparer = Comparer<string>.Create((first, second) => IsCorrectOrder(first, second) ? -1 : 1);
        var dividerPackets = new [] { "[[2]]", "[[6]]"};
    
        return input
            .Where(line => line.Length > 1)
            .Concat(dividerPackets)
            .OrderBy(line => line, customIComparer)
            .Select((line, index) => (packet: line, index: index + 1))
            .Aggregate(1, (result, line) => dividerPackets.Contains(line.packet) ? result * line.index : result);
    }   

    public bool IsCorrectOrder(string left, string right) => 
        (left.Substring(0,1), right.Substring(0,1)) switch {
            ("[", "[") => IsCorrectOrder(left.Substring(1), right.Substring(1)),
            ("]", "]") => IsCorrectOrder(left.Substring(1).TrimStart(','), right.Substring(1).TrimStart(',')),
            ("]", _) => true,
            (_, "]") => false,
            (_, "[") when IsInteger.IsMatch(left) => IsCorrectOrder(IsInteger.Replace(left, "[$1]"), right),
            ("[", _) when IsInteger.IsMatch(right) => IsCorrectOrder(left, IsInteger.Replace(right, "[$1]")),
            (_, _) when IsInteger.IsMatch(left) && IsInteger.IsMatch(right) => 
                IsCorrectOrder(GetInteger(left), GetInteger(right)),
            _ => throw new Exception($"Case not handled for {left} and {right}")
    };

    public bool IsCorrectOrder(int left, int right) => left < right;

    public bool IsCorrectOrder((int integer, string remaining) left, (int integer, string remaining) right) =>
        left.integer == right.integer
            ? IsCorrectOrder(left.remaining, right.remaining)
            : IsCorrectOrder(left.integer, right.integer);

    public (int integer, string remaining) GetInteger(string input)
    {
        var matches = IntegerRegex.Match(input);

        return (int.Parse(matches.Groups["digit"].Value), matches.Groups["rest"].Value);
    }

}
