using Xunit;
using FluentAssertions;

public class Day1Test
{
    private Day1 _sut = new Day1();

    [Fact]
    public void Part1_Sample_ShouldBeCorrect()
    {
        var answer = _sut.GetPart1Sample();
        answer.Should().Be(24000);
    }

    [Fact]
    public void Part1_Input_ShouldBeCorrect()
    {
        var answer = _sut.GetPart1Answer();
        answer.Should().Be(73211);
    }

    [Fact]
    public void Part2_Sample_ShouldBeCorrect()
    {
        var answer = _sut.GetPart2Sample();
        answer.Should().Be(45000);
    }

    [Fact]
    public void Part2_Input_ShouldBeCorrect()
    {
        var answer = _sut.GetPart2Answer();
        answer.Should().Be(213958);
    }
}
