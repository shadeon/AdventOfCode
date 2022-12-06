interface IDay
{
    IEnumerable<string> InputData { get; }

    IEnumerable<string> SampleData { get; }
}

interface IDayPart1<T>
{
    T GetPart1Answer(IEnumerable<string> input);
}

interface IDayPart2<T>
{
    T GetPart2Answer(IEnumerable<string> input);
}