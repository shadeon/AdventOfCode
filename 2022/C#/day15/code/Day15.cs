public readonly record struct Point(int X, int Y);

public readonly record struct Sensor
{
    public Point Location { get; init; }

    public Point NearestBeacon { get; init; }

    public int ManhattenDistance { get; init; }
}

public readonly record struct Range
{
    public int Max { get; }

    public int Min { get; }

    public bool IsEmpty { get; }

    public Range(int min, int max) : this(min, max, false) { }

    public Range(): this(0, 0, true) { }

    private Range(int min, int max, bool isEmpty) => (Min, Max, IsEmpty) = (min, max, isEmpty);

    public bool isWithin(int index) => !IsEmpty && index >= Min && index <= Max;
}

public class Day15 : IDay, IDayPart1<int>, IDayPart2<long>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input, int fromRow)
    {
        var sensors = GetSensors(input).ToArray();

        // now get the sensors that can impact the target row
        var ofInterest = sensors.Where(s => s.Location.Y - s.ManhattenDistance <= fromRow && s.Location.Y + s.ManhattenDistance >= fromRow);

        // now for each beacon, project their coverage into that row
        var invalidLocations = ofInterest.Aggregate(new List<Point>(), (acc, sensor) => {
            var verticalDistance = Math.Abs(sensor.Location.Y - fromRow);
            var horizontalCoverage = sensor.ManhattenDistance - verticalDistance;
            acc.AddRange(
                Enumerable.Range(sensor.Location.X - horizontalCoverage, horizontalCoverage * 2 + 1)
                    .Select(i => new Point(i, fromRow)));

            return acc;
        })
        .Distinct()
        // remove any known beacons
        .Where(loc => !sensors.Any(s => s.NearestBeacon == loc));

        return invalidLocations.Count();
    }
        
    public long GetPart2Answer(IEnumerable<string> input, int maxIndex)
    {
        var sensors = GetSensors(input).ToArray();

        var column = new Range(0, maxIndex);

        var beacon = Enumerable.Range(0, maxIndex)
            .Select(y => (y, sensors.Select(s => ProjectBeaconIntoRow(s, y, maxIndex))
                                    .Where(r => !r.IsEmpty)
                                    .OrderBy(r => r.Min)
                                    .ThenBy(r => r.Max)))
            .Select(row => new Point(row.Item2.Aggregate(0, (x, range) => range.isWithin(x) ? range.Max + 1 : x), row.y))
            .First(point => point.X <= maxIndex && point.Y <= maxIndex);

        return beacon.X * (long)4000000 + beacon.Y;
    }

    public int GetManhattenDistance(Point source, Point destination) =>
        Math.Abs(destination.X - source.X) + Math.Abs(destination.Y - source.Y);

    public IEnumerable<Sensor> GetSensors(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var split = line.Split(":", StringSplitOptions.TrimEntries);
            var sensorPoint = GetPointFromString(split[0].Substring(10));
            var beaconPoint = GetPointFromString(split[1].Substring(21));

            yield return new Sensor() { 
                Location =  sensorPoint,
                NearestBeacon = beaconPoint,
                ManhattenDistance = GetManhattenDistance(sensorPoint, beaconPoint)
            };
        }
    }

    public Point GetPointFromString(string input)
    {
        var stringPoint = input.Split(",", StringSplitOptions.TrimEntries).Select(s => int.Parse(s.Substring(2)));
        return new Point(stringPoint.First(), stringPoint.Last());
    }

    public Range ProjectBeaconIntoRow(Sensor sensor, int row, int limit)
    {
        var verticalDistance = Math.Abs(sensor.Location.Y - row);
        // Too far away to influence
        if (verticalDistance > sensor.ManhattenDistance)
        {
            return new Range();
        }

        var coverage = sensor.ManhattenDistance - verticalDistance;
        // clamp at the limits of our search range
        return new Range(
            Math.Max(0, sensor.Location.X - coverage),
            Math.Min(limit, sensor.Location.X + coverage)
        );
    }
}
