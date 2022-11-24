using AkinEmailChatApp.Data;
using AkinEmailChatApp.Models;
using AkinEmailChatApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AkinEmailChatApp.Services;

public interface IAccountService
{
    Task<User> GetOrCreateUser(LoginViewModel vm);
    Task<List<string>> GetAllUsers(string term);
}

public class AccountService : IAccountService
{
    private readonly AppDbContext _db;

    public AccountService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> GetOrCreateUser(LoginViewModel vm)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == vm.Name);
        
        if (user != null) 
            return user;
        
        user = new User
        {
            Id = default,
            Name = vm.Name,
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<List<string>> GetAllUsers(string term)
    {
        return await _db.Users.Where(c=>c.Name.Contains(term)).Select(c => new string(c.Name)).ToListAsync();
    }
}
