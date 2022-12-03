using Xunit;
using FluentAssertions;

namespace tests;

public class Day3Tests
{
    private Day3 _sut = new Day3();

    [Fact]
    public void GetPart1Answer_ShouldReturn157_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(157);
    }

    [Fact]
    public void GetPart1Answer_ShouldReturn8515_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(8515);
    }

    [Fact]
    public void GetPart2Answer_ShouldReturn70_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(70);
    }

    [Fact]
    public void GetPart2Answer_ShouldReturn2434_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(2434);
    }
}
