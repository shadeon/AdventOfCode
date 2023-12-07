using System.Text.RegularExpressions;

public class Day7 : IDay, IDayPart1<int>, IDayPart2<int?>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\day7\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\day7\sample.txt");

    private enum HandType
    {
        FiveOfAKind = 6,
        FourOfAKind = 5,
        FullHouse = 4,
        ThreeOfAKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }

    public int GetPart1Answer(IEnumerable<string> input) =>
        input.Select(l => parseHandLine(l))
            .OrderBy(line => line.hand.Type)
            .ThenBy(h => h.mappedHand)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();

    public int? GetPart2Answer(IEnumerable<string> input) =>
        input.Select(l => parseHandLine(l, true))
            .OrderBy(line => line.hand.Type)
            .ThenBy(h => h.mappedHand)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();

    private (Hand hand, string mappedHand, int Bid) parseHandLine(string line, bool isPart2 = false)
    {
        var items = line.Split(' ');
        var hand = new Hand(items[0], getHand(items[0], isPart2));
        return (hand, replaceNotation(hand.Cards, isPart2), int.Parse(items[1]));
    }
    
    private readonly record struct Hand(string Cards, HandType Type);

    private Regex _replaceLetters = new("A|K|T|J");

    private string replaceNotation(string hand, bool jokersWild = false) =>
        _replaceLetters.Replace(hand, (m) => m.Value switch {
            "A" => "Z",
            "K" => "Y",
            "T" => "A",
            "J" when jokersWild => "0",
            _ => m.Value
        });

    private HandType getHand(string hand, bool jokersWild = false)
    {
        var handToProcess = jokersWild ? hand.Replace("J", "") : hand;

        // handle edge case of 5 jokers
        if (jokersWild && handToProcess.Length == 0)
        {
            return HandType.FiveOfAKind;
        }

        var jokerCount = hand.Length - handToProcess.Length;

        var charCounts = handToProcess.GroupBy(c => c).Select(g => g.Count()).ToArray();

        return charCounts.Length switch {
            1 => HandType.FiveOfAKind,
            2 => charCounts.Contains(4 - jokerCount) ? HandType.FourOfAKind : HandType.FullHouse,
            3 => charCounts.Contains(3 - jokerCount) ? HandType.ThreeOfAKind : HandType.TwoPair,
            4 => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

}
