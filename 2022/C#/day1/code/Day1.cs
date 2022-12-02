public class Day1 : IDay, IDayPart1<int>, IDayPart2<int>
{
    private readonly IEnumerable<string> _input = File.ReadLines(@".\data\input.txt");
    private readonly IEnumerable<string> _sample = File.ReadLines(@".\data\sample.txt");

    public IEnumerable<string> InputData => _input;

    public IEnumerable<string> SampleData => _sample;

    public int GetPart1Answer() => part1(_input);

    public int GetPart1Sample() => part1(_sample);

    private int part1(IEnumerable<string> input) => 
        getElves(input)
        .Max();

    public int GetPart2Sample() => part2(_sample);

    public int GetPart2Answer() => part2(_input);

    private int part2(IEnumerable<string> input) =>
        getElves(input)
        .OrderByDescending(elf => elf)
        .Take(3)
        .Sum();

    public IEnumerable<int> getElves(IEnumerable<string> input) => input
        .Aggregate(newStack<List<int>>(), (acc, line) => {
            if (int.TryParse(line, out var foodItem)) {
                acc.Peek().Add(foodItem);
            }
            else {
                acc.Push(new List<int>());
            }
            return acc;
        })
        .Select(elf => elf.Sum());

    private Stack<T> newStack<T>() where T : new() => 
        new Stack<T>(new [] {new T()});
}
