using Xunit;
using FluentAssertions;

public class Day5Tests
{
    Day5 _sut = new Day5();

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(35);
    }

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(379811651);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(46);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(27992443);
    } 
}
