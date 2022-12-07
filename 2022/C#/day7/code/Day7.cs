public class Day7 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input) =>
        processCommands(input)
            .Values.Where(size => size <= 100000)
            .Sum();
        
    public int GetPart2Answer(IEnumerable<string> input)
    {
        var sizeByDirPath = processCommands(input);
        var unused = 70000000 - sizeByDirPath["/"];
        
        return sizeByDirPath.Values.Where(size => size + unused > 30000000)
            .OrderBy(size => size)
            .First();
    }

    private Dictionary<string, int> processCommands(IEnumerable<string> input)
    {
        var directoryPath = new Stack<string>();
        var sizeByDirPath = new Dictionary<string, int>();

        do
        {
            var line = input.First();
            if (line.StartsWith("$ cd"))
            {
                var newPath = changeDirectory(line.Substring(5), directoryPath);
                input = input.Skip(1);

                if (!sizeByDirPath.ContainsKey(newPath))
                {
                    sizeByDirPath.Add(newPath, 0);
                }

            }
            else if (line.StartsWith("$ ls"))
            {
                var (sizeOfContents, remainingLines) = lsCommand(input.Skip(1));

                foreach (var path in directoryPath)
                {
                    sizeByDirPath[path] = sizeByDirPath[path] + sizeOfContents;
                }
                
                input = remainingLines;
            }

        } while (input.Any());

        return sizeByDirPath;
    }

    private string changeDirectory(string dirName, Stack<string> directoryPath)
    {
        if (dirName == "..")
        {
            directoryPath.Pop();
            return directoryPath.Peek();
        }

        if (dirName == "/")
        {
            directoryPath.Clear();
        }

        var current = directoryPath.TryPeek(out var top) ? $@"{top}/" : string.Empty;
        var newPath = $"{current}{dirName}";
        directoryPath.Push(newPath);

        return newPath;
    }

    public (int size, IEnumerable<string> remainingLines) lsCommand(IEnumerable<string> lines)
    {
        var sizeOfContents = lines.TakeWhile(l => !l.StartsWith("$"))
            .Where(l => !l.StartsWith("dir"))
            .Select(l => int.Parse(l.Split(" ")[0]))
            .Sum();
        
        var remainingLines = lines.SkipWhile(l => !l.StartsWith("$"))
            .ToArray();

        return (sizeOfContents, remainingLines);
    }

}
