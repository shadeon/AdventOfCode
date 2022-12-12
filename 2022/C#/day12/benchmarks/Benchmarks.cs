using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Benchmarks
{
    private static readonly Day12 _sut = new Day12();

    [Benchmark] 
    public void Part1AnswerWithAStar() => _sut.GetPart1Answer(_sut.InputData);

    [Benchmark]
    public void Part2AnswerWithDijkstra() => _sut.GetPart2AnswerWithDijkstra(_sut.InputData);

    [Benchmark]
    public void Part2AnswerWithAStar() => _sut.GetPart2AnswerWithAStar(_sut.InputData);
}