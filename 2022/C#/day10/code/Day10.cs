public class Day10 : IDay, IDayPart1<int>, IDayPart2<IEnumerable<string>>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public IEnumerable<int> part1Cycles = new[] { 20, 60, 100, 140, 180, 220 };

    public int GetPart1Answer(IEnumerable<string> input) =>
        GetCycles(input, 220)
            .Where(kvp => part1Cycles.Contains(kvp.Key))
            .Select(kvp => kvp.Key * kvp.Value)
            .Sum();

    private Dictionary<int, int> GetCycles(IEnumerable<string> input, int cycleCount)
    {
        var xRegister = 1;
        var instructions = new Queue<string>(input);
        var processing = new Queue<string>();

        var cycles = Enumerable.Range(1, cycleCount).Aggregate(new Dictionary<int, int>(), (acc, cycle) =>
        {
            // start of cycle
            if (processing.Count < 1)
            {
                var next = instructions.Dequeue();
                if (next.StartsWith("addx"))
                {
                    processing.Enqueue("noop");
                }
                processing.Enqueue(next);
            }

            // record register
            acc.Add(cycle, xRegister);

            // end of cycle
            var instruction = processing.Dequeue();
            xRegister += instruction.StartsWith("addx") ? int.Parse(instruction.Split(" ")[1]) : 0;

            return acc;
        });
        return cycles;
    }

    public IEnumerable<string> GetPart2Answer(IEnumerable<string> input)
    {
        var cycles = GetCycles(input, 240);

        return Enumerable.Range(0, 240)
            .Select(pos => GetPixel(pos, cycles[pos + 1]))
            .Chunk(40)
            .Select(row => string.Join("", row));
    }

    public string GetPixel(int crtPos, int xRegister) => (crtPos % 40) switch {
        var p when p >= xRegister - 1 && p <= xRegister + 1 => "#",
        _ => "."
    };
}
