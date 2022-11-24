namespace AkinEmailChatApp.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<Message> Messages { get; set; } = null!;
}