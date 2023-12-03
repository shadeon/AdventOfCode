
public class Day2 : IDay, IDayPart1<int>, IDayPart2<int?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day2\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day2\sample.txt");

    private Handful _part1Possible = new Handful(12, 13, 14);

    public int GetPart1Answer(IEnumerable<string> input) =>
        input.Select(createGame)
        .Where(game => game.Hands.All(h => h.IsPossible(_part1Possible)))
        .Sum(game => game.Id);
        
    public int? GetPart2Answer(IEnumerable<string> input) =>
        input.Select(line => createGame(line).Hands)
        .Select(hands => hands.Aggregate(getMinimumRequiredCubes))
        .Sum(hand => hand.Red * hand.Blue * hand.Green);

    private readonly record struct Handful(int Red, int Green, int Blue)
    {
        public bool IsPossible(Handful maxCount) => 
            Blue <= maxCount.Blue &&
            Green <= maxCount.Green &&
            Red <= maxCount.Red;
    };

    private readonly record struct Game(int Id, IEnumerable<Handful> Hands);

    private Game createGame(string line)
    {
        var indexOfColon = line.IndexOf(':');
        var id = int.Parse(line[5..indexOfColon]);

        return new Game(id, parseHands(line[(indexOfColon + 2)..]));
    }

    private IEnumerable<Handful> parseHands(string line)
    {
        var hands = line.Split(';', StringSplitOptions.TrimEntries);

        return hands.Select(h => {
            var beads = h.Split(',', StringSplitOptions.TrimEntries);

            return beads.Aggregate(new Handful(), (acc, val) => {
                var space = val.IndexOf(' ');
                var quantity = int.Parse(val[0..space]);
                return val[(space+1)..] switch {
                    "red" => acc with { Red = quantity },
                    "green" => acc with { Green = quantity },
                    "blue" => acc with { Blue = quantity },
                    _ => acc
                };
            });
        });
    }

    private Handful getMinimumRequiredCubes(Handful minimum, Handful draw) =>
        new Handful(
            Math.Max(minimum.Red, draw.Red),
            Math.Max(minimum.Green, draw.Green),
            Math.Max(minimum.Blue, draw.Blue)
        );
}
