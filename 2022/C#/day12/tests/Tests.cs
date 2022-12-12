using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day12 _sut = new Day12();

    [Fact]
    public void Part1Answer_ShouldBe31_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(31);
    }

    [Fact]
    public void Part1Answer_ShouldBe472_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(472);
    }

    [Fact]
    public void Part2Answer_ShouldBe29_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(29);
    }

    [Fact]
    public void Part2Answer_ShouldBe465_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(465);
    } 

    [Fact]
    public void Part2AnswerWithDijkstra_ShouldBe465_WhenInputData()
    {
        var answer = _sut.GetPart2AnswerWithDijkstra(_sut.InputData);

        answer.Should().Be(465);
    } 

    [Fact]
    public void Part2AnswerWithAStar_ShouldBe465_WhenInputData()
    {
        var answer = _sut.GetPart2AnswerWithAStar(_sut.InputData);

        answer.Should().Be(465);
    } 

}
