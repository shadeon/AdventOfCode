public class Day6 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        GetPositionWhereLastCharsUnique(input.First(), 4);

    public int GetPositionWhereLastCharsUnique(string input, int charCount) =>
        input
            .Skip(charCount - 1)
            .Select((c, i) => (position: i + charCount, window: input.Substring(i, charCount)))
            .First(t => t.window.Distinct().Count() == charCount)
            .position;
        
    public int GetPart2Answer(IEnumerable<string> input) =>
        GetPositionWhereLastCharsUnique(input.First(), 14);

}
