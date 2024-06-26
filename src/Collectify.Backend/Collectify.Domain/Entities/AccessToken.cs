using Collectify.Domain.Common;

namespace Collectify.Domain.Entities;

public class AccessToken : IEntity
{
    Guid IEntity.Id
    {
        get => UserId;
        set => UserId = value;
    }

    public string Token { get; set; } = default!;

    public DateTimeOffset ExpiryTime { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}