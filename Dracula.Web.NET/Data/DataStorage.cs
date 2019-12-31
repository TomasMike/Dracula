using Dracula.Web.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dracula.Web.NET.Data
{
	public static class DataStorage
	{
		private static List<User> Users;

		public static void Init()
		{
			Users = new List<User>();
		}

		public static List<User> GetUsers() => Users;

		public static void AddUser(User user)
		{
			Users.Add(user);
		}

		public static User GetUserBySessionID(string sessionID = null)
		{
			return Users.FirstOrDefault(_ => _.SessionID == (sessionID ?? HttpContext.Current.Session.SessionID));
		}
	}
}