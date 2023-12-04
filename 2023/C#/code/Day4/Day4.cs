
public class Day4 : IDay, IDayPart1<double>, IDayPart2<int?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day4\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day4\sample.txt");

    public double GetPart1Answer(IEnumerable<string> input) =>
        input.Select(line =>
        {
            int matches = getNumberOfMatches(line);
            return matches > 0 ? Math.Pow(2, matches - 1) : 0;
        }).Sum();

    private int getNumberOfMatches(string line)
    {
        var numbers = line.Substring(line.IndexOf(':') + 1).Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = getNumbers(numbers.First());
        var scratched = getNumbers(numbers.Last());

        // Could have just used .Intersect, but thought I'd try to be performant for once
        var matches = scratched.Where(winningNumbers.Contains).Count();
        return matches;
    }

    private HashSet<int> getNumbers(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToHashSet();

    public int? GetPart2Answer(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        var cardCount = new Dictionary<int, int>(
            Enumerable.Range(0, lines.Length).Select(i => KeyValuePair.Create(i, 1))
        );
        
        return lines.Select((line, index) => (line, index))
            .Aggregate(cardCount, (count, kvp) => {
                var (line, row) = kvp;
                var matchCount = getNumberOfMatches(line);

                for(int i = 1; i <= matchCount; i++)
                {
                    count[row + i] = count[row + i] + count[row];
                }

                return count;
            })
            .Values.Sum();
    }
}
