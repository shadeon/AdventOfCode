using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day16 _sut = new Day16();

    [Fact]
    public void Part1Answer_ShouldBe1651_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(1651);
    }

    [Fact]
    public void Part1Answer_ShouldBe1906_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(1906);
    }

    [Fact]
    public void Part2Answer_ShouldBe1707_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(1707);
    }   

    [Fact]
    public void Part2Answer_ShouldBe2548_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(2548);
    } 

}
