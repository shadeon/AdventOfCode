
public class Day6 : IDay, IDayPart1<double>, IDayPart2<double?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day6\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day6\sample.txt");

    private readonly record struct Race(double Time, double Distance);

    public double GetPart1Answer(IEnumerable<string> input) => 
        getRaces(input)
            .Select(getRaceStrategies)
            .Aggregate((total, val) => total * val);

    private double getRaceStrategies(Race race)
    {
        var caseCount = race.Time - 1;
        var range = Math.Ceiling(caseCount / 2);

        double recordTimes = 0;
        for (double i = range; i * (race.Time - i) > race.Distance; i--)
        {
            recordTimes++;   
        }
        return recordTimes * 2 - caseCount % 2;
    }

    public double? GetPart2Answer(IEnumerable<string> input) => 
        getRaceStrategies(getTheGreatRace(input));

    private Race getTheGreatRace(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        var time = lines[0][11..].Replace(" ", "");
        var distance = lines[1][11..].Replace(" ", "");

        return new Race(double.Parse(time), double.Parse(distance));
    }

    private IEnumerable<Race> getRaces(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        var times = lines[0].ParseNumbers<int>(' ', 11);
        var distances = lines[1].ParseNumbers<int>(' ', 11);

        return times.Zip(distances).Select(t => new Race(t.First, t.Second));
    }
}
