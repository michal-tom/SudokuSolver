namespace SudokuSolver.Parser
{
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

        public Grid? Grid { get; set; }
    }
}
