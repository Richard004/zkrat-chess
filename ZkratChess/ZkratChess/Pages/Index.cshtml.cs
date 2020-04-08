using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZkratChess.Library;
using ZkratChess.Services;

namespace ZkratChess.Pages
{
    public class IndexModel : PageModel
    {
        ChessPersistenceService persistenceService = new ChessPersistenceService();

        [BindProperty(SupportsGet = true)]
        public string Game { get; set; }

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

        private byte[,] chessBoard;

        public byte GetChessPiece(int i,int j)
        {
            return chessBoard[i, j];
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


        public string getFigureStringAt(int i,int j)
        {
            byte role = getRole(GetChessPiece(i, j));
            string BootStrapFigure = null;
            if(role == 1)
            {
                BootStrapFigure = @"<span class=""glyphicon glyphicon-pawn""></span>";
            }
            if (role == 2)
            {
                BootStrapFigure =  @"<span class=""glyphicon glyphicon-tower""></span>";
            }
            if (role == 3)
            {
                BootStrapFigure = @"<span class=""glyphicon glyphicon-knight""></span>";
            }
            if (role == 4)
            {
                BootStrapFigure = @"<span class=""glyphicon glyphicon-bishop""></span>";
            }
            if (role == 5)
            {
                BootStrapFigure = @"<span class=""glyphicon glyphicon-queen""></span>";
            }
            if (role == 6)
            {
                BootStrapFigure = @"<span class=""glyphicon glyphicon-king""></span>";
            }
           

            return BootStrapFigure;
            
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Game))
                return Redirect("home");

            if (!persistenceService.ExistsGame(Game))
                return Redirect("home");

            chessBoard = persistenceService.LoadBoard(Game);
            if (chessBoard == null)
                return Redirect("home");

            return Page();
        }

        public IActionResult OnPost()
        {
            chessBoard = persistenceService.LoadBoard(Game);

            var moveValidator = new ChessMoveValidator(chessBoard, Step);
            moveValidator.Validate();
            foreach(var error in moveValidator.GetAllErrors())
            {
                ModelState.AddModelError("step", error);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            moveValidator.MakeMove();
            persistenceService.SaveBoard(Game, chessBoard);

            return Page();
        }
    }
}
