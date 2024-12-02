public class Day2 : IDayPart1<int>, IDayPart2<int>
{
    private static readonly int _day = 2;

    public int Day { get; } = _day;

    public IEnumerable<string> InputData { get; } = File.ReadLines(@$".\data\day{_day}\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@$".\data\day{_day}\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var reports = input.Select(line => line.Split(" ").Select(int.Parse));

        return reports.Where(IsPart1ReportSafe).Count();
    }

    public int GetPart2Answer(IEnumerable<string> input)
    {
        var reports = input.Select(line => line.Split(" ").Select(int.Parse));

        return reports.Where(report => IsPart2ReportSafe(report)).Count();
    }

    private bool IsPart1ReportSafe(IEnumerable<int> report)
    {
        var pairs = report.Window().Select(pair => pair.Item2 - pair.Item1);

        var isIncrease = pairs.First() > 0;

        return pairs.All(delta => IsValid(delta, isIncrease));
    }

    private static bool IsValid(int delta, bool isIncrease)
    {
        return (delta, isIncrease) switch
        {
            (0, _) => false,
            ( > 0 and <= 3, true) => true,
            ( < 0 and >= -3, false) => true,
            _ => false
        };
    }

    private bool IsPart2ReportSafe(IEnumerable<int> report)
    {
        var pairs = report.Window().ToList();

        var isIncreasing = pairs[0].Item2 - pairs[0].Item1 > 0;
        var invalidPairIndex = pairs.FindIndex(pair => !IsValid(pair.Item2 - pair.Item1, isIncreasing));

        if (invalidPairIndex == -1) {
            return true;
        }

        var skipIndex = invalidPairIndex + 1;
        while (skipIndex > -1) {
            if (IsPart1ReportSafe(report.Where((level, index) => index != skipIndex))) {
                return true;
            }
            skipIndex -= 1;
        }

        return false;
    }
}
