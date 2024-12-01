interface IDay
{
    int Day { get; }

    IEnumerable<string> InputData { get; }

    IEnumerable<string> SampleData { get; }
}

interface IDayPart1<T> : IDay
{
    T GetPart1Answer(IEnumerable<string> input);
}

interface IDayPart2<T> : IDay
{
    T GetPart2Answer(IEnumerable<string> input);
}