using Xunit;
using FluentAssertions;

public class Day4Tests
{
    Day4 _sut = new Day4();

    [Fact]
    public void Part1Answer_ShouldBe2_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(2);
    }

    [Fact]
    public void Part1Answer_ShouldBe498_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(498);
    }

    [Fact]
    public void Part2Answer_ShouldBe4_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(4);
    }

    [Fact]
    public void Part2Answer_ShouldBe859_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(859);
    }

}