using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day13 _sut = new Day13();

    [Fact]
    public void Part1Answer_ShouldBe13_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(13);
    }

    [Theory]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]")]
    [InlineData("[[1],[2,3,4]]", "[[1],4]")]
    [InlineData("[[4,4],4,4]", "[[4,4],4,4,4]")]
    [InlineData("[]", "[3]")]
    public void IsCorrectOrder_ShouldBeTrue_WhenLeftIsSmaller(string left, string right)
    {
        var answer = _sut.IsCorrectOrder(left, right);

        answer.Should().BeTrue();
    }

    [Theory]
    [InlineData("[9]", "[[8,7,6]]")]
    [InlineData("[7,7,7,7]", "[7,7,7]")]
    [InlineData("[[[]]]", "[[]]")]
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]")]
    public void IsCorrectOrder_ShouldBeFalse_WhenRightIsSmaller(string left, string right)
    {
        var answer = _sut.IsCorrectOrder(left, right);

        answer.Should().BeFalse();
    }

    [Fact]
    public void Part1Answer_ShouldBe5506_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(5506);
    }

    [Fact]
    public void Part2Answer_ShouldBe140_WhenSampleData()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(140);
    }

    [Fact]
    public void Part2Answer_ShouldBe21756_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(21756);
    } 
}
