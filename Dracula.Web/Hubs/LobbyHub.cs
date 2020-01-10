using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Dracula.Core;

namespace Dracula.Web.Hubs
{
	public class LobbyHub : Hub
	{
		//zavolane ked niekto posle msg
		public async Task SendMessage(string user, string message)
		{
			DataStorage.ChatHistory.Add(new { user=user,message=message});
			//server povie vsetkym napojenim ze dostal message a nech si ju zobrazia
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public async Task PlayerJoinedLobby()
		{
			object x = null;
			await Clients.All.SendAsync("PlayerJoinedLobbyTEST", x);
		}
	}
}