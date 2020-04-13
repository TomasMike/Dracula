using Dracula.Web.Models;
using System.Web.Mvc;

namespace Dracula.Web.NET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [Route("/Route/Lobby")]
        public ActionResult Lobby()
        {
            return View(new LobbyModel() { Data = LobbyManager.GetPlayersSimpleObj() });
        }

        [Route("/Route/Game")]
        public ActionResult Game()
        {
            return View();
        }

        public ActionResult LoginUser(string name)
        {
            if (string.IsNullOrEmpty(name) || !LobbyManager.IsNickAvailable(name))
            {
                return View("Index", new LoginModel() { IsError = true });
            }

            LobbyManager.AddPlayer(name);

            ViewBag.Name = name;
            return View("Lobby", new LobbyModel() { Data = LobbyManager.GetPlayersSimpleObj() });
        }

        public ActionResult StartGame()
        {

            return View("Game",new GameModel());
        }


        private string GenerateLocationDivs()
        {

        }
    }
}