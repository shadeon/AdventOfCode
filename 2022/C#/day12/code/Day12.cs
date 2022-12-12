using System.Text;

public readonly record struct Square
{
    public int X { get; init; }

    public int Y { get; init; }

    public int Elevation { get; init; }

    public int gScore { get; init; }

    public int hScore { get; init; }

    public int fScore => gScore + hScore;

    public (int X, int Y)? Parent { get; init; }
}

public class Day12 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var grid = GetGrid(input);
        var start = FindSquareWithLetter(input, "S");
        var end = FindSquareWithLetter(input, "E");

        var shortestPath = AStar(grid, start, end);
        return shortestPath.HasValue ? shortestPath.Value : throw new Exception("Could not find destination!");
    }
        
    public int GetManhattenDistance(int destinationX, int destinationY, int sourceX, int sourceY) =>
        Math.Abs(destinationX - sourceX) + Math.Abs(destinationY - sourceY);

    public IEnumerable<(int x, int y)> GetNeighbouringSquares(int x, int y, Func<int, bool> predicate, int[][] grid)
    {
        var rowMin = Math.Max(y - 1, 0);
        var rowMax = Math.Min(y + 1, grid.Length - 1);
        var colMin = Math.Max(x - 1, 0);
        var colMax = Math.Min(x + 1, grid[y].Length - 1);

        return new (int x, int y)[] { (x, rowMin), (colMin, y), (x, rowMax), (colMax, y) }
            .Where(c => !(c.x == x && c.y == y) &&  predicate(grid[c.y][c.x]));
    }

    public int[][] GetGrid(IEnumerable<string> input) =>
        input.Select(line => 
            Encoding.ASCII.GetBytes(line)
                .Select(Convert.ToInt32)
                .Select(i => i switch {
                    69 => 26, // E
                    83 => 1, // S
                    > 96 => i - 96, // start at 1
                    _ => int.MaxValue

                })
                .ToArray())
        .ToArray();

    public (int x, int y) FindSquareWithLetter(IEnumerable<string> input, string letter)
    {
        var row = input.Select((line, y) => (line, y)).First(row => row.line.Contains(letter));

        return (row.line.IndexOf(letter), row.y);
    }

    public int? AStar(int[][] grid, (int x, int y) start, (int x, int y) goal, int? fScoreLimit = null)
    {
        // openSet with fScore as priority
        var active = new PriorityQueue<Square, int>();
        active.Enqueue(new Square() {
            Elevation = 1,
            gScore = 0,
            hScore = GetManhattenDistance(goal.x, goal.y, start.x, start.y),
            Parent = null,
            X = start.x,
            Y = start.y
        }, 0);

        // gScores contains the cost of cheapest path from start to co-ord
        var gScores = new Dictionary<(int x, int y), int>();
        gScores.Add(start, 0);

        while (active.Count > 0)
        {
            var current = active.Dequeue();
            if ((current.X, current.Y) == goal)
            {
                return current.fScore;
            }

            if (fScoreLimit.HasValue && current.fScore > fScoreLimit.Value)
            {
                return null;
            }

            foreach (var next in GetNeighbouringSquares(current.X, current.Y, (e) => e <= current.Elevation + 1, grid))
            {
                var nextSquare = new Square() {
                    Elevation = grid[next.y][next.x],
                    gScore = gScores[(current.X, current.Y)] + 1,
                    hScore = GetManhattenDistance(goal.x, goal.y, next.x, next.y),
                    Parent = (current.X, current.Y),
                    X = next.x,
                    Y = next.y
                };

                if (!gScores.ContainsKey((next.x, next.y)))
                {
                    gScores.Add(next, nextSquare.gScore);
                    active.Enqueue(nextSquare, nextSquare.fScore);
                }
                else if (nextSquare.gScore < gScores[next])
                {
                    // this path to neighbour is better than any other, so update
                    gScores[next] = nextSquare.gScore;
                    if (!active.UnorderedItems.Contains((nextSquare, nextSquare.fScore)))
                    {
                        active.Enqueue(nextSquare, nextSquare.fScore);
                    }
                }
            }
        }

        return null;
    }

    public int GetPart2Answer(IEnumerable<string> input) => GetPart2AnswerWithDijkstra(input);

    public int GetPart2AnswerWithAStar(IEnumerable<string> input)
    {
        var grid = GetGrid(input);
        var end = FindSquareWithLetter(input, "E");

        var starts = grid
            .SelectMany((row, y) => row
                .Select((int value, int x) => (x, y, value))
                .Where(sq => sq.value == 1))
            .Select(sq => (sq.x, sq.y, md: GetManhattenDistance(end.x, end.y, sq.x, sq.y)))
            .OrderBy(sq => sq.md)
            .ToArray();
        
        var first = starts.First();

        var shortest = starts.Skip(1)
            .Aggregate(AStar(grid, (first.x, first.y), end), (currentBest, start) => {
                if (currentBest.HasValue && start.md > currentBest.Value)
                {
                    return currentBest;
                }

                var result = AStar(grid, (start.x, start.y), end, currentBest);
                return result.HasValue && (!currentBest.HasValue || result < currentBest) ? result : currentBest;
            });

        return shortest.HasValue ? shortest.Value : throw new Exception("Could not find destination!");
    }

    public int GetPart2AnswerWithDijkstra(IEnumerable<string> input)
    {
        var grid = GetGrid(input);
        var end = FindSquareWithLetter(input, "E");

        var distances = Dijkstra(grid, end);

        return distances.Where(kvp => grid[kvp.Key.y][kvp.Key.x] == 1)
            .Select(kvp => kvp.Value)
            .OrderBy(d => d)
            .First();
    }

    public Dictionary<(int x, int y), int> Dijkstra(int[][] grid, (int x, int y) start)
    {
        var distance = new Dictionary<(int x, int y), int>();
        distance.Add(start, 0);

        var active = new PriorityQueue<Square, int>();
        active.Enqueue(new Square() {
            Elevation = grid[start.y][start.x],
            gScore = 0,
            hScore = 0,
            Parent = null,
            X = start.x,
            Y = start.y
        }, 0);

        while (active.Count > 0)
        {
            var current = active.Dequeue();
            foreach (var next in GetNeighbouringSquares(current.X, current.Y, (e) => current.Elevation - 1 <= e, grid))
            {
                var nextSquare = new Square() {
                    Elevation = grid[next.y][next.x],
                    gScore = distance[(current.X, current.Y)] + 1,
                    hScore = 0, // not direction driven
                    Parent = (current.X, current.Y),
                    X = next.x,
                    Y = next.y
                };

                if (!distance.ContainsKey((next.x, next.y)))
                {
                    distance.Add((next.x, next.y), nextSquare.gScore);
                    active.Enqueue(nextSquare, nextSquare.gScore);
                }
                else if (nextSquare.gScore < distance[(next.x, next.y)])
                {
                    distance[(next.x, next.y)] = nextSquare.gScore;
                    if (!active.UnorderedItems.Contains((nextSquare, nextSquare.gScore)))
                    {
                        active.Enqueue(nextSquare, nextSquare.gScore);
                    }
                }
            }
        }

        return distance;
    }

}
