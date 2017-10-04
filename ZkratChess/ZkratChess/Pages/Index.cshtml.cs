using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZkratChess.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Step { get; set; }

        /*  chessPiece  = color+role
                nic     0  0000 0000

                w p     1  0000 0001
                w V     2  0000 0010   
                w J     3  0000 0011 
                w S     4  0000 0100
                w D     5  0000 0101
                w K     6  0000 0110

                b p    17  0001 0001
                b V    18  0001 0010
                b J    19  0001 0011
                b S    20  0001 0100
                b D    21  0001 0101
                b K    22  0001 0110
        */

        private static byte[,] chessBoard = new byte[8,8]{
            { 2, 3, 4, 5, 6, 4, 3, 2},
            { 1, 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            {17,17,17,17,17,17,17,17},
            {18,19,20,21,22,20,19,18}};

        public byte GetChessPiece(int i,int j)
        {
            return chessBoard[i, j];
        }

        public void SetChessPiece(int i, int j, byte chessPiece)
        {
            chessBoard[i, j] = chessPiece;
        }

        public bool isWhiteField(int i,int j)
        {
            return (i+j+1) % 2 == 0;
        }

        public bool isWhiteFigure(int i, int j)
        {
            return isWhite(GetChessPiece(i, j));
        }

        public bool isWhite(byte chessPiece)
        {
            return chessPiece < 16;
        }

        public byte getRole(byte chessPiece)
        {
            return (byte)(chessPiece & 0b00000111);
        }

        public char getRoleChar(byte role)
        {
            char[] table = {' ', 'p', 'V', 'J','S','D','K' };
            return table[role];
        }

        public char getCharAt(int i,int j)
        {
            var role = getRole(GetChessPiece(i, j));
            return getRoleChar(role);
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var step = Step;
            int from_i = 7-(step[1]-'1');
            int from_j =    step[0]-'A';
            int to_i = 7-(step[3]-'1');
            int to_j =    step[2]-'A';

            var chessPieceInTo = GetChessPiece(to_i, to_j);
            var chessPieceInFrom = GetChessPiece(from_i, from_j);

            SetChessPiece(from_i, from_j, 0);
            SetChessPiece(to_i, to_j, chessPieceInFrom);


            return Page();
        }
    }
}
