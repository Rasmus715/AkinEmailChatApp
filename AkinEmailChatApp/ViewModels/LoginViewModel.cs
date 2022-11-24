using System.ComponentModel.DataAnnotations;

namespace AkinEmailChatApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please, provide a name")]
    public string Name { get; init; } = null!;
}