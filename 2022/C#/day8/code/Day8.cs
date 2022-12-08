public class Day8 : IDay, IDayPart1<int>, IDayPart2<int>
{
    public IEnumerable<string> InputData { get; } = File.ReadLines(@".\data\input.txt");

    public IEnumerable<string> SampleData { get; } = File.ReadLines(@".\data\sample.txt");

    public int GetPart1Answer(IEnumerable<string> input)
    {
        var grid = GetGrid(input);
        return grid
            .SelectMany((row, rIndex) => 
                row.Select((height, cIndex) => IsVisible(height, rIndex, cIndex, grid)))
            .Count(t => t);
    }
        
    public int GetPart2Answer(IEnumerable<string> input)
    {
        var grid = GetGrid(input);
        
        return grid
            .SelectMany((row, rIndex) => 
                row.Select((tree, cIndex) => 
                    GetScenicScore(row, cIndex) * GetScenicScore(grid.Select(r => r[cIndex]).ToList(), rIndex)))
            .Max();
    }

    public List<List<int>> GetGrid(IEnumerable<string> input) =>
        input.Select(line => line.Select(c => int.Parse($"{c}")).ToList()).ToList();

    public bool IsVisible(int height, int row, int column, List<List<int>> grid)
    {
        if (row == 0 || column == 0 || row == grid.Count -1 || column == grid[0].Count - 1)
        {
            return true;
        }

        // test row
        // left
        if (Enumerable.Range(0, column).All(i => grid[row][i] < height))
        {
            return true;
        }

        // right
        if (Enumerable.Range(column + 1, grid[0].Count - 1 - column).All(i => grid[row][i] < height))
        {
            return true;
        }

        // test column
        // above
        if (Enumerable.Range(0, row).All(i => grid[i][column] < height))
        {
            return true;
        }

        //below
        if (Enumerable.Range(row + 1, grid.Count - 1 - row).All(i => grid[i][column] < height))
        {
            return true;
        }

        return false;
    }

    public int GetScenicScore(List<int> line, int location)
    {
        // Always 0 if around the outside
        if (location == 0 || line.Count - 1 == location)
        {
            return 0;
        }

        var upIndex = location + 1;
        var downIndex = location - 1;

        // look up
        while (upIndex < line.Count - 1 && line[upIndex] < line[location])
        {
            upIndex++;
        }

        // look down
        while (downIndex > 0 && line[downIndex] < line[location])
        {
            downIndex--;
        }

        return (upIndex - location) * (location - downIndex);
    }

}
