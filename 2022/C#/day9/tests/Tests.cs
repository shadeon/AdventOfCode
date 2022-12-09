using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day9 _sut = new Day9();

    [Fact]
    public void Part1Answer_ShouldBe13_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(13);
    }

    [Fact]
    public void Part1Answer_ShouldBe6212_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(6212);
    }

    [Fact]
    public void Part2Answer_ShouldBe1_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(1);
    }

    [Fact]
    public void Part2Answer_ShouldBe36_WhenSecondSampleData()
    {
        var input = new [] {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20"
        };
        var answer = _sut.GetPart2Answer(input);

        answer.Should().Be(36);
    }

    [Fact]
    public void Part2Answer_ShouldBe2522_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(2522);
    }
}
