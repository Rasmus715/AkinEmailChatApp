"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (sender, timestamp, title, body) 
{
    var newRow = document.createElement("tr");
    newRow.setAttribute("data-clickable",'');
    var timestampCell = document.createElement("td");
    var senderCell = document.createElement("td");
    var titleCell = document.createElement("td");
    
    var bodyRow = document.createElement("tr");
    
    bodyRow.setAttribute("style","height:auto;display:none;");
    var bodyCell = document.createElement("td");
    bodyCell.setAttribute("colspan","3");
    bodyCell.setAttribute("style","border:none;height:auto;");
    
    var bodyLabel = document.createElement("label");
    bodyLabel.setAttribute("class","form-control");
    bodyLabel.setAttribute("style","height:auto;");
    bodyLabel.textContent = body;
    
    bodyCell.append(bodyLabel);
    bodyRow.append(bodyCell)
    document.getElementById("dataRows").prepend(bodyRow);
    
    timestampCell.innerHTML = timestamp;
    senderCell.innerHTML = sender;
    titleCell.innerHTML = title;
    newRow.append(timestampCell);
    newRow.append(senderCell);
    newRow.append(titleCell);
    
    document.getElementById("dataRows").prepend(newRow);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var sender = document.getElementById("senderInput");
    var receiver = document.getElementById("receiverInput");
    var title = document.getElementById("titleInput");
    var message = document.getElementById("messageInput");

        var msg = {
            Sender : sender.value,
            Recipient : receiver.value,
            Title : title.value,
            Body : message.value
        };

        connection.invoke("SendMessageToGroup", msg)
            .catch(function (err) {
                return console.error(err.toString());
            });
    document.getElementById("status").innerHTML = "Your message sent successfully!";
    sender.value = "";
    receiver.value = "";
    title.value = "";
    message.value = "";
    event.preventDefault();
});