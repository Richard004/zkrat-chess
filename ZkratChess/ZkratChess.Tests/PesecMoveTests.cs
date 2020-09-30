using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZkratChess.Library;

namespace ZkratChess.Tests
{
    [TestClass]
    public class PesecMoveTests
    {

        private static GameState Game001 => new GameState{
            Board=new byte[8, 8]{
                { 2, 3, 4, 5, 6, 4, 3, 2},
                { 1, 0, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0},
                { 0, 0,17, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                {17,17, 0,17,17,17,17,17},
                {18,19,20,21,22,20,19,18}
            },
            IsWhiteMove = false
        };

        private static GameState Game001White => new GameState
        {
            Board = new byte[8, 8]{
                { 2, 3, 4, 5, 6, 4, 3, 2},
                { 1, 0, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0},
                { 0, 0,17, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                {17,17, 0,17,17,17,17,17},
                {18,19,20,21,22,20,19,18}
            },
            IsWhiteMove = true
        };


        [TestMethod]
        public void TestBlackO1Ok()
        {
            var chessBoard = ChessBoard.NewGameBlack;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2C3");
            moveValidator.Validate();
            Assert.AreEqual(0, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestBlackSikmoNotk()
        {
            var chessBoard = ChessBoard.NewGameBlack;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2B3");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestBlackO2Ok()
        {
            var chessBoard = ChessBoard.NewGameBlack;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2C4");
            moveValidator.Validate();
            Assert.AreEqual(0, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestBlackO3NotOk()
        {
            var chessBoard = ChessBoard.NewGameBlack;
            var moveValidator = new ChessMoveValidator(chessBoard, "C2C5");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestMoveFromEmptyFieldNotOk()
        {
            var chessBoard = ChessBoard.NewGameWhite;
            var moveValidator = new ChessMoveValidator(chessBoard, "C3C5");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestTakeWiteB5C4()
        {
            var chessBoard = Game001White;
            var moveValidator = new ChessMoveValidator(chessBoard, "B5C4");
            moveValidator.Validate();
            Assert.AreEqual(0, moveValidator.GetAllErrors().Count);
        }

        [TestMethod]
        public void TestTakeBlackB2B7()
        {
            var chessBoard = Game001;
            var moveValidator = new ChessMoveValidator(chessBoard, "B2B7");
            moveValidator.Validate();
            Assert.AreEqual(1, moveValidator.GetAllErrors().Count);
        }

    }
}
