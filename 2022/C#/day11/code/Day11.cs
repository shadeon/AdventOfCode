public class Day11 : IDay, IDayPart1<int>, IDayPart2<long>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        AfterRounds(20, GetMonkeys(input), item => (int)Math.Floor((double)item/3))
            .OrderByDescending(m => m.ThrownItemCount)
            .Take(2)
            .Aggregate((int)1, (acc, m) => acc * m.ThrownItemCount);
    
    public IEnumerable<Monkey> GetMonkeys(IEnumerable<string> input) => input
        .Chunk(7)
        .Select(chunk => {
            var monkeyLines = chunk.ToArray();
            var test = int.Parse(monkeyLines[3].Split("by ")[1]);
            var targetIfTrue = int.Parse(monkeyLines[4].Split("monkey ")[1]);
            var targetIfFalse = int.Parse(monkeyLines[5].Split("monkey ")[1]);

            Func<long, long> operation = monkeyLines[2].Split("= ")[1] switch {
                "old * old" => item => item * item,
                var s when s.StartsWith("old ") => ParseOp(s.Substring(4).Split(" ")) switch {
                    ("*", var by) => item => item * by,
                    ("+", var by) => item => item + by,
                    _ => throw new Exception("Unknown operator")
                },
                _ => throw new Exception("Unknown operation")
            };

            var monkey = new Monkey() {
                Number = int.Parse(monkeyLines[0].Substring(7).Trim(':')),
                Test = result => result ? targetIfTrue : targetIfFalse,
                TestDivisor = test,
                Operation = operation
            };

            var startingItems = monkeyLines[1]
                .Substring(18)
                .Split(",", StringSplitOptions.TrimEntries)
                .Select(int.Parse);

            foreach (var item in startingItems)
            {
                monkey.AddItem(item);
            }

            return monkey;
        });

    public IEnumerable<Monkey> AfterRounds(int rounds, IEnumerable<Monkey> monkeys, Func<long, long> worryManager)
    {
        var monkeyArray = monkeys.ToArray();

        foreach (var round in Enumerable.Range(1, rounds))
        {
            foreach (var monkey in monkeyArray)
            {
                while (monkey.Items.Any())
                {
                    var target = monkey.ThrowNext(worryManager);
                    if (target.HasValue)
                    {
                        monkeyArray[target.Value.Monkey].AddItem(target.Value.item);
                    }
                }
            }
        }

        return monkeyArray;
    }

    public (string op, int by) ParseOp(IEnumerable<string> split) => 
        (split.First(), int.Parse(split.Last()));

    public long GetPart2Answer(IEnumerable<string> input)
    {
        var monkeys = GetMonkeys(input);
        long lcm = monkeys.Aggregate(1, (acc, monkey) => acc * monkey.TestDivisor);
        var finalState = AfterRounds(10000, GetMonkeys(input),  item => item % lcm);

        return finalState.OrderByDescending(m => m.ThrownItemCount)
            .Take(2)
            .Aggregate((long)1, (acc, m) => acc * m.ThrownItemCount);
    }

}
