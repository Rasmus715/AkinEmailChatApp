namespace AkinEmailChatApp.ViewModels;

public class MessageViewModel
{
    public string Id { get; set; } = null!;
    public string Sender { get; init; } = null!;
    public string Recipient { get; init; } = null!;
    public string Title { get; set; } = "No Title";
    public string Body { get; set; } = "No Body";
    public string Timestamp { get; set; } = null!;
}