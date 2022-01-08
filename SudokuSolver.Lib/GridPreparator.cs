namespace SudokuSolver.Lib
{
    using SudokuSolver.Lib.Enums;
    using SudokuSolver.Lib.Model;

    public static class GridPreparator
    {
        public static Grid CreateGrid(IList<byte?> inputValues)
        {
            var grid = new Grid();
            CreateGridGroups(grid);
            AddGridCells(grid, inputValues);
            return grid; 
        }

        public static void AnalyzeCells(Grid grid)
        {
            foreach (var cell in grid.Cells.Where(p => !p.Done))
            {
                var availableValues = Consts.AvailableValues.AsEnumerable();
                if (cell.RowCellsGroup != null)
                {
                    availableValues = availableValues.Except(cell.RowCellsGroup.CellsValues);
                }

                if (cell.ColumnCellsGroup != null)
                {
                    availableValues = availableValues.Except(cell.ColumnCellsGroup.CellsValues);
                }

                if (cell.BoxCellsGroup != null)
                {
                    availableValues = availableValues.Except(cell.BoxCellsGroup.CellsValues);
                }

                cell.AvailableValues = availableValues.ToList();
            }
        }
     
        private static void CreateGridGroups(Grid grid)
        {
            for (var i = 0; i < Consts.GridCellsGroupsCount; i++)
            {
                var groupNumber = (byte)(i + 1);
                grid.RowCellsGroups.Add(new CellsGroup(GroupType.Row, groupNumber));
                grid.ColumnCellsGroups.Add(new CellsGroup(GroupType.Column, groupNumber));
                grid.BoxCellsGroups.Add(new CellsGroup(GroupType.Box, groupNumber));
            }
        }

        private static void AddGridCells(Grid grid, IList<byte?> inputValues)
        {
            for (byte i = 0; i < Consts.GridCellsCount; i++)
            {
                var cellNumber = (byte) (i + 1);
                var rowNumber = (byte) ((i / Consts.GridCellsGroupsCount) + 1);
                var columnNumber = (byte) (i % Consts.GridCellsGroupsCount + 1);
                var boxNumber = (byte) (((rowNumber - 1) / 3) + (3 * ((columnNumber - 1) / 3)) + 1);
                var cellValue = inputValues[i];

                var cell = new Cell(cellNumber, rowNumber, columnNumber, boxNumber, cellValue, grid);

                grid.Cells.Add(cell);
                grid.RowCellsGroups.First(p => p.Number == rowNumber).Cells.Add(cell);
                grid.ColumnCellsGroups.First(p => p.Number == columnNumber).Cells.Add(cell);
                grid.BoxCellsGroups.First(p => p.Number == boxNumber).Cells.Add(cell);
            }
        }
    }
}
