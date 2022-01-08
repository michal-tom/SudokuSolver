namespace SudokuSolver.Lib.Model
{
    public class SolvingStep
    {
        public SolvingStep(byte number, Cell cell, byte value)
        {
            this.Number = number;
            this.Cell = cell;
            this.Value = value;
            this.AvailableCellValues = cell.AvailableValues?.ToList();
            this.UpdatedAvailableValuesCells = new List<Cell>();
        }

        public byte Number { get; set; }

        public byte Value { get; set; }

        public Cell Cell { get; set; }

        public IList<byte>? AvailableCellValues { get; set; }

        public IList<Cell> UpdatedAvailableValuesCells { get; set; }

        public override string ToString()
        {
            return $"Step no {this.Number}: value {this.Value} in cell ({this.Cell.RowNumber}, {this.Cell.ColumnNumber})";
        }
    }
}
