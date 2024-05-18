using System.Linq.Expressions;
using Collectify.Domain.Entities;
using Collectify.Persistence.DataContexts;
using Collectify.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Collectify.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext) : 
    EntityRepositoryBase<AppDbContext, User>(appDbContext), 
    IUserRepository
{
    public new IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default, 
        bool asNoTracking = false)
    {
        return base
            .Get(predicate, asNoTracking)
            .OrderBy(user => user.UserName);
    }

    public async ValueTask<bool> UserExists(
        string emailAddress,
        CancellationToken cancellationToken = default)
    {
        return await base
            .Get(asNoTracking: true)
            .AnyAsync(user => user.EmailAddress == emailAddress, cancellationToken);
    }
    
    public async ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await base
            .Get(user => user.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public new async ValueTask<User> CreateAsync(
        User user, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return await base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public new async ValueTask<User> UpdateAsync(
        User user, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return await base.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public new async ValueTask<User> DeleteAsync(
        User user,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        return await base.DeleteAsync(user, saveChanges, cancellationToken);
    }
}