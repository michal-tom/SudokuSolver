namespace SudokuSolver.Lib
{
    using SudokuSolver.Lib.Model;

    public static class GridResolver
    {
        public static bool TrySolve(Grid grid)
        {
            if (TryUseSmartStrategy(grid))
            {
                return true;
            }

            if (!GridValidator.Validate(grid))
            {
                return false;
            }

            if (TryUseBruteForceStrategy(grid))
            {
                return true;
            }

            return false;
        }

        private static bool TryUseSmartStrategy(Grid grid)
        {
            while (!grid.Solved)
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

            return grid.Solved;
        }

        private static bool TryUseBruteForceStrategy(Grid grid)
        {
            var gridSolved = false;
            var firstEmptyCell = grid.Cells.Where(p => !p.Solved).OrderBy(p => p.AvailableValues?.Count() ?? default).ThenBy(p => p.Number).FirstOrDefault();

            if (firstEmptyCell?.AvailableValues == null)
            {
                return grid.Solved;
            }

            var availableValues = firstEmptyCell.AvailableValues.OrderBy(p => p).ToList();

            foreach (var guessedValue in availableValues)
            {
                gridSolved = TryGuessNextValue(firstEmptyCell, guessedValue);
                if (gridSolved)
                {
                    break;
                }
            }

            return gridSolved;
        }

        private static bool TryGuessNextValue(Cell cell, byte guessedValue)
        {
            var solvingStep = SetCellValue(cell, guessedValue, true);

            if (TrySolve(cell.Grid))
            {
                return true;
            }

            if (solvingStep != null)
            {
                RestoreSolvingSteps(cell.Grid, solvingStep);
            }

            return false;
        }

        private static void RestoreCellValue(SolvingStep gridSolvingStep)
        {
            var cell = gridSolvingStep.Cell;
            cell.Value = null;
            cell.AvailableValues = gridSolvingStep.AvailableCellValues;
            foreach(var updatetedCell in gridSolvingStep.UpdatedAvailableValuesCells)
            {
                updatetedCell.AvailableValues?.Add(gridSolvingStep.Value);
            }
        }

        private static bool TryPickValuesForCellsWithOnlyOneAvailableValue(IList<Cell> gridCells)
        {
            var cellsWithOnlyOneAvailableValue = gridCells.Where(p => p.AvailableValues?.Count == 1);
            if (!cellsWithOnlyOneAvailableValue.Any())
            {
                return false;
            }

            foreach (var cell in cellsWithOnlyOneAvailableValue)
            {
                if (cell.AvailableValues != null && cell.AvailableValues.Any())
                {
                    SetCellValue(cell, cell.AvailableValues.First());
                }
            }

            return true;
        }

        private static bool TryFindGroupsWithValuesThatCouldBeOnlyInOneCell(IEnumerable<CellsGroup> cellsGroups)
        {
            var newCellValues = new Dictionary<Cell, byte>();

            foreach (var cellsGroup in cellsGroups.Where(p => !p.Solved))
            {
                foreach (var availableValue in cellsGroup.AvailableCellsValues)
                {
                    var cells = cellsGroup.Cells.Where(p => !p.Solved && p.AvailableValues != null && p.AvailableValues.Contains(availableValue));
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

        private static SolvingStep? SetCellValue(Cell cell, byte value, bool isGuessed = false)
        {
            if (cell.Solved || cell.AvailableValues == null || !cell.AvailableValues.Contains(value))
            {
                return null;
            }

            var stepNumber = (byte) (cell.Grid.SolvingSteps.Count + 1);
            var solvingStep = new SolvingStep(stepNumber, cell, value, isGuessed);

            cell.Value = value;
            cell.AvailableValues = null;
            cell.Grid.SolvingSteps.Add(solvingStep);

            var updatedCells = new List<Cell>();

            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.RowCellsGroup, value));
            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.ColumnCellsGroup, value));
            updatedCells.AddRange(UpdateAvailableValuesInCellsGroup(cell.BoxCellsGroup, value));

            solvingStep.UpdatedAvailableValuesCells = updatedCells;

            return solvingStep;
        }

        private static IList<Cell> UpdateAvailableValuesInCellsGroup(CellsGroup? cellsGroup, byte value)
        {
            var updatedCells = new List<Cell>();

            if (cellsGroup == null || cellsGroup.Solved)
            {
                return updatedCells;
            }

            foreach (var cell in cellsGroup.Cells.Where(p => !p.Solved))
            {
                if (cell.AvailableValues != null && cell.AvailableValues.Contains(value))
                {
                    cell.AvailableValues.Remove(value);
                    updatedCells.Add(cell);
                }
            }

            return updatedCells;
        }

        private static void RestoreSolvingSteps(Grid grid, SolvingStep solvingStep)
        {
            var gridSolvingSteps = grid.SolvingSteps.Where(p => p.Number >= solvingStep.Number).OrderByDescending(p => p.Number).ToList();

            foreach (var gridSolvingStep in gridSolvingSteps)
            {
                RestoreCellValue(gridSolvingStep);
                grid.SolvingSteps.Remove(gridSolvingStep);
            }
        }
    }
}
