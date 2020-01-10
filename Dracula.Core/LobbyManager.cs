using System;
using System.Collections.Generic;
using System.Linq;

namespace Dracula.Core
{
	public static class LobbyManager
	{
		public static List<Player> Players;

		public static void Init()
		{
			Players = new List<Player>();
		}

		public static void AddPlayer(string name)
		{
			Players.Add(new Player(){Name = name});
		}

		public static bool IsNickAvailable(string nick)
		{
			return Players.All(_ => _.Name != nick);
		}

		public static object[] GetPlayersSimpleObj()
		{
			throw  new NotImplementedException();
		}
	}
}