namespace SudokuSolver.Lib
{
    public class Consts
    {
        public const byte GridCellsCount = 81;

        public const byte GridCellsGroupsCount = 9;

        public const byte MinimumInputCellsCount = 10;

        public readonly static IList<byte> AvailableValues = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public readonly static IList<char> ValuesChars = AvailableValues.Select(p => p.ToString().ToCharArray()[0]).ToList();

        public readonly static IList<char> EmptyValuesMarkers = new List<char> { '0', '.', '_' };
    }
}
