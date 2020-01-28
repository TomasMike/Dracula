$(document).ready(function ()
{
	console.log("lobby.js when document ready");



	appSettings.PlayerListTable = document.getElementById("PlayerList");
});

"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();
var MoveDivParentBox;

var appSettings = {
	playerName: "",
	PlayerListTable:null
};
connection.start().then(function () 
{
	console.log("connectionStart");

	MoveDivParentBox = document.getElementById("parentBox");

	connection.invoke("PlayerJoinedLobbySend")
		.catch(function (err) 
		{
			console.info(err);
		});
}).catch(function (err)
{
	return console.error(err.toString());
});

connection.on("LobbyPlayerListRefresh", function (playerListJson) 
{



	debugger;
	var playerList = JSON.parse(playerListJson);
	//update player list
	//Array.prototype.forEach.call($(appSettings.PlayerListTable).find("tr.playerRow"), child =>
	//{
	//	appSettings.PlayerListTable.removeChild(child);
	//});

	playerList.forEach(function (player) {
		var row = $("#PlayerList>tr.playerRow[data-playerName='" + player.Name + "']")[0];
		if (row === undefined) {
			var r = GenerateLobbyRow(player);
			$(appSettings.PlayerListTable).append(r);
		} else {
			
			$(row).find("*[data-character='dracula']")
		}
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
	$(row).data("playerName", p.Name);
	var name = $("<td></td>").text(p.Name);
	var b = document.createElement("button");
	$(b).attr("onclick", "console.log(\"" + p.Name + "\");");
	$(b).text("Kick");
	$(row).append(
		name,
		$("<td></td>").append(b),
		"<td><input type='checkbox' data-playerName ='" + p.Name + "' data-character='dracula' " + (p.SelectedCharacter === 'dracula' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-playerName ='" + p.Name + "' data-character='lordGodalming' " + (p.SelectedCharacter === 'lordGodalming' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-playerName ='" + p.Name + "' data-character='drJohnStewart' " + (p.SelectedCharacter === 'drJohnStewart' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-playerName ='" + p.Name + "' data-character='vanHelsing' " + (p.SelectedCharacter === 'vanHelsing' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-playerName ='" + p.Name + "' data-character='minaHarker' " + (p.SelectedCharacter === 'minaHarker' ? "checked" : "") + " ></input></td>"
	);
	return row;
}



function up() {  connection.invoke("MoveSend", "up", appSettings.playerName);}
function down() {connection.invoke("MoveSend", "down", appSettings.playerName);}
function left() { connection.invoke("MoveSend", "left", appSettings.playerName);}
function right() { connection.invoke("MoveSend", "right", appSettings.playerName);}

function KickPlayerFromLobby()
{
	//connection.
}

connection.on("MoveReceive", function (players)
{

	var pl = JSON.parse(players);
	//Array.prototype.forEach.call(parentBox.children, child =>
	//{
	//	parentBox.removeChild(child);
	//});

	pl.forEach(function (p)
	{
		var d = document.createElement("div");

		d.textContent = p.Name;
		$(d).css("top", p.TopOffset);
		$(d).css("left", p.LeftOffset);
		$(d).css("position", "absolute");
		MoveDivParentBox.appendChild(d);
	});
});