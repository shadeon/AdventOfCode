
using System.Text.RegularExpressions;

public class Day3 : IDay, IDayPart1<int>, IDayPart2<int?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day3\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day3\sample.txt");

    private Regex _numbers = new Regex(@"\d+");

    private Regex _symbols = new Regex(@"[^\d.]");

    private readonly record struct PartNumber(int Row, int Index, int Length, string Value);

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var schematic = input.ToArray();
        return getPartNumbers(schematic).Sum(p => int.Parse(p.Value));
    }

    private IEnumerable<PartNumber> getPartNumbers(string[] schematic) =>
        schematic
            .SelectMany((line, row) => _numbers.Matches(line).Select(match => new PartNumber(row, match.Index, match.Length, match.Value)))
            .Where(candidate =>
                // either side first
                containsSymbol(schematic[candidate.Row], candidate) ||
                // above, if possible
                (candidate.Row > 0 && containsSymbol(schematic[candidate.Row - 1], candidate)) ||
                // below, if possible
                (candidate.Row < schematic.Length - 1 && containsSymbol(schematic[candidate.Row + 1], candidate))
            );

    private bool containsSymbol(string line, PartNumber partNumber) =>
        _symbols.IsMatch(
            line.Substring(
                Math.Max(partNumber.Index - 1, 0),
                partNumber.Length + Math.Min(1, partNumber.Index) + Math.Min(1, line.Length - partNumber.Length - partNumber.Index)
            )
        );
        
    public int? GetPart2Answer(IEnumerable<string> input)
    {
        var schematic = input.ToArray();
        return getGearRatios(schematic).Sum();
    }

    private IEnumerable<int> getGearRatios(string[] schematic)
    {
        var gearRegex = new Regex(@"\*");
        return schematic.SelectMany((line, row) =>
            // find all potential gears in each line
            gearRegex.Matches(line)
                .Select(gear =>
                    // process numbers either side of gear
                    getAdjacentPartNumbers(schematic[row], gear.Index)
                        .Concat(
                            // process above if not in first row
                            row > 0 ? getAdjacentPartNumbers(schematic[row - 1], gear.Index) : Enumerable.Empty<int>()
                        )
                        .Concat(
                            // process below if not in last row
                            row < schematic.Length - 1 ? getAdjacentPartNumbers(schematic[row + 1], gear.Index) : Enumerable.Empty<int>()
                        )
                        .ToArray()
                )
                .Where(adjacentNumbers => adjacentNumbers.Length == 2)
                .Select(adjacentNumbers => adjacentNumbers.Aggregate((f, s) => f * s))
        );
    }

    private IEnumerable<int> getAdjacentPartNumbers(string line, int gearIndex) =>
        _numbers.Matches(line)
            .Where(match => match.Index <= gearIndex + 1 && (match.Index + match.Length - 1) >= gearIndex - 1)
            .Select(match => int.Parse(match.Value));
    
}
