using AkinEmailChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkinEmailChatApp.Controllers;

public class MessageController: Controller
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMessagesService _messagesService;
    private readonly IAccountService _accountService;

    public MessageController(ILogger<MessageController> logger, IMessagesService messagesService, IAccountService accountService)
    {
        _logger = logger;
        _messagesService = messagesService;
        _accountService = accountService;
    }
    
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var messages = await _messagesService.GetMessagesForUser(User.Identity?.Name!);
        Console.WriteLine(messages.Count);
        return View(messages);
    }
    
    [Authorize]
    public async Task<JsonResult> AutoCompleteUsers(string term)
    {
        var users = await _accountService.GetAllUsers(term);
        return new JsonResult(users);
    }
}