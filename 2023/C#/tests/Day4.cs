using Xunit;
using FluentAssertions;

public class Day4Tests
{
    Day4 _sut = new Day4();

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(13);
    }

    [Fact]
    public void Part1Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(27845);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(30);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(9496801);
    } 
}
