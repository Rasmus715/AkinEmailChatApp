using System.Globalization;
using AkinEmailChatApp.Data;
using AkinEmailChatApp.Models;
using AkinEmailChatApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AkinEmailChatApp.Services;

public interface IMessagesService
{
    Task<List<MessageViewModel>> GetMessagesForUser(string userName);
    Task<MessageViewModel> SendMessage(MessageViewModel message);
}

public class MessageService : IMessagesService
{
    private readonly AppDbContext _db;
    private readonly IAccountService _accountService;
    
    public MessageService(AppDbContext db, IAccountService accountService)
    {
        _db = db;
        _accountService = accountService;
    }
    
    public async Task<List<MessageViewModel>> GetMessagesForUser(string userName)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == userName);
        return await _db.Messages.Where(m => m.RecipientId == user!.Id).Include(c => c.Sender).OrderByDescending(c=>c.TimeSent).Select(c =>
            new MessageViewModel
            {
                Id = c.Id.ToString(),
                Sender = c.Sender.Name,
                Recipient = user!.Name,
                Title = c.Title,
                Body = c.Body,
                Timestamp = c.TimeSent.ToString(CultureInfo.InvariantCulture)
            }).ToListAsync();
    }

    public async Task<MessageViewModel> SendMessage(MessageViewModel message)
    {
        var recipient = await _accountService.GetOrCreateUser(new LoginViewModel
        {
            Name = message.Recipient
        });
        
        var sender = await _accountService.GetOrCreateUser(new LoginViewModel
        {
            Name = message.Sender
        });

        if (message.Title.Equals(""))
            message.Title = "No Title";
        
        if (message.Body.Equals(""))
            message.Body = "No Body";
        
        await _db.Messages.AddAsync(new Message
        {
            RecipientId = recipient.Id,
            Sender = sender,
            Title = message.Title,
            Body = message.Body,
            TimeSent = DateTime.Now
        });
        
        await _db.SaveChangesAsync();
        return message;
    }
}