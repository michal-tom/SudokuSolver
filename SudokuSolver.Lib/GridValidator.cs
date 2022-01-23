namespace SudokuSolver.Lib
{
    using SudokuSolver.Lib.Model;

    public static class GridValidator
    {
        public static bool Validate(Grid grid)
        {
            return ValidateAvailableCellValues(grid.Cells)
                && ValidateCellGroups(grid.RowCellsGroups)
                && ValidateCellGroups(grid.ColumnCellsGroups)
                && ValidateCellGroups(grid.BoxCellsGroups);  
        }

        private static bool ValidateAvailableCellValues(IList<Cell> cells)
        {
            return cells.All(p => p.Solved || p.AvailableValues == null || p.AvailableValues.Any());
        }

        private static bool ValidateCellGroups(IList<CellsGroup> cellGroups)
        {
            var result = true;

            foreach (var cellGroup in cellGroups)
            {
                var duplicatedValues = cellGroup.CellsValues.GroupBy(p => p).Where(p => p.Count() > 1);
                if (duplicatedValues.Any())
                {
                    result = false;
                    break;
                }

                foreach (var avaliableValue in cellGroup.AvailableCellsValues)
                {
                    if (!cellGroup.Cells.Any(p => !p.Solved && (p.AvailableValues == null || p.AvailableValues.Contains(avaliableValue))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
