var day10 = new Day10();

// Part 1
Console.WriteLine($"Part 1 Answer: {day10.GetPart1Answer(day10.InputData)}");

// Part 2
Console.WriteLine($"Part 2 Answer:");
Console.WriteLine(
    string.Join(Environment.NewLine, 
        day10.GetPart2Answer(day10.InputData).Select(c => c.Replace(".", " ")
)));
