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
	var playerList = JSON.parse(playerListJson);
	
	playerList.forEach(function (player) {
		debugger;
		var row = $("#PlayerList>tr.playerRow[data-player-name='" + player.Name + "']");
		if (row === undefined || row.length === 0) 
		{
			var r = GenerateLobbyRow(player);
			$(appSettings.PlayerListTable).append(r);
		}
		else {
			$(row).find("*[data-character='dracula']").prop("checked", player.SelectedCharacter === "dracula");
			$(row).find("*[data-character='lordGodalming']").prop("checked", player.SelectedCharacter === "lordGodalming");
			$(row).find("*[data-character='drJohnStewart']").prop("checked", player.SelectedCharacter === "drJohnStewart");
			$(row).find("*[data-character='vanHelsing']").prop("checked", player.SelectedCharacter === "vanHelsing");
			$(row).find("*[data-character='minaHarker']").prop("checked", player.SelectedCharacter === "minaHarker");
		}
	});
	
	$("#PlayerList input[type='checkbox']").change(function (e) 
	{
		debugger;
		connection.invoke("PlayerDataChangedSend", appSettings.PlayerListTable.outerHTML)
			.catch(function (err) 
			{
				console.info(err);
			});
	});

});

function GenerateLobbyRow(p)
{
	var row = document.createElement("tr");
	row.dataset.playerName = p.Name;
	$(row).addClass("playerRow");
	var name = $("<td></td>").text(p.Name);
	var b = document.createElement("button");
	$(b).attr("onclick", "console.log(\"" + p.Name + "\");");
	$(b).text("Kick");
	$(row).append(
		name,
		$("<td></td>").append(b),
		"<td><input type='checkbox' data-player-name ='" + p.Name + "' data-character='dracula' " + (p.SelectedCharacter === 'dracula' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-player-name ='" + p.Name + "' data-character='lordGodalming' " + (p.SelectedCharacter === 'lordGodalming' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-player-name ='" + p.Name + "' data-character='drJohnStewart' " + (p.SelectedCharacter === 'drJohnStewart' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-player-name ='" + p.Name + "' data-character='vanHelsing' " + (p.SelectedCharacter === 'vanHelsing' ? "checked" : "") + " ></input></td>",
		"<td><input type='checkbox' data-player-name ='" + p.Name + "' data-character='minaHarker' " + (p.SelectedCharacter === 'minaHarker' ? "checked" : "") + " ></input></td>"
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


