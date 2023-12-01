using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day1 _sut = new Day1();

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(142);
    }

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(54990);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SamplePart2Data);

        answer.Should().Be(281);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(54473);
    } 
}
