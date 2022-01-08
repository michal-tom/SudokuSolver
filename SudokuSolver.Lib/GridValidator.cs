namespace SudokuSolver.Lib
{
    using SudokuSolver.Lib.Model;

    public static class GridValidator
    {
        public static bool Validate(Grid grid)
        {
            return ValidateCellGroups(grid.RowCellsGroups) && ValidateCellGroups(grid.ColumnCellsGroups) && ValidateCellGroups(grid.BoxCellsGroups);  
        }

        private static bool ValidateCellGroups(IList<CellsGroup> cellGroups)
        {
            var result = true;

            foreach (var cellGroup in cellGroups)
            {
                var cellsValues = cellGroup.Cells.Select(p => p.Value);
                var duplicatedCellValues = cellsValues.Where(p => p != null).GroupBy(p => p).Where(p => p.Count() > 1);
                if (duplicatedCellValues.Any())
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
