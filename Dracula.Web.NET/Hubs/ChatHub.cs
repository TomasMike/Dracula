﻿using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace Dracula.Web.Hubs
{
	[HubName("ChatHub")]
	public class ChatHub : Hub
	{
		//zavolane ked niekto posle msg
		public async Task SendMessage(string user, string message)
		{
			DataStorage.ChatHistory.Add(new { user=user,message=message});
			//server povie vsetkym napojenim ze dostal message a nech si ju zobrazia
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public  object[] GetHistory()
		{
			return DataStorage.ChatHistory.ToArray();
		}
	}
}