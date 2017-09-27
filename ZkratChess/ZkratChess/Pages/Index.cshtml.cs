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

        private byte[,] chessBoard = new byte[8,8]{
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0}};


        private bool isWhite(byte chessPiece)
        {
            return chessPiece < 16;
        }

        private byte getRole(byte chessPiece)
        {
            return (byte)(chessPiece & 0b00000111);
        }

        public void OnGet()
        {

        }
    }
}
