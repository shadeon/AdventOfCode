
public class Day5 : IDay, IDayPart1<double>, IDayPart2<double?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day5\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day5\sample.txt");

    public double GetPart1Answer(IEnumerable<string> input)
    {
        var seeds = getSeeds(input.First());
        var maps = getTranslationMaps(input.Skip(1));

        return seeds.Aggregate(double.MaxValue, (lowest, seed) =>
        {
            var location = maps.Aggregate(seed, (current, translations) =>
            {
                // will either be a translation, or the default which will have 0 for mapValue
                var matchedTranslation = translations.FirstOrDefault(t => t.Start <= current && current <= t.End);

                return current + matchedTranslation.mapValue;
            });

            return Math.Min(location, lowest);
        });
    }

    public double? GetPart2Answer(IEnumerable<string> input)
    {
        var seedRanges = input.First()[7..].Split(' ')
            .Select(double.Parse)
            .Chunk(2).Select(pair => (start: pair[0], end: pair[0] + pair[1] - 1));

        var maps = getTranslationMaps(input.Skip(1));
        
        var location = maps.Aggregate(seedRanges, (ranges, translations) => {
            // create new ranges based on translation
            var outstandingRanges = new Queue<(double start, double end)>(ranges);
            var translated = new List<(double start, double end)>();

            while(outstandingRanges.Count > 0)
            {
                var range = outstandingRanges.Dequeue();
                // get next matching translation
                var nextTranslation = translations.FirstOrDefault(t => t.Start <= range.end && range.start <= t.End);

                if (nextTranslation.Start == 0 && nextTranslation.End == 0)
                {
                    // no translation required, add as is
                    translated.Add(range);
                    continue;
                }
                
                // add any portion outside the translation as an outstanding range
                if (range.start < nextTranslation.Start)
                {
                    outstandingRanges.Enqueue((range.start, nextTranslation.Start - 1));
                }

                if (range.end > nextTranslation.End)
                {
                    outstandingRanges.Enqueue((nextTranslation.End + 1, range.end));
                }
                
                // translate the portion within the range
                translated.Add((
                    Math.Max(range.start, nextTranslation.Start) + nextTranslation.mapValue,
                    Math.Min(range.end, nextTranslation.End) + nextTranslation.mapValue
                ));
            }

            return translated;
        })
        .MinBy(r => r.start);

        return location.start;
    }

    private readonly record struct Translation(double Start, double End, double mapValue);

    private IEnumerable<double> getSeeds(string line) => 
        line[7..].Split(' ').Select(double.Parse);
    
    private IEnumerable<IEnumerable<Translation>> getTranslationMaps(IEnumerable<string> input) =>
        input.Aggregate(new List<List<Translation>>(), (acc, line) => {
            // divider, skip
            if (string.IsNullOrWhiteSpace(line))
            {
                return acc;
            }

            // next category
            if (line.EndsWith(':'))
            {
                acc.Add(new List<Translation>());
                return acc;
            }

            // translation range
            var digits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var currentMap = acc.Last();

            var destination = double.Parse(digits[0]);
            var source = double.Parse(digits[1]);
            var range = double.Parse(digits[2]) - 1;
            currentMap.Add(new Translation(source, source + range, destination - source));

            return acc;
        });
}
