using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Benchmarks
{
    private static readonly Day15 _sut = new Day15();

    [Benchmark] 
    public void Part1AnswerWithSample() => _sut.GetPart1Answer(_sut.SampleData, 10);

    [Benchmark]
    public void Part1AnswerWithInput() => _sut.GetPart1Answer(_sut.InputData, 2000000);

    [Benchmark]
    public void Part2AnswerWithSample() => _sut.GetPart2Answer(_sut.SampleData, 20);
    
    [Benchmark]
    public void Part2AnswerWithInput() => _sut.GetPart2Answer(_sut.InputData, 4000000);
}