public class DayX : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        throw new NotImplementedException();
        
    public int GetPart2Answer(IEnumerable<string> input) =>
        throw new NotImplementedException();

}
