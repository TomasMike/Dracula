"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.start().then(function ()
{
	console.log("connection started.");
	

}).catch(function (err)
{
	return console.error(err.toString());
});

connection.on("PlayerJoinedLobbyTEST", function (players)
{
	console.log(players);
	//writeMessage(user, message);
});

function test()
{
	var x = connection.invoke("PlayerJoinedLobby").catch(function (err)
	{
		console.info(err);
	});
}

//document.getElementById("sendButton").addEventListener("click", function (event)
//{
//	console.log("send");

//	var user = document.getElementById("userInput").value;
//	var message = document.getElementById("messageInput").value;
//	connection.invoke("SendMessage", user, message).catch(function (err)
//	{
//		return console.error(err.toString());
//	});
//	event.preventDefault();
//});

//function writeMessage(user, message) 
//{
//	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//	var encodedMsg = user + " says " + msg;
//	var li = document.createElement("li");
//	li.textContent = encodedMsg;
//	document.getElementById("messagesList").appendChild(li);
//}