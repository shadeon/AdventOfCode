using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day11 _sut = new Day11();

    [Fact]
    public void Part1Answer_ShouldBe10605_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(10605);
    }
  
    [Fact]
    public void Part1Answer_ShouldBe54752_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(54752);
    }

    [Fact]
    public void Part2Answer_ShouldBe2713310158_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be((long)2713310158);
    }

    [Fact]
    public void Part2Answer_ShouldBe13606755504_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be((long)13606755504);
    } 
}
