namespace Collectify.Application.Identity.Models.Dtos;

public class SignUpDetails
{
    public string UserName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}