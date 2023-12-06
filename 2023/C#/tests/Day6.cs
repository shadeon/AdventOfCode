using Xunit;
using FluentAssertions;

public class Day6Tests
{
    Day6 _sut = new Day6();

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(288);
    }

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(2269432);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(71503);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(35865985);
    } 
}
