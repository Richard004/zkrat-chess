using System.Collections.Generic;

namespace ZkratChess.Library
{


    public class ChessMoveValidator
    {
        private byte[,] chessBoard;
        private int from_i;
        private int from_j;
        private int to_i;
        private int to_j;
        private List<string> stepErrors = new List<string>();

        public ChessMoveValidator(byte[,] chessBoard, int from_i, int from_j, int to_i, int to_j)
        {
            this.chessBoard = chessBoard;
            this.from_i = from_i;
            this.from_j = from_j;
            this.to_i = to_i;
            this.to_j = to_j;
        }

        public ChessMoveValidator(byte[,] chessBoard, string step)
        {
            this.chessBoard = chessBoard;
            this.from_i = 7 - (step[1] - '1');
            this.from_j = step[0] - 'A';
            this.to_i = 7 - (step[3] - '1');
            this.to_j = step[2] - 'A';
        }

        public byte GetChessPiece(int i, int j)
        {
            return chessBoard[i, j];
        }

        public bool isWhite(byte chessPiece)
        {
            return chessPiece < 16;
        }

        public byte getRole(byte chessPiece)
        {
            return (byte)(chessPiece & 0b00000111);
        }

        public void SetChessPiece(int i, int j, byte chessPiece)
        {
            chessBoard[i, j] = chessPiece;
        }


        public void Validate()
        {
            var chessPieceInTo = GetChessPiece(to_i, to_j);
            var chessPieceInFrom = GetChessPieceInFrom();

            var from_color = isWhite(chessPieceInFrom);
            var to_color = isWhite(chessPieceInTo);

            var from_role = getRole(chessPieceInFrom);
            var to_role = getRole(chessPieceInTo);

            if (chessPieceInFrom == 0)
            {
                stepErrors.Add("Invalid move! Protože nemůžeme táhnout z prázdného pole!");
            }

            if (from_i == to_i && from_j == to_j)
            {
                stepErrors.Add("Invalid move! Táhnout je potřeba někam jinam než na výchozí políčko!");
            }

            if (chessPieceInTo != 0 && from_color == to_color)
            {
                stepErrors.Add("Invalid move! Není povoleno sebrat figurku stejné barvy!");
            }

            if (from_role == 1)
            {
                ValidatePesak(chessPieceInTo, from_color);
            }
        }

        private void ValidatePesak(byte chessPieceInTo, bool from_color)
        {
            var iTolerance = (from_color == true) ? (+1) : (-1);
            var colorName = (from_color == true) ? "Bílý" : "Černý";

            if (chessPieceInTo != 0) //neco bereme
            {
                if (!(to_i - from_i == iTolerance && (from_j - to_j == 1 || from_j - to_j == -1)))
                {
                    stepErrors.Add("Invalid move! " + colorName + " pěšec může brát jen šikmo o 1");
                }
            }
            else//nic nebereme
            {
                if (from_j != to_j)
                {
                    stepErrors.Add("Invalid move! " + colorName + " pěšec nemůže táhnout dostrany");
                }

                if ((from_color == true) ? (from_i >= 2) : (from_i <= 5))
                {
                    if (!((to_i - from_i) == iTolerance))
                    {
                        stepErrors.Add("Invalid move! " + colorName + " pěšec může táhnout pouze o 1 pole dopředu");
                    }
                }
                else
                {
                    if (!((to_i - from_i) == iTolerance || (to_i - from_i) == 2 * iTolerance))
                    {
                        stepErrors.Add("Invalid move! " + colorName + " pěšec v základní pozici může táhnout pouze o 1 nebo 2 pole dopředu");
                    }
                }
            }
        }

        private byte GetChessPieceInFrom()
        {
            return GetChessPiece(from_i, from_j);
        }

        public List<string> GetAllErrors()
        {
            return stepErrors;
        }

        public void MakeMove()
        {
            SetChessPiece(to_i, to_j, GetChessPieceInFrom());
            SetChessPiece(from_i, from_j, 0);

        }

    }
}
