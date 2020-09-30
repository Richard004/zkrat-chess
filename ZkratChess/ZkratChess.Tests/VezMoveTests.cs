using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZkratChess.Library;

namespace ZkratChess.Tests
{
    [TestClass]
    public class VezMoveTests
    {
        [TestMethod]
        public void TestMoveWhiteVezSkrzPesaky01()
        {
            var chessBoard = ChessBoard.NewGameWhite;
            var moveValidator = new ChessMoveValidator(chessBoard, "A8A6");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestMoveWhiteVezSikmo()
        {
            var chessBoard = ChessBoard.NewGameWhite;
            var moveValidator = new ChessMoveValidator(chessBoard, "A8C6");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

    }
}
