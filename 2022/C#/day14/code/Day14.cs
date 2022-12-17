public readonly record struct Point
{
    public Point(int x, int y) => (X, Y) = (x, y);

    public int X { get; }

    public int Y { get; }

    public Point Down() => new Point(X, Y + 1);

    public Point DownLeft() => new Point(X - 1, Y + 1);

    public Point DownRight() => new Point(X + 1, Y + 1);
};

public class Day14 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public Point SandEntrance { get; } = new Point(500, 0);

    public const string Rock = "Rock";

    public const string Sand = "Sand";

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var cave = GetPaths(input)
            .Aggregate(new List<Point>(), (cave, path) => DrawRockPath(cave, path))
            .Distinct()
            .ToDictionary(p => p, _ => Rock);

        var maxDepth = cave.Keys.MaxBy(p => p.Y).Y;
        return FillWithSand(cave, maxDepth, grain => grain.Y < maxDepth);
    }

    private int FillWithSand(Dictionary<Point, string> cave, int maxDepth, Func<Point, bool> predicate)
    {
        var restedGrains = 0;
        var finished = false;
        do
        {
            var newGrain = SandStep(cave, SandEntrance, maxDepth);
            if (predicate(newGrain))
            {
                cave.Add(newGrain, Sand);
                restedGrains++;
            }
            else
            {
                finished = true;
            }
        } while (!finished);

        return restedGrains;
    }

    public IEnumerable<IEnumerable<Point>> GetPaths(IEnumerable<string> input) => input
        .Select(line => 
            line.Split("->", StringSplitOptions.TrimEntries)
                .Select(p => 
                    p.Split(",")
                        .Select(int.Parse))
                .Select(p => new Point(p.First(), p.Last()))
        );
    
    public List<Point> DrawRockPath(List<Point> cave, IEnumerable<Point> path)
    {
        var _ = path.Aggregate((first, second) => {
            if (first.X == second.X)
            {
                cave.AddRange(CreateRocks(i => new Point(first.X, i), first.Y, second.Y));
            }
            else
            {
                cave.AddRange(CreateRocks(i => new Point(i, first.Y), first.X, second.X));
            }
            return second;
        });

        return cave;
    }

    public Point SandStep(IDictionary<Point, string> cave, Point sand, int maxDepth)
    {
        if (sand.Y > maxDepth)
        {
            return sand;
        }

        var down = sand.Down();
        if (!cave.ContainsKey(down))
        {
            return SandStep(cave, down, maxDepth);
        }

        var downLeft = sand.DownLeft();
        if (!cave.ContainsKey(downLeft))
        {
            return SandStep(cave, downLeft, maxDepth);
        }

        var downRight = sand.DownRight();
        if (!cave.ContainsKey(downRight))
        {
            return SandStep(cave, downRight, maxDepth);
        }

        return sand;
    }

    public IEnumerable<Point> CreateRocks(Func<int, Point> selector, int from, int to) =>
        Enumerable.Range(Math.Min(from, to), Math.Abs(from - to) + 1).Select(selector);

    public int GetPart2Answer(IEnumerable<string> input)
    {
        var cave = GetPaths(input)
            .Aggregate(new List<Point>(), (cave, path) => DrawRockPath(cave, path))
            .Distinct()
            .ToDictionary(p => p, _ => Rock);

        var maxDepth = cave.Keys.MaxBy(p => p.Y).Y;
        // Unlike part 1, stop instead when we attempt to fill the entry twice
        return FillWithSand(cave, maxDepth, grain => !cave.ContainsKey(grain));
    }

}
