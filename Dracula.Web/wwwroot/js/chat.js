//"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

////Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

//connection.on("ReceiveMessage", function (user, message)
//{
//	console.log("receive");
//	writeMessage(user, message);
//});

//connection.start().then(function ()
//{
//	console.log("start");
//	debugger;
//	var x = connection.invoke("GetHistory").catch(function (err)
//	{
//		return console.error(err.toString());
//	}).then(function (s)
//	{
//		s.forEach(function (i) { writeMessage(i.user, i.message) });
//	});

//	document.getElementById("sendButton").disabled = false;
//}).catch(function (err)
//{
//	return console.error(err.toString());
//});

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