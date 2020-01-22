using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dracula.Core;
using Dracula.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dracula.Web
{
	public class HomeController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View(new LoginModel());
		}

		[Route("/Route/Lobby")]
		public IActionResult Lobby()
		{
			return View();
		}

		[Route("/Route/Game")]
		public IActionResult Game()
		{
			return View();
		}

		public IActionResult LoginUser(string name)
		{
			if (string.IsNullOrEmpty(name) || !LobbyManager.IsNickAvailable(name))
			{
				return View("Index", new LoginModel() {IsError = true});
			}

			LobbyManager.AddPlayer(name);

			ViewBag.Name = name;
			return View("Lobby");
		}

		public IActionResult StartGame()
		{
			return View("Game");
		}
	}
}
