using Collectify.Domain.Common;
using Collectify.Domain.Enums;

namespace Collectify.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;
    
    public string PasswordHash { get; set; } = default!;

    public Role Role { get; set; } = Role.User;

    public AccountStatus Status { get; set; } = AccountStatus.Active;
    
    public AccessToken? AccessToken { get; set; }
}