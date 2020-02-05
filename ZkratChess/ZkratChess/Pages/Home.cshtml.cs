using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZkratChess.Services;

namespace ZkratChess.Pages
{
    public class HomeModel : PageModel
    {
        ChessPersistenceService persistenceService = new ChessPersistenceService();

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Zkrat chess, the best chess online!";
        }


        public IActionResult OnPost()
        {
            var r = new Random();
            var c = r.Next(7, 147);
            var gameName = c.ToString("000");
            persistenceService.LoadOrCreateNewGame(gameName);
            return Redirect($"index?game={gameName}");
        }
    }
}
