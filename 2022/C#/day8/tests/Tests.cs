using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day8 _sut = new Day8();

    [Fact]
    public void Part1Answer_ShouldBe21_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(21);
    }

    [Fact]
    public void Part1Answer_ShouldBe1835_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(1835);
    }

    [Fact]
    public void Part2Answer_ShouldBe8_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(8);
    }

    [Fact]
    public void Part2Answer_ShouldBe263670_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(263670);
    } 
}
