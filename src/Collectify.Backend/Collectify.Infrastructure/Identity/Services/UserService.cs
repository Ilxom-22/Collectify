using System.Linq.Expressions;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Entities;
using Collectify.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Collectify.Infrastructure.Identity.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
     public async ValueTask<bool> UserExists(
        string emailAddress,
        CancellationToken cancellationToken = default)
    {
        return await userRepository.UserExists(emailAddress, cancellationToken);
    }

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default,
        bool asNoTracking = false)
    {
        return userRepository.Get(predicate, asNoTracking);
    }
    
    public async ValueTask<User?> GetByEmail(string emailAddress)
    {
        var foundUser = await userRepository
            .Get(user => user.EmailAddress == emailAddress)
            .FirstOrDefaultAsync();

        return foundUser;
    }

    public async ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public async ValueTask<User> CreateAsync(
        User user, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        return await userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }
}