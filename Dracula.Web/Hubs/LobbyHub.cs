using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Dracula.Core;
using Newtonsoft.Json;

namespace Dracula.Web.Hubs
{
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

		public async Task PlayerJoinedLobbySend()
		{
			RefreshPlayerList();
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


		private async void RefreshPlayerList()
		{
			await Clients.All.SendAsync("LobbyPlayerListRefresh", LobbyManager.GetPlayersSimpleObj());
		}


		public override Task OnDisconnectedAsync(Exception exception)
		{
			throw new NotImplementedException();
		}

		public async Task PlayerDataChangedSend(string data)
		{
			var x = new System.Xml.XmlDocument();
			try
			{
				x.LoadXml(data);
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}