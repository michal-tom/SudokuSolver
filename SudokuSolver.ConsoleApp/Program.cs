using SudokuSolver.Lib;
using SudokuSolver.Parser;

Console.WriteLine("Sudoku Solver App");
Console.WriteLine();
Console.WriteLine($"Input string: {args[0]}");
Console.WriteLine();

var result = InputParser.ParseGridData(args[0]);

if (result == null || !result.IsSuccessfull || result.Grid == null)
{
    Console.WriteLine(result?.GetParsingErrorMessage() ?? "Incorrect input string!");
    return;
}

Console.WriteLine("Input grid:");
Console.WriteLine(result.Grid.GetDescription());

var startTime = DateTime.Now;
var isSolved = GridResolver.TrySolve(result.Grid) && result.Grid.Solved;
var endTime = DateTime.Now;
var totalTimeInMs = (int)(endTime - startTime).TotalMilliseconds;

Console.WriteLine($"Time: {totalTimeInMs} ms");
Console.WriteLine();
Console.WriteLine($"Resolved: {isSolved}");

if (isSolved)
{
    Console.WriteLine();
    Console.WriteLine($"Valid: {GridValidator.Validate(result.Grid)}");
    Console.WriteLine();
    Console.WriteLine(result.Grid.GetDescription());
    Console.WriteLine();
    // Console.WriteLine(result.Grid.GetStepsDescription());
}

Console.ReadKey();