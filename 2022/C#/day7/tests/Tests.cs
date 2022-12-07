using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day7 _sut = new Day7();

    [Fact]
    public void Part1Answer_ShouldBe95437_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(95437);
    }

    [Fact]
    public void Part1Answer_ShouldBe1648397_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(1648397);
    }

    [Fact]
    public void Part2Answer_ShouldBe24933642_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(24933642);
    }

    [Fact]
    public void Part2Answer_ShouldBe1815525_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(1815525);
    } 
}
