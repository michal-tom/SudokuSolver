namespace SudokuSolver.Lib.Model
{
    using SudokuSolver.Lib.Enums;

    public class CellsGroup
    {
        public CellsGroup(GroupType type, byte number)
        {
            this.Type = type;
            this.Number = number;
            this.Cells = new List<Cell>();
        }

        public GroupType Type { get; set; }

        public byte Number { get; set; }

        public IList<Cell> Cells { get; set; }

        public bool Solved =>
            this.Cells != null &&
            this.Cells.Count == Consts.GridCellsGroupsCount &&
            this.Cells.All(p => p.Solved) == true;

        public IEnumerable<byte> CellsValues => this.Cells?.Where(p => p.Solved).Select(p => p.Value.HasValue ? p.Value.Value : default) ?? new List<byte>();

        public IEnumerable<byte> AvailableCellsValues => Consts.AvailableValues.Except(this.CellsValues);
    }
}
