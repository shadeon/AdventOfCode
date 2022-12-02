public class Day2 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    private const string _opponentRock = "A";
    private const string _opponentPaper = "B";
    private const string _opponentScissors = "C";

    private const string _rock = "X";
    private const string _paper = "Y";
    private const string _scissors = "Z";

    private const string _mustLose = "X";
    private const string _mustDraw = "Y";
    private const string _mustWin = "Z";

    public int GetPart1Answer(IEnumerable<string> input) =>
        input.Select(GetRoundScore)
        .Sum();

    public int GetRoundScore(string line)
    {
        var game = line.Split(" ", StringSplitOptions.TrimEntries);
        return GetRoundScore(game[0], game[1]);
    }

    public int GetRoundScore(string opponent, string yours) =>
        GetShapeScore(yours) + GetGameScore(opponent, yours);

    public int GetShapeScore(string shape) => shape switch 
    {
        "X" => 1,
        "Y" => 2,
        "Z" => 3,
        _ => 0
    };

    public int GetGameScore(string opponent, string yours) => (opponent, yours) switch
    {
        (_opponentRock, _rock) => 3,
        (_opponentRock, _paper) => 6,
        (_opponentRock, _scissors) => 0,
        (_opponentPaper, _rock) => 0,
        (_opponentPaper, _paper) => 3,
        (_opponentPaper, _scissors) => 6,
        (_opponentScissors, _rock) => 6,
        (_opponentScissors, _paper) => 0,
        (_opponentScissors, _scissors) => 3,
        _ => -1
    };

    public string GetShapeForResult(string opponent, string outcome) => (opponent, outcome) switch
    {
        ("A", _mustLose) => _scissors,
        ("A", _mustDraw) => _rock,
        ("A", _mustWin) => _paper,        
        ("B", _) => outcome,
        ("C", _mustLose) => _paper,
        ("C", _mustDraw) => _scissors,
        ("C", _mustWin) => _rock,
        _ => "Unknown"
    };

    public int GetPart2Answer(IEnumerable<string> input) => 
        input
        .Select(line => line.Split(" ", StringSplitOptions.TrimEntries))
        .Select(game => GetRoundScore(game[0], GetShapeForResult(game[0], game[1])))
        .Sum();
}