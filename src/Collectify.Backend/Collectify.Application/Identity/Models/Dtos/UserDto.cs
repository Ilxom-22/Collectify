using Collectify.Domain.Enums;

namespace Collectify.Application.Identity.Models.Dtos;

public class UserDto
{
    public Guid Id { get; init; }

    public string UserName { get; init; } = default!;

    public string EmailAddress { get; init; } = default!;

    public Role Role { get; set; }

    public AccountStatus Status { get; init; }
}