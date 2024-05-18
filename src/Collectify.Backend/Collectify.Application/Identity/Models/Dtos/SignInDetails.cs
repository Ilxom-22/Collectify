namespace Collectify.Application.Identity.Models.Dtos;

public class SignInDetails
{
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}