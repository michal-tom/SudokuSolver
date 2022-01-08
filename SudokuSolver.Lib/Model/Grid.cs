namespace SudokuSolver.Lib.Model
{
    using System.Text;

    public class Grid
    {
        public Grid()
        {
            this.Cells = new List<Cell>();
            this.RowCellsGroups = new List<CellsGroup>();
            this.ColumnCellsGroups = new List<CellsGroup>();
            this.BoxCellsGroups = new List<CellsGroup>();
        }

        public IList<Cell> Cells { get; set; }

        public IList<CellsGroup> RowCellsGroups { get; set; }

        public IList<CellsGroup> ColumnCellsGroups { get; set; }

        public IList<CellsGroup> BoxCellsGroups { get; set; }

        public bool Done =>
            this.Cells != null &&
            this.Cells.Count == Consts.GridCellsCount &&
            this.Cells.All(p => p.Done) == true;

        public byte EmptyCellsCount => (byte) (Consts.GridCellsCount - this.Cells?.Count(p => p.Done) ?? 0);

        public IEnumerable<CellsGroup> AllGridCellsGroups => this.RowCellsGroups.Concat(this.ColumnCellsGroups).Concat(this.BoxCellsGroups);

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var cell in this.Cells)
            {
                sb.Append(cell);
            }

            return sb.ToString();
        }

        public string GetDescription()
        {
            var sb = new StringBuilder();

            foreach (var row in this.RowCellsGroups)
            {
                foreach (var cell in row.Cells)
                {
                    sb.Append("|");
                    sb.Append(cell);
                }

                sb.AppendLine("|");
            }

            return sb.ToString();
        }
    }
}
