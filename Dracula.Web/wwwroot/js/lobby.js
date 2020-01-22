$(document).ready(function ()
{
	console.log("lobby.js when document ready");

	$("#PlayerList checkbox").change(function ()
	{
		debugger;
	});
});

"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();
var parentBox;
console.log("lobby.js");
var appSettings = {
	playerName:""
};
connection.start().then(function () 
{
	console.log("connectionStart");

	parentBox = document.getElementById("parentBox");

	connection.invoke("PlayerJoinedLobbySend")
		.catch(function (err) 
		{
			console.info(err);
		});



}).catch(function (err)
{
	return console.error(err.toString());
});


connection.on("SetPlayerName", function (name) { playerName = name; });

connection.on("LobbyPlayerListRefresh", function (players) 
{
	var pl = JSON.parse(players);
	var table = document.getElementById("PlayerList");
	//update player list
	Array.prototype.forEach.call(table.children, child =>
	{
		table.removeChild(child);
	});

	pl.forEach(function (p) 
	{
		var r = GenerateLobbyRow(p);
		$(table).append(r);
	});
	


});

connection.on("MoveReceive", function (players)
{
	
	var pl = JSON.parse(players);
	Array.prototype.forEach.call(parentBox.children, child =>
	{
		parentBox.removeChild(child);
	});

	pl.forEach(function (p)
	{
		var d = document.createElement("div");
		
		d.textContent = p.Name;
		$(d).css("top", p.TopOffset);
		$(d).css("left", p.LeftOffset);
		$(d).css("position", "absolute");
		parentBox.appendChild(d);
	});
});

function up() {  connection.invoke("MoveSend", "up", appSettings.playerName);}
function down() {
	
	connection.invoke("MoveSend", "down", appSettings.playerName);
}
function left() { connection.invoke("MoveSend", "left", appSettings.playerName);}
function right() { connection.invoke("MoveSend", "right", appSettings.playerName);}

function KickPlayerFromLobby()
{
	connection.


}

function GenerateLobbyRow(p) {
	var row = document.createElement("tr");
	var name = $("<td></td>").text(p.Name);
	var b = document.createElement("button");
	$(b).attr("onclick", "console.log(\"" + p.Name + "\");");
	$(b).text("Kick");
	$(row).append(name, $("<td></td>").append(b),"<td><input type='checkbox' name=\"" + p.Name + "_"+"Dracula\"/></td>");
	return row;
}

