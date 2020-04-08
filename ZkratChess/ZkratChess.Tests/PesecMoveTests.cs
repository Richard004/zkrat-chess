using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZkratChess.Library;

namespace ZkratChess.Tests
{
    [TestClass]
    public class PesecMoveTests
    {
        [TestMethod]
        public void TestBilyO2Ok()
        {
            var chessBoard = ChessBoard.NewGame;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2C4");
            moveValidator.Validate();
            Assert.AreEqual(0, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestBilyO3NotOk()
        {
            var chessBoard = ChessBoard.NewGame;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2C5");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

    }
}
