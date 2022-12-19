public readonly record struct Valve(string Name, int FlowRate)
{
    public IEnumerable<string> ConnectedValves { get; init; } = Enumerable.Empty<string>();

    public IDictionary<string, int> Distances { get; init; } = new Dictionary<string, int>();
}

public class Day16 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var roundsLeft = 30;
        var startValve = "AA";

        var valves = GetDistances(GetValves(input), startValve);
        var valvesOfInterest = valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name).ToArray();
        var scores = new Dictionary<string, int>();

        VisitValves(scores, startValve, 0, roundsLeft, valvesOfInterest, valves);

        return scores.Values.Max();
    }

    public void VisitValves(Dictionary<string, int> cache, string current, int score, int timeRemaining, IEnumerable<string>valvesOfInterest, IDictionary<string, Valve> valves)
    {
        // update cache with score if it's better than our previous attempt with this set
        var cacheKey = string.Join("", valvesOfInterest.OrderBy(v => v));
        cache[cacheKey] = Math.Max(cache.GetValueOrDefault(cacheKey, 0), score);

        foreach (var valve in valvesOfInterest)
        {
            // time it would take to travel to valve and open it
            var timeAfterAction = timeRemaining - valves[current].Distances[valve] - 1;
            if (timeAfterAction <= 0) 
            {
                // no point acting, would run out of time before it has any impact
                continue;
            }
            VisitValves(cache, valve, score + (valves[valve].FlowRate * timeAfterAction), timeAfterAction, valvesOfInterest.Where(v => v != valve).ToArray(), valves);
        }
    }
        
    public int GetPart2Answer(IEnumerable<string> input)
    {
        var roundsLeft = 26;
        var startValve = "AA";

        var valves = GetDistances(GetValves(input), startValve);
        var valvesOfInterest = valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name).ToArray();
        var scores = new Dictionary<string, int>();

        VisitValves(scores, startValve, 0, roundsLeft, valvesOfInterest, valves);

        // Create paths taken and their scores - note that the scores list contains nodes still closed at the end
        // so we need to convert this back into the opened list.
        var paths = scores.Select(kvp => 
            (path: valvesOfInterest.Except(kvp.Key.Chunk(2).Select(c => string.Join("", c))).ToArray(), score: kvp.Value)
        ).ToArray();

        // find the best scoring pair of disjoint sets
        return paths.Aggregate(0, (score, path1) => 
            paths.Where(p => !p.path.Intersect(path1.path).Any())
                .Aggregate(score, (acc, path2) => Math.Max(acc, path1.score + path2.score))
        );
    }

    public Dictionary<string, Valve> GetValves(IEnumerable<string> input)
    {
        var result = new Dictionary<string, Valve>();
        foreach (var line in input)
        {
            var delim = line.IndexOf(';');
            var name = line.Substring(6, 2);
            var rate = int.Parse(line.Substring(23, delim - 23));

            var valvesStart = line.Substring(delim + 8, 1) == "s" ? delim + 25 : delim + 24;
            var connected = line.Substring(valvesStart).Split(',', StringSplitOptions.TrimEntries);

            result.Add(name, new Valve(name, rate) { ConnectedValves = connected });
        }

        return result;
    }

    public Dictionary<string, Valve> GetDistances(IDictionary<string, Valve> valves, string start)
    {
        // Although we will traverse every room to calculate distances, we only want to store
        // distances between valves that have flow rate, and the start.
        // All closed valves are essentially just travel costs in the graph

        var result = new Dictionary<string, Valve>();
        var valvesOfInterest = valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name).ToArray();

        var active = new PriorityQueue<string, int>();

        foreach (var startValve in valvesOfInterest.Append(start))
        {
            var valve = valves[startValve];
            var distance = new Dictionary<string, int>();
            distance.Add(startValve, 0);

            active.Clear();
            active.Enqueue(startValve, 0);

            while (active.Count > 0)
            {
                var current = active.Dequeue();
                foreach(var next in valves[current].ConnectedValves)
                {
                    var gScore = distance[current] + 1;

                    if (!distance.ContainsKey(next))
                    {
                        distance.Add(next, gScore);
                        active.Enqueue(next, gScore);
                    }
                    else if (gScore < distance[next])
                    {
                        distance[next] = gScore;
                        if (!active.UnorderedItems.Contains((next, gScore)))
                        {
                            active.Enqueue(next, gScore);
                        }
                    }
                }
            }
            
            result.Add(startValve, new Valve(startValve, valve.FlowRate) { ConnectedValves = valve.ConnectedValves, Distances = distance });
        }

        return result;
    }        

}
