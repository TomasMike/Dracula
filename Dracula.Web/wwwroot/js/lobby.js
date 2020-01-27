$(document).ready(function ()
{
	console.log("lobby.js when document ready");



	appSettings.PlayerListTable = document.getElementById("PlayerList");
});

"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();
var parentBox;
console.log("lobby.js");
var appSettings = {
	playerName: "",
	PlayerListTable:null
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

connection.on("LobbyPlayerListRefresh", function (players) 
{

	debugger;
	var pl = JSON.parse(players);
	//update player list
	Array.prototype.forEach.call($(appSettings.PlayerListTable).find("tr.playerRow"), child =>
	{
		appSettings.PlayerListTable.removeChild(child);
	});

	pl.forEach(function (p) 
	{
		var r = GenerateLobbyRow(p);
		$(appSettings.PlayerListTable).append(r);
	});
	
	$("#PlayerList input[type='checkbox']").change(function ()
	{
		connection.invoke("PlayerDataChangedSend")
			.catch(function (err) 
			{
				console.info(err);
			});
	});

});

function GenerateLobbyRow(p)
{
	var row = document.createElement("tr");
	$(row).addClass("playerRow");
	var name = $("<td></td>").text(p.Name);
	var b = document.createElement("button");
	$(b).attr("onclick", "console.log(\"" + p.Name + "\");");
	$(b).text("Kick");
	$(row).append(
		name,
		$("<td></td>").append(b),
		"<td><input type='checkbox' name=\"" + p.Name + "_" + "Dracula\"" + (p.SelectedCharacter === 0 ? "checked" : "") +"'></input></td>",
		"<td><input type='checkbox' name=\"" + p.Name + "_" + "LordGodalming\"" + (p.SelectedCharacter === 1 ? "checked" : "") +"'></input></td>",
		"<td><input type='checkbox' name=\"" + p.Name + "_" + "DrJohnStewart\"" + (p.SelectedCharacter === 2 ? "checked" : "") +"'></input></td>",
		"<td><input type='checkbox' name=\"" + p.Name + "_" + "VanHelsing\"" + (p.SelectedCharacter === 3 ? "checked" : "") +"'></input></td>",
		"<td><input type='checkbox' name=\"" + p.Name + "_" + "MinaHarker\"" + (p.SelectedCharacter === 4 ? "checked" : "") +"'></input></td>"

		);
	return row;
}

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
function down() {connection.invoke("MoveSend", "down", appSettings.playerName);}
function left() { connection.invoke("MoveSend", "left", appSettings.playerName);}
function right() { connection.invoke("MoveSend", "right", appSettings.playerName);}

function KickPlayerFromLobby()
{
	//connection.
}


