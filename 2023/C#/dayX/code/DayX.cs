public class DayX : IDay, IDayPart1<int>, IDayPart2<string>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        throw new NotImplementedException();
        
    public string GetPart2Answer(IEnumerable<string> input) =>
        "Unknown";

}
