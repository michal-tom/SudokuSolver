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
                SetCellValue(cell, cell.AvailableValues.First());
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
                SetCellValue(newCellValue.Key, newCellValue.Value);
            }

            return true;
        }

        private static bool SetCellValue(Cell cell, byte value)
        {
            if (cell.Done || cell.AvailableValues == null || !cell.AvailableValues.Contains(value))
            {
                return false;
            }

            var stepNumber = (byte) (cell.Grid.SolvingSteps.Count + 1);
            var newSolvingStep = new SolvingStep(stepNumber, cell, value);

            cell.Value = value;
            cell.AvailableValues = null;
            cell.Grid.SolvingSteps.Add(newSolvingStep);

            var updatedCells = new List<Cell>();

            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.RowCellsGroup, value));
            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.ColumnCellsGroup, value));
            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.BoxCellsGroup, value));

            newSolvingStep.UpdatedAvailableValuesCells = updatedCells;

            return true;
        }

        private static IList<Cell> UpdateAvailableValuesInCellsGroup(CellsGroup? cellsGroup, byte value)
        {
            var updatedCells = new List<Cell>();

            if (cellsGroup == null || cellsGroup.Done)
            {
                return updatedCells;
            }

            foreach (var cell in cellsGroup.Cells.Where(p => !p.Done))
            {
                if (cell.AvailableValues != null && cell.AvailableValues.Contains(value))
                {
                    cell.AvailableValues.Remove(value);
                    updatedCells.Add(cell);
                }
            }

            return updatedCells;
        }
    }
}
