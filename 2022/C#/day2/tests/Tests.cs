using Xunit;
using FluentAssertions;

public class Day2Tests
{
    private readonly Day2 _sut = new Day2();

    [Fact]
    public void SampleData_ShouldNotBeEmpty()
    {
        var sampleData = _sut.SampleData;

        sampleData.Should().NotBeEmpty();
    }

    [Fact]
    public void InputData_ShouldNotBeEmpty()
    {
        var inputData = _sut.InputData;

        inputData.Should().NotBeEmpty();
    }

    [Fact]
    public void GetShapeScore_ShouldReturn1_WhenShapeIsX()
    {
        var score = _sut.GetShapeScore("X");

        score.Should().Be(1);
    }

    [Fact]
    public void GetShapeScore_ShouldReturn2_WhenShapeIsY()
    {
        var score = _sut.GetShapeScore("Y");

        score.Should().Be(2);
    }

    [Fact]
    public void GetShapeScore_ShouldReturn1_WhenShapeIsZ()
    {
        var score = _sut.GetShapeScore("Z");

        score.Should().Be(3);
    }

    [Theory]
    [InlineData("A", "X")]
    [InlineData("B", "Y")]
    [InlineData("C", "Z")]
    public void GetGameScore_ShouldBe3_WhenGameIsDraw(string opponent, string yours)
    {
        var score = _sut.GetGameScore(opponent, yours);

        score.Should().Be(3);
    }

    [Theory]
    [InlineData("A", "Z")]
    [InlineData("B", "X")]
    [InlineData("C", "Y")]
    public void GetGameScore_ShouldBe0_WhenGameIsLoss(string opponent, string yours)
    {
        var score = _sut.GetGameScore(opponent, yours);

        score.Should().Be(0);
    }

    [Theory]
    [InlineData("A", "Y")]
    [InlineData("B", "Z")]
    [InlineData("C", "X")]
    public void GetGameScore_ShouldBe6_WhenGameIsWon(string opponent, string yours)
    {
        var score = _sut.GetGameScore(opponent, yours);

        score.Should().Be(6);
    }

    [Fact]
    public void GetPart1Answer_ShouldReturn15_ForSample()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(15);
    }

    [Fact]
    public void Day2_GetPart1Answer_ShouldReturn8392_ForSample()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(8392);
    }

    [Theory]
    [InlineData("X", "Z")]
    [InlineData("Y", "X")]
    [InlineData("Z", "Y")]
    public void GetShapeForResult_ShouldBeCorrect_WhenOpponentPlaysRock(string outcome, string expected)
    {
        var score = _sut.GetShapeForResult("A", outcome);

        score.Should().Be(expected);
    }

    [Theory]
    [InlineData("X", "X")]
    [InlineData("Y", "Y")]
    [InlineData("Z", "Z")]
    public void GetShapeForResult_ShouldBeCorrect_WhenOpponentPlaysPaper(string outcome, string expected)
    {
        var score = _sut.GetShapeForResult("B", outcome);

        score.Should().Be(expected);
    }

    [Theory]
    [InlineData("X", "Y")]
    [InlineData("Y", "Z")]
    [InlineData("Z", "X")]
    public void GetShapeForResult_ShouldBeCorrect_WhenOpponentPlaysScissors(string outcome, string expected)
    {
        var score = _sut.GetShapeForResult("C", outcome);

        score.Should().Be(expected);
    }

    [Fact]
    public void GetPart2Answer_ShouldReturn12_ForSample()
    {
        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Be(12);
    }

    [Fact]
    public void GetPart2Answer_ShouldReturn10116_ForInput()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(10116);
    }
}