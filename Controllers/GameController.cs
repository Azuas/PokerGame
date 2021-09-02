using Microsoft.AspNetCore.Mvc;

namespace PokerGame.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
