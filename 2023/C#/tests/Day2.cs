using Xunit;
using FluentAssertions;

public class Day2Tests
{
    Day2 _sut = new Day2();

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(8);
    }

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(2716);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(2286);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(72227);
    } 
}
