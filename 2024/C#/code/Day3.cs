using System.Linq;
using System.Text.RegularExpressions;

public class Day3 : IDayPart1<int>, IDayPart2<int>
{
    private static readonly int _day = 3;

    public int Day { get; } = _day;

    public IEnumerable<string> InputData { get; } = File.ReadLines(@$".\data\day{_day}\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@$".\data\day{_day}\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var regex = new Regex(@"mul\((?<first>\d{1,3}),(?<second>\d{1,3})\)");
        return input.SelectMany(line => regex.Matches(line))
            .Where(match => match.Success)
            .Select(match => int.Parse(match.Groups["first"].Value) * int.Parse(match.Groups["second"].Value))
            .Sum();
    }

    public int GetPart2Answer(IEnumerable<string> input)
    {
        var regex = new Regex(@"do\(\)|don't\(\)|mul\((?<first>\d{1,3}),(?<second>\d{1,3})\)");

        var enabled = true;
        return input.SelectMany(line => regex.Matches(line))
            .Where(match => match.Success)
            .Aggregate(new List<int>(), (result, match) => {
                if (match.Groups["0"].Value.StartsWith("don't"))
                {
                    enabled = false;
                }
                else if (match.Groups["0"].Value.StartsWith("do"))
                {
                    enabled = true;
                }
                else if (enabled)
                {
                    result.Add(int.Parse(match.Groups["first"].Value) * int.Parse(match.Groups["second"].Value));
                }

                return result;
            })
            .Sum();
    }
}
