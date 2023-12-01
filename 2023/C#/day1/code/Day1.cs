using System.Text.RegularExpressions;

public class Day1 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public IEnumerable<string> SamplePart2Data { get; } = File.ReadLines(@".\data\samplePt2.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        getPart1Adjustments(input).Sum(s => int.Parse(s));
        
    public int GetPart2Answer(IEnumerable<string> input) =>
        getPart2Adjustments(input).Sum(s => int.Parse(s));

    private IEnumerable<string> getPart1Adjustments(IEnumerable<string> input) =>
        input.Select(line => 
            Regex.Matches(line, "\\d").Where(m => m.Success).Select(m => m.Value)
        ).Select(line => {
            var first = line.First();
            var second = line.Last();
            return $"{first}{second}";
        }
    );

    private string _part2Regex = @"\d|one|two|three|four|five|six|seven|eight|nine";

    private IEnumerable<string> getPart2Adjustments(IEnumerable<string> input) =>
        input.Select(line => {
            var first = convertToNumber(Regex.Match(line, _part2Regex).Value);
            var second = convertToNumber(Regex.Match(line, _part2Regex, RegexOptions.RightToLeft).Value);
            return $"{first}{second}";
        }
    );

    private string convertToNumber(string digit) => digit switch {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => digit
    };

}
