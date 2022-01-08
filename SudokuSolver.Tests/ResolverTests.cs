namespace SudokuSolver.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SudokuSolver.Lib;
    using SudokuSolver.Parser;

    [TestClass]
    public class ResolverTests
    {
        [DataTestMethod]
        [DataRow("6..9......3.4.5.1.7.8.1.5..917.6.4......4......4.8.932..5.2.8.6.7.1.4.2......6..1", "651938274239475618748612593917263485382549167564781932195327846876154329423896751")]
        public void SudokuGridSolvingTest(string inputGridString, string solvedGridString)
        {
            var result = InputParser.ParseGridData(inputGridString);

            Assert.AreEqual(true, result.IsSuccessfull);
            Assert.IsNotNull(result.Grid);

            var isSuccessfull = GridResolver.TrySolve(result.Grid);

            Assert.AreEqual(true, isSuccessfull);
            Assert.AreEqual(result.Grid.ToString(), solvedGridString);
        }
    }
}