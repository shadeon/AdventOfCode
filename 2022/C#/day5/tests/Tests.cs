using Xunit;
using FluentAssertions;
using System.Linq;

public class Day5Tests
{
    Day5 _sut = new Day5();

    [Fact]
    public void Part1Answer_ShouldBeCMZ_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be("CMZ");
    }
    [Fact]
    public void Part1Answer_ShouldBeTBVFVDZPN_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be("TBVFVDZPN");
    }

    [Fact]
    public void Part2Answer_ShouldBeMCD_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be("MCD");
    }

    [Fact]
    public void Part2Answer_ShouldBeVLCWHTDSZ_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be("VLCWHTDSZ");
    }

    [Theory]
    [InlineData("move 1 from 2 to 10", "1", "2", "10")]
    [InlineData("move 13 from 1 to 3", "13", "1", "3")]
    [InlineData("move 2 from 20 to 1", "2", "20", "1")]
    public void Instruction_ShouldMatchCorrectly_WhenGivenMoveLines(
        string line,
        string quantity,
        string from,
        string to
    )
    {
        var groups = _sut.Instruction.Matches(line).First().Groups;
        var numberMatch = groups["number"].Value;
        var fromMatch = groups["from"].Value;
        var toMatch = groups["to"].Value;

        numberMatch.Should().Be(quantity);
        fromMatch.Should().Be(from);
        toMatch.Should().Be(to);
    }
    

/*
*/
}