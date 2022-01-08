namespace SudokuSolver.Lib.Model
{
    public class Cell
    {
        public Cell(byte number, byte row, byte column, byte box, byte? value, Grid grid)
        {
            this.Number = number;
            this.RowNumber = row;
            this.ColumnNumber = column;
            this.BoxNumber = box;
            this.Value = value;
            this.Grid = grid;
        }

        public byte? Value { get; set; }

        public byte Number { get; set; }

        public byte RowNumber {  get; set; }

        public byte ColumnNumber { get; set; }

        public byte BoxNumber { get; set; }

        public IList<byte>? AvailableValues { get; set; }

        public Grid Grid { get; set; }

        public bool Done => this.Value.HasValue;

        public CellsGroup? RowCellsGroup => this.Grid?.RowCellsGroups.FirstOrDefault(p => p.Number == this.RowNumber);

        public CellsGroup? ColumnCellsGroup => this.Grid?.ColumnCellsGroups.FirstOrDefault(p => p.Number == this.ColumnNumber);

        public CellsGroup? BoxCellsGroup => this.Grid?.BoxCellsGroups.FirstOrDefault(p => p.Number == this.BoxNumber);

        public override string ToString() => this.Value?.ToString() ?? " ";
    }
}
