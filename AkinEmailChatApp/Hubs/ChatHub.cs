using System.Globalization;
using AkinEmailChatApp.Models;
using AkinEmailChatApp.Services;
using AkinEmailChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AkinEmailChatApp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IMessagesService _messagesService;

    public ChatHub(IMessagesService messagesService)
    {
        _messagesService = messagesService;
    }

    public override Task OnConnectedAsync()
    {
        Groups.AddToGroupAsync(Context.ConnectionId, Context.User!.Identity?.Name!);
        return base.OnConnectedAsync();
    }
    
    public async Task SendMessage(Message message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
    
    public async Task SendMessageToGroup(MessageViewModel message)
    {
        Console.WriteLine("sender: " + message.Sender);
        Console.WriteLine("receiver: " + message.Recipient);
        Console.WriteLine("message title: " + message.Title);
        Console.WriteLine("message body: " + message.Body);
        Console.WriteLine("message ID: ");
        message = await _messagesService.SendMessage(message);
        await Clients.Group(message.Recipient).SendAsync("ReceiveMessage", 
            message.Sender, 
            DateTime.Now.ToString(CultureInfo.InvariantCulture), 
            message.Title, 
            message.Body, 
            message.Id);
    }
}