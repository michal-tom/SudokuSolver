namespace SudokuSolver.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SudokuSolver.Lib.Enums;
    using SudokuSolver.Parser;

    [TestClass]
    public class InputParserTests
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("123456789")]
        [DataRow("..17....2.4...2.......5.7.67.....................................................")]
        [DataRow("..17....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51.")]
        [DataRow("..17....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51...")]
        [DataRow("a.17....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51..")]
        [DataRow("1.17....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51..")]
        [DataRow("..17....2.41..2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51..")]
        [DataRow("..17....2.1...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51..")]
        public void InvalidSudokuGridInputStringTest(string inputGridString)
        {
            var result = InputParser.ParseGridData(inputGridString);

            Assert.IsFalse(result.IsSuccessfull);
            Assert.AreNotEqual(ParsingResultType.NoError, result.Type);
        }

        [DataTestMethod]
        [DataRow("..17....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51..")]
        [DataRow("001700002040002000000050706700060050059000460060020008903080000000900080400005100")]
        [DataRow("__17____2_4___2_______5_7_67___6__5__59___46__6__2___89_3_8_______9___8_4____51__")]
        [DataRow("0017....2.4...2.......5.7.67...6..5..59...46..6..2...89.3.8.......9...8.4....51__")]
        public void ProperSudokuGridInputStringTest(string inputGridString)
        {
            var result = InputParser.ParseGridData(inputGridString);

            Assert.IsTrue(result.IsSuccessfull);
            Assert.AreEqual(ParsingResultType.NoError, result.Type);
            Assert.IsNotNull(result.Grid);
        }
    }
}