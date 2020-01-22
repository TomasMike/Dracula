using System;
using System.Collections.Generic;
using System.Linq;
using Dracula.Core;
using Dracula.Web.Hubs;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Dracula.Web
{
	public static class LobbyManager
	{
		public static List<Player> Players;
		private static Random r = new Random();
		public static LobbyHub hub;
		public static void Init()
		{
			Players = new List<Player>();
		}

		public static void AddPlayer(string name)
		{
			Players.Add(new Player(){Name = name, LeftOffset = r.Next(100),TopOffset = r.Next(100)});
		}

		public static bool IsNickAvailable(string nick)
		{
			return Players.All(_ => _.Name != nick);
		}

		public static string GetPlayersSimpleObj()
		{
			return JsonConvert.SerializeObject(Players);
		}

		public static Player GetPlayerByName(string name)
		{
			return Players.FirstOrDefault(_ => _.Name == name);
		}
	}
}