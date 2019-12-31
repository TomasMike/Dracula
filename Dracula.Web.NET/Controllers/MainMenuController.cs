using Dracula.Web.NET.Data;
using Dracula.Web.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dracula.Web.NET.Controllers
{
	public class MainMenuController : Controller
	{
		[HttpPost]
		public ActionResult CreateUser(FormCollection fc)
		{
			var u = new User()
			{
				Name = fc["Name"],
				SessionID =  HttpContext.Session.SessionID
			};
			DataStorage.AddUser(u);
			return RedirectToAction("Index", "MainMenu", u);
		}

		public ActionResult Index(User u)
		{
			return View(u);
		}
	}
}