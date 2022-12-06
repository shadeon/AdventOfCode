using Xunit;
using FluentAssertions;

public class Day6Tests
{
    Day6 _sut = new Day6();

    [Fact]
    public void Part1Answer_ShouldBe7_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(7);
    }

    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void Part1Answer_ShouldBeCorrect_WhenExampleData(string input, int expected)
    {
        var answer = _sut.GetPositionWhereLastCharsUnique(input, 4);

        answer.Should().Be(expected);
    }

    [Fact]
    public void Part1Answer_ShouldBe1343_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(1343);
    }

    [Fact]
    public void Part2Answer_ShouldBe19_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(19);
    }

    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void Part2Answer_ShouldBeCorrect_WhenExampleData(string input, int expected)
    {
        var answer = _sut.GetPositionWhereLastCharsUnique(input, 14);

        answer.Should().Be(expected);
    }

    [Fact]
    public void Part2Answer_ShouldBe2193_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(2193);
    }
}
