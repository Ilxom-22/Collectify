using System.Linq.Expressions;
using Collectify.Domain.Entities;

namespace Collectify.Application.Identity.Services;

public interface IUserService
{
    IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default, 
        bool asNoTracking = false);

    ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);
    
    ValueTask<User?> GetByEmail(string emailAddress);

    ValueTask<bool> UserExists(string emailAddress, CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}