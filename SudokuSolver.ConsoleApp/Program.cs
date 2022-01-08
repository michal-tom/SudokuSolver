// See https://aka.ms/new-console-template for more information

using SudokuSolver.Lib;
using SudokuSolver.Parser;

Console.WriteLine("Sudoku Solver app");
Console.WriteLine();
Console.WriteLine($"Input string: {args[0]}");
Console.WriteLine();

var result = InputParser.ParseGridData(args[0]);

if (result == null || !result.IsSuccessfull || result.Grid == null)
{
    //TODO: print error
    //Console.WriteLine("Input string is empty!");
    //Console.WriteLine($"Input string should have exactly {Consts.GridCellsCount} chars (passed string has {inputChars.Length} chars)!");
    //Console.WriteLine($"Input string contains forbidden chars!");
    Console.WriteLine("Incorrect input string!");
    return;
}

Console.WriteLine("Input grid:");
Console.WriteLine(result.Grid.GetDescription());

var isSuccessfull = GridResolver.TrySolve(result.Grid);

Console.WriteLine($"Resolved: {isSuccessfull}");
Console.WriteLine(result.Grid.GetDescription());
Console.WriteLine();
Console.WriteLine(result.Grid.GetStepsDescription());

Console.ReadKey();