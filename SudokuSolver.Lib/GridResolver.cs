namespace SudokuSolver.Lib
{
    using SudokuSolver.Lib.Model;

    public static class GridResolver
    {
        public static bool TrySolve(Grid grid)
        {
            while (!grid.Done)
            {
                if (TryPickValuesForCellsWithOnlyOneAvailableValue(grid.Cells))
                {
                    continue;
                }

                if (TryFindGroupsWithValuesThatCouldBeOnlyInOneCell(grid.AllGridCellsGroups))
                {
                    continue;
                }

                break;
            }

            return grid.Done;
        }

        private static bool TryPickValuesForCellsWithOnlyOneAvailableValue(IList<Cell> gridCells)
        {
            var cells = gridCells.Where(p => p.AvailableValues?.Count == 1);
            if (!cells.Any())
            {
                return false;
            }

            foreach (var cell in cells)
            {
                AddCellValue(cell, cell.AvailableValues.First());
            }

            return true;
        }

        private static bool TryFindGroupsWithValuesThatCouldBeOnlyInOneCell(IEnumerable<CellsGroup> cellsGroups)
        {
            var newCellValues = new Dictionary<Cell, byte>();

            foreach (var cellsGroup in cellsGroups.Where(p => !p.Done))
            {
                foreach (var availableValue in cellsGroup.AvailableCellsValues)
                {
                    var cells = cellsGroup.Cells.Where(p => !p.Done && p.AvailableValues != null && p.AvailableValues.Contains(availableValue));
                    if (cells.Count() == 1 && !newCellValues.ContainsKey(cells.First()))
                    {
                        newCellValues.Add(cells.First(), availableValue);
                    }
                }
            }

            if (!newCellValues.Any())
            {
                return false;
            }

            foreach(var newCellValue in newCellValues)
            {
                AddCellValue(newCellValue.Key, newCellValue.Value);
            }

            return true;
        }

        private static bool AddCellValue(Cell cell, byte value)
        {
            if (cell.Done || cell.AvailableValues == null || !cell.AvailableValues.Contains(value))
            {
                return false;
            }

            cell.Value = value;
            cell.AvailableValues = null;

            UpdateAvailableValuesInCellsGroup(cell.RowCellsGroup, value);
            UpdateAvailableValuesInCellsGroup(cell.ColumnCellsGroup, value);
            UpdateAvailableValuesInCellsGroup(cell.BoxCellsGroup, value);

            return true;
        }

        private static void UpdateAvailableValuesInCellsGroup(CellsGroup? cellsGroup, byte value)
        {
            if (cellsGroup == null || cellsGroup.Done)
            {
                return;
            }

            foreach (var cell in cellsGroup.Cells.Where(p => !p.Done))
            {
                if (cell.AvailableValues != null && cell.AvailableValues.Contains(value))
                {
                    cell.AvailableValues.Remove(value);
                }
            }
        }
    }
}
