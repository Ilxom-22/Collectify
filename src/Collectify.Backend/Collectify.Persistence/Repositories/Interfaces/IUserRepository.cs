using System.Linq.Expressions;
using Collectify.Domain.Entities;

namespace Collectify.Persistence.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default,
        bool asNoTracking = false);
    
    ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default);

    ValueTask<bool> UserExists(
        string emailAddress,
        CancellationToken cancellationToken = default);

    ValueTask<bool> IsUserNameTaken(
        string userName,
        CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(
        User user, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(
        User user, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(
        User user, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default);
}