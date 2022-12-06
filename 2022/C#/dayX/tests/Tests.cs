using Xunit;
using FluentAssertions;

public class DayXTests
{
    DayX _sut = new DayX();

    [Fact]
    public void Part1Answer_ShouldBe0_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(0);
    }

/*     
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

    [Fact]
    public void Part2Answer_ShouldBe2193_WhenInputData()
    {
        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Be(2193);
    } 
*/
}
