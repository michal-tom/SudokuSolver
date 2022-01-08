namespace SudokuSolver.Parser
{
    using SudokuSolver.Lib;
    using SudokuSolver.Lib.Enums;

    public static class InputParser
    {
        public static ParsingResult ParseGridData(string input)
        {
            var result = ValidateInputString(input);
            if (!result.IsSuccessfull)
            {
                return result;
            }

            var initialGridValues = ParseInputValues(input);

            if (initialGridValues.Count(p => p != null) < Consts.MinimumInputCellsCount)
            {
                return new ParsingResult { IsSuccessfull = false, Type = ParsingResultType.NotEnoughValues };
            }

            var grid = GridPreparator.CreateGrid(initialGridValues);

            if (!GridValidator.Validate(grid))
            {
                return new ParsingResult { IsSuccessfull = false, Type = ParsingResultType.NotEnoughValues };
            }

            GridPreparator.AnalyzeCells(grid);

            return new ParsingResult { IsSuccessfull = true, Type = ParsingResultType.NoError, Grid = grid };
        }

        private static ParsingResult ValidateInputString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ParsingResult { IsSuccessfull = false, Type = ParsingResultType.EmptyInput };
            }

            var inputChars = input.Trim().ToCharArray();
            if (inputChars.Length != Consts.GridCellsCount)
            {
                return new ParsingResult { IsSuccessfull = false, Type = ParsingResultType.InvalidLength };
            }

            if (!inputChars.All(p => Consts.ValuesChars.Contains(p) || Consts.EmptyValuesMarkers.Contains(p)))
            {
                return new ParsingResult { IsSuccessfull = false, Type = ParsingResultType.InvalidChars };
            }

            return new ParsingResult { IsSuccessfull = true };
        }

        private static List<byte?> ParseInputValues(string input)
        {
            var gridCellsValues = new List<byte?>();

            foreach (var inputChar in input.Trim().ToCharArray())
            {
                if (Consts.EmptyValuesMarkers.Contains(inputChar))
                {
                    gridCellsValues.Add(null);
                }
                else if (byte.TryParse(inputChar.ToString(), out var inputGridValue))
                {
                    gridCellsValues.Add(inputGridValue);
                }
            }

            return gridCellsValues;
        }
    }
}
