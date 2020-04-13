using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Dracula.Web.Hubs
{
	[HubName("lobbyHub")]
	public class LobbyHub : Hub
	{
		public LobbyHub() : base()
		{

		}

		//zavolane ked niekto posle msg
		public async Task SendMessage(string user, string message)
		{
			DataStorage.ChatHistory.Add(new { user = user, message = message });
			//server povie vsetkym napojenim ze dostal message a nech si ju zobrazia
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		[HubMethodName("playerJoinedLobbySend")]
		public async Task PlayerJoinedLobbySend()
		{
			RefreshPlayerList();
			//await Clients.All.SendAsync("ReceiveMessage");

		}

		public async Task MoveSend(string direction, string name)
		{
			var player = LobbyManager.GetPlayerByName(name);

			switch (direction)
			{
				case "up":
					player.TopOffset -= 10; break;
				case "down":
					player.TopOffset += 10; break;
				case "left":
					player.LeftOffset += 10; break;
				case "right":
					player.LeftOffset -= 10; break;
			}

			await Clients.All.SendAsync("MoveReceive", JsonConvert.SerializeObject(LobbyManager.Players));
		}

		public async Task KickPlayer(string name)
		{
			LobbyManager.Players.RemoveAll(_ => _.Name == name);
			//Clients.Caller.
			//RefreshPlayerList();
		}


		private void RefreshPlayerList()
		{
			Clients.All.LobbyPlayerListRefresh(LobbyManager.GetPlayersSimpleObj());
		}

		public async Task PlayerDataChangedSend(string data)
		{
			var jData = (JObject)JsonConvert.DeserializeObject(data);
			foreach (var item in jData.Children())
			{
				//LobbyManager.Players.FirstOrDefault
			}
		}
	}
}