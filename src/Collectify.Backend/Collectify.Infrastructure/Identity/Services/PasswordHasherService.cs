using Collectify.Application.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace Collectify.Infrastructure.Identity.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public bool ValidatePassword(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
}