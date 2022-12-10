using Xunit;
using FluentAssertions;

public class DayXTests
{
    Day10 _sut = new Day10();

    [Fact]
    public void Part1Answer_ShouldBe13140_WhenSampleData()
    {
        var answer = _sut.GetPart1Answer(_sut.SampleData);

        answer.Should().Be(13140);
    }

    [Fact]
    public void Part1Answer_ShouldBe17180_WhenInputData()
    {
        var answer = _sut.GetPart1Answer(_sut.InputData);

        answer.Should().Be(17180);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenSampleData()
    {
        var expected = new [] {
            "##..##..##..##..##..##..##..##..##..##..",
            "###...###...###...###...###...###...###.",
            "####....####....####....####....####....",
            "#####.....#####.....#####.....#####.....",
            "######......######......######......####",
            "#######.......#######.......#######....."
        };

        var answer = _sut.GetPart2Answer(_sut.SampleData);

        answer.Should().Equal(expected);
    }

    [Fact]
    public void Part2Answer_ShouldBeCorrect_WhenInputData()
    {
        var expected = new [] {
            "###..####.#..#.###..###..#....#..#.###..",
            "#..#.#....#..#.#..#.#..#.#....#..#.#..#.",
            "#..#.###..####.#..#.#..#.#....#..#.###..",
            "###..#....#..#.###..###..#....#..#.#..#.",
            "#.#..#....#..#.#....#.#..#....#..#.#..#.",
            "#..#.####.#..#.#....#..#.####..##..###.."
        };

        var answer = _sut.GetPart2Answer(_sut.InputData);

        answer.Should().Equal(expected);
    } 
}
