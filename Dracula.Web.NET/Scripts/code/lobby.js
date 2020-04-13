var hub;
$(document).ready(function () {
	console.log("lobby.js when document ready");

	appSettings.PlayerListTable = document.getElementById("PlayerList");
	hub = $.connection.lobbyHub; //set hub with the server side class 

	$.connection.hub.start()
		.then(function () {
			hub.server.playerJoinedLobbySend()
				.catch(function (err) {
					console.info(err);
				});

			console.log("connectionStart");
			parentBox = document.getElementById("parentBox");
		})
		.done(function () {
			console.log('Now connected, connection ID=' + $.connection.hub.id);
		})
		.fail(function () {
			console.log('Could not Connect!');
		});

	//client recieve events

	hub.client.LobbyPlayerListRefresh = function (playerListJson) {
		var playerList = JSON.parse(playerListJson);

		playerList.forEach(function (player) {
			var row = $("#PlayerList>tr.playerRow[data-player-name='" + player.Name + "']");
			if (row === undefined || row.length === 0) {
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

		$("#PlayerList input[type='checkbox']").change(function (e) {
			var selectedChar = $(e.currentTarget).data("character");
			var selectedPlayer = $(e.currentTarget).data("playerName");
			$("#PlayerList").find("input[data-character='" + selectedChar + "']").prop("checked", false)
			$("#PlayerList").find("input[data-player-name='" + selectedPlayer + "']").prop("checked", false)
			$(e.currentTarget).prop("checked", true);
			hub.invoke("PlayerDataChangedSend", JSON.stringify(getPlayerDataJSON()))
				.catch(function (err) {
					console.info(err);
				});
		});
	}

	hub.client.startGame = function () { };

	hub.client.MoveReceive = function (players) {

		var pl = JSON.parse(players);
		Array.prototype.forEach.call(parentBox.children, child => {
			parentBox.removeChild(child);
		});

		pl.forEach(function (p) {
			var d = document.createElement("div");

			d.textContent = p.Name;
			$(d).css("top", p.TopOffset);
			$(d).css("left", p.LeftOffset);
			$(d).css("position", "absolute");
			parentBox.appendChild(d);
		});
	};
});

var parentBox;
var appSettings = {
	playerName: "",
	PlayerListTable: null
};

function GenerateLobbyRow(p) {
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

function up() { hub.server.moveSend("up", appSettings.playerName); }
function down() { hub.server.moveSend("down", appSettings.playerName); }
function left() { hub.server.moveSend("left", appSettings.playerName); }
function right() { hub.server.moveSend("right", appSettings.playerName); }

function KickPlayerFromLobby() {
	//connection.
}

function getPlayerDataJSON() {
	var playerData = {};

	$("#PlayerList>tr.playerRow").each(function (i, row) {
		var rowData = {};
		$(row).find("*[data-character]").each(function (j, input) {
			//rowdata
			rowData[$(input).data("character")] = $(input).prop("checked");
		});
		playerData[$(row).data("playerName")] = rowData
	});

	return playerData;
}


