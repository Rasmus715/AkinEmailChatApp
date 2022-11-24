namespace AkinEmailChatApp.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid RecipientId { get; set; } 
    public User Sender { get; set; } = null!;
    public string Title { get; set; } = "No Title";
    public string Body { get; set; } = "No Body";
    public DateTime TimeSent { get; set; }
}