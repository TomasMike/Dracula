import $ from "jquery";
import signalR from "wwwroot/"

var appSettings = {
 PlayerListTable : document.getElementById("PlayerList")
};
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();
