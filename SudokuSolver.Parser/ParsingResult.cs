namespace SudokuSolver.Parser
{
    using SudokuSolver.Lib;
    using SudokuSolver.Lib.Enums;
    using SudokuSolver.Lib.Model;

    public class ParsingResult
    {
        public ParsingResult()
        {
            this.IsSuccessfull = true;
            this.Type = ParsingResultType.NoError;
        }

        public bool IsSuccessfull { get; set; }

        public ParsingResultType Type { get; set; }

        public string? ErrorMesage { get; set; }

        public Grid? Grid { get; set; }

        public string? GetParsingErrorMessage()
        {
            switch (this.Type)
            {
                case ParsingResultType.NoError:
                    return null;
                case ParsingResultType.EmptyInput:
                    return "Input string cannot be empty!";
                case ParsingResultType.InvalidChars:
                    return "Input string contains forbidden chars!";
                case ParsingResultType.InvalidLength:
                    return $"Input string should have exactly {Consts.GridCellsCount} chars!";
                case ParsingResultType.NotEnoughValues:
                    return $"Input string should have minimum {Consts.MinimumInputCellsCount} values already set!";
                case ParsingResultType.NotValid:
                    return "Sudoku puzzle build on input string is not valid!";
                default:
                    throw new ArgumentException("Unsupported type of error", nameof(this.Type));
            }
        }
    }
}
