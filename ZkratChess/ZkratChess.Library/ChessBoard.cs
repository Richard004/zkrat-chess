namespace ZkratChess.Library
{
    public class ChessBoard
    {
        public static GameState NewGameWhite => 
            new GameState
            {
                Board = new byte[8, 8]{
                    { 2, 3, 4, 5, 6, 4, 3, 2},
                    { 1, 1, 1, 1, 1, 1, 1, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    {17,17,17,17,17,17,17,17},
                    {18,19,20,21,22,20,19,18}},
                IsWhiteMove = true
            };

        public static GameState NewGameBlack =>
            new GameState
            {
                Board = new byte[8, 8]{
                    { 2, 3, 4, 5, 6, 4, 3, 2},
                    { 1, 1, 1, 1, 1, 1, 1, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0, 0},
                    {17,17,17,17,17,17,17,17},
                    {18,19,20,21,22,20,19,18}},
                IsWhiteMove = false
            };

    }

}
