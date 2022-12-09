public readonly record struct Point(int X, int Y);

public readonly record struct Pair(Point head, Point tail);

public class Day9 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var startRope = new Pair(new Point(0,0), new Point(0,0));
        var instructions = GetInstructions(input);

        var visited = new HashSet<Point>();
        var finalRope = instructions.Aggregate(startRope, (current, instruction) => {
            var newHead = MovePoint(current.head, instruction);
            var newRope = new Pair(newHead, MoveTail(newHead, current.tail)); 
            visited.Add(newRope.tail);
            return newRope;
        });

        return visited.Count();
    }
        
    public int GetPart2Answer(IEnumerable<string> input)
    {
        var instructions = GetInstructions(input);
        var startRope = Enumerable.Repeat(new Point(0,0), 10).ToList();
        var visited = new HashSet<Point>();

        var finalRope = instructions.Aggregate(startRope, (current, instruction) => {
            var newRope = new List<Point>() { MovePoint(current[0], instruction) };
            for (var i = 1; i < current.Count; i++)
            {
                if (newRope[i-1] == current[i])
                {
                    // Skip calculating the rest of the rope as it doesn't move
                    newRope.AddRange(current.Skip(i));
                    break;
                }
                newRope.Add(MoveTail(newRope[i-1], current[i]));
            }
            visited.Add(newRope[newRope.Count -1]);
            return newRope;
        });

        return visited.Count();
    }

    public IEnumerable<(int, int)> GetInstructions(IEnumerable<string> input) => input
        .Aggregate(new List<(int, int)>(), (acc, line) => {
            var split = line.Split(" ");
            acc.AddRange(Enumerable.Repeat(GetInstruction(split[0]), int.Parse(split[1])));
            return acc;
    });

    public (int xDistance, int yDistance) GetInstruction(string direction) => direction switch {
        "U" => (0, 1),
        "D" => (0, -1),
        "L" => (-1, 0),
        "R" => (1, 0),
        _ => throw new ArgumentException("Unknown Direction", nameof(direction))
    };

    public Point MovePoint(Point point, (int X, int Y) distance) => 
        new Point(point.X + distance.X, point.Y + distance.Y);

    public (int X, int Y) GetEndDistance(Point head, Point tail) =>
        (head.X - tail.X, head.Y - tail.Y);

    public Point MoveTail(Point newHead, Point currentTail)
    {
        var distance = GetEndDistance(newHead, currentTail);

        return distance switch {
            (0, 0) => currentTail, // same space
            ((>= -1 and <= 1), (>= -1 and <= 1)) => currentTail, // adjacent
            // single axis or Diagonal
            ((>= -2 and <= 2), (>= -2 and <= 2)) => MovePoint(currentTail, ((int)GetMove(distance.X), (int)GetMove(distance.Y))),
            _ => throw new Exception("Unable to move pair")
        };
    }

    public double GetMove(double distance) =>
        Math.Round(distance / 2, MidpointRounding.AwayFromZero);
}
