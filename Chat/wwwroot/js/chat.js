"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
var messagesList = document.getElementById("messagesList");    
messagesList.scrollTop = messagesList.scrollHeight


connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says: " + msg;
    var bold = document.createElement("b");
    bold.textContent = user;
    var span = document.createElement("span");
    span.textContent = " says: " + msg;
    var li = document.createElement("li");
    li.appendChild(bold);
    li.appendChild(span);
    
    var extraMessages = messagesList.children.length - 49
    if (extraMessages > 0) {
        for (var i = 0; i < extraMessages; i++) {
            messagesList.removeChild(messagesList.children[0]);
        }
    }
    messagesList.appendChild(li);
    messagesList.scrollTop = messagesList.scrollHeight
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageInput").addEventListener("keyup", function (event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        document.getElementById("sendButton").click();
    }
});

document.getElementById("sendButton").addEventListener("click", function (event) {    
    var messageInput = document.getElementById("messageInput");
    connection.invoke("SendMessage", messageInput.value).then(function () {
        messageInput.value = "";
        console.log("aqui");
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});