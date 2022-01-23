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
            this.SolvingSteps = new List<SolvingStep>();
        }

        public IList<Cell> Cells { get; set; }

        public IList<CellsGroup> RowCellsGroups { get; set; }

        public IList<CellsGroup> ColumnCellsGroups { get; set; }

        public IList<CellsGroup> BoxCellsGroups { get; set; }

        public IList<SolvingStep> SolvingSteps { get; set; }

        public bool Solved =>
            this.Cells != null &&
            this.Cells.Count == Consts.GridCellsCount &&
            this.Cells.All(p => p.Solved) == true;

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

        public string GetStepsDescription()
        {
            var sb = new StringBuilder();

            foreach (var step in this.SolvingSteps)
            {
                sb.AppendLine(step.ToString());
            }

            return sb.ToString();
        }
    }
}
