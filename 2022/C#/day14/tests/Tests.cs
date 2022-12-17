using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day14 _sut = new Day14();

    [Fact]
    public void Part1Answer_ShouldBe24_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(24);
    }

    [Fact]
    public void Part1Answer_ShouldBe961_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(961);
    }

    [Fact]
    public void Part2Answer_ShouldBe93_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(93);
    }
 
    [Fact]
    public void Part2Answer_ShouldBe26375_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(26375);
    } 

}
