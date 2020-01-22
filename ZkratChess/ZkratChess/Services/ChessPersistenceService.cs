using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZkratChess.Services
{
    public class ChessPersistenceService
    {
        private static byte[,] chessBoardNewGame = new byte[8, 8]{
            { 2, 3, 4, 5, 6, 4, 3, 2},
            { 1, 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            {17,17,17,17,17,17,17,17},
            {18,19,20,21,22,20,19,18}};


        public void SaveBoard(string gameName,byte[,] board)
        {
            var fileName = GameToFileName(gameName);
            var dirName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var file = File.Create(fileName);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    file.WriteByte(board[i, j]);

            file.Close();
        }

        private static string GameToFileName(string gameName)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var fileName = Path.Combine(homePath,"ChessGames", "game-" + gameName + ".xfd");
            return fileName;
        }


        public byte[,] LoadOrCreateNewGame(string gameName)
        {
            string fileName = GameToFileName(gameName);
            if (!File.Exists(fileName))
            {
                SaveBoard(gameName, chessBoardNewGame);
            }
            return LoadBoard(gameName);
        }

        public byte[,] LoadBoard(string gameName)
        {
            string fileName = GameToFileName(gameName);
            var file = File.OpenRead(fileName);

            var board = new byte[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j] = (byte)file.ReadByte();

            file.Close();
            return board;
        }
    }
}
