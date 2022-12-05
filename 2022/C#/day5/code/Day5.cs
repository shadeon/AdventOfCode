using System.Text.RegularExpressions;

public readonly record struct Instruction(int Quantity, int From, int To);

public class Day5 : IDay, IDayPart1<string>, IDayPart2<string>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public string GetPart1Answer(IEnumerable<string> input)
    {
        var stacks = GetStacks(input);
        var instructions = GetInstructions(input);
        
        foreach (var instruction in instructions)
        {
            for (int i = 1; i <= instruction.Quantity; i++)
            {
                stacks[instruction.To - 1].Push(stacks[instruction.From - 1].Pop());
            }
        }

        return stacks.Aggregate("", (output, stack) => $"{output}{stack.Peek()}");
    }

    public string GetPart2Answer(IEnumerable<string> input)
    {
        var stacks = GetStacks(input);
        var instructions = GetInstructions(input);

        foreach (var instruction in instructions)
        {
            var inTransit = new List<string>();
            for (int i = 1; i <= instruction.Quantity; i++ )
            {
                inTransit.Add(stacks[instruction.From - 1].Pop());
            }

            foreach(var crate in inTransit.Reverse<string>())
            {
                stacks[instruction.To - 1].Push(crate);
            }
        }

        return stacks.Aggregate("", (output, stack) => $"{output}{stack.Peek()}");
    }

    public List<Stack<string>> GetStacks(IEnumerable<string> input) => input
        .TakeWhile(line => !line.StartsWith(" 1"))
        .Reverse()
        .Aggregate(new List<Stack<string>>(), (s, line) => {
            if (line.StartsWith(" 1")) {
                return s;
            }
            var crates = line.Chunk(4).Select(chunk => chunk.Skip(1).First()).ToArray();

            for(int i = 0; i < crates.Length; i++)
            {
                if (i > s.Count - 1)
                {
                    s.Add(new Stack<string>());
                }

                if (crates[i] != ' ')
                {
                    s[i].Push($"{crates[i]}");
                }
            }
            return s;
        });

    private IEnumerable<Instruction> GetInstructions(IEnumerable<string> input) => input
        .SkipWhile(line => !line.StartsWith("move"))
        .Select(GetInstruction);

    public Regex Instruction { get; } = new Regex(@"move (?<number>\d+) from (?<from>\d+) to (?<to>\d+)");

    public Instruction GetInstruction(string line)
    {
        var groups = Instruction.Matches(line).First().Groups;
        Func<string, int> getValue = (name) => int.Parse(groups[name].Value);
        return new Instruction(getValue("number"), getValue("from"), getValue("to"));
    }

}