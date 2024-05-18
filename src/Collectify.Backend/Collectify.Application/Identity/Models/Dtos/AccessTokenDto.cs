namespace Collectify.Application.Identity.Models.Dtos;

public class AccessTokenDto
{
    public string Token { get; init; } = default!;

    public DateTimeOffset ExpiryTime { get; init; }

    public Guid UserId { get; init; }
}