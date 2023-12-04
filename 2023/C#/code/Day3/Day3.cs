
using System.Data;
using System.Text.RegularExpressions;

public class Day3 : IDay, IDayPart1<int>, IDayPart2<int?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day3\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day3\sample.txt");

    private Regex _numbers = new Regex(@"\d+");

    private Regex _symbols = new Regex(@"[^\d.]");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var schematic = input.ToArray();
        return getPartNumbers(schematic).Sum();
    }

    private IEnumerable<int> getPartNumbers(string[] schematic) =>
        schematic
            .SelectMany((line, row) => {
                var adjacentSymbols = 
                    _symbols.Matches(schematic[row])
                    // process above if not first row
                    .Concat(row > 0 ? _symbols.Matches(schematic[row - 1]) : Enumerable.Empty<Match>())
                    // process below if not final row
                    .Concat(row < schematic.Length - 1 ? _symbols.Matches(schematic[row + 1]) : Enumerable.Empty<Match>())
                    .Select(m => m.Index)
                    .ToArray();

                return _numbers.Matches(line)
                    .Where(match => adjacentSymbols.Any(index => index >= match.Index - 1 && index <= match.Index + match.Length ))
                    .Select(match => int.Parse(match.Value));
            });
        
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
