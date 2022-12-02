interface IDay
{
    IEnumerable<string> InputData { get; }

    IEnumerable<string> SampleData { get; }
}

interface IDayPart1<T>
{
    T GetPart1Answer();

    T GetPart1Sample();
}

interface IDayPart2<T>
{
    T GetPart2Answer();

    T GetPart2Sample();
}