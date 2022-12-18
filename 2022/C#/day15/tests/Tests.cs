using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day15 _sut = new Day15();

    [Fact]
    public void Part1Answer_ShouldBe26_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData, 10);

        answer.Should().Be(26);
    }

    [Fact]
    public void Part1Answer_ShouldBe4883971_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData, 2000000);

        answer.Should().Be(4883971);
    }

     
    [Fact]
    public void Part2Answer_ShouldBe56000011_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData, 20);

        answer.Should().Be(56000011);
    }

    [Fact]
    public void Part2Answer_ShouldBe12691026767556_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData, 4000000);

        answer.Should().Be(12691026767556);
    } 
}
