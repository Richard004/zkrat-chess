using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZkratChess.Library;

namespace ZkratChess.Services
{
    public class ChessPersistenceService
    {


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

        public bool ExistsGame(string gameName)
        {
            string fileName = GameToFileName(gameName);
            return File.Exists(fileName);
        }

        public byte[,] LoadOrCreateNewGame(string gameName)
        {
            string fileName = GameToFileName(gameName);
            if (!File.Exists(fileName))
            {
                SaveBoard(gameName, ChessBoard.NewGame);
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
