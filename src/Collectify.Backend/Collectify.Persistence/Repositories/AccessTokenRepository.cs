using Collectify.Domain.Entities;
using Collectify.Persistence.DataContexts;
using Collectify.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Collectify.Persistence.Repositories;

public class AccessTokenRepository(AppDbContext appDbContext) :
    EntityRepositoryBase<AppDbContext, AccessToken>(appDbContext), 
    IAccessTokenRepository
{
    public async ValueTask<AccessToken?> GetByUserIdAsync(
        Guid userId, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await base
            .Get(token => token.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public new async ValueTask<AccessToken> CreateAsync(
        AccessToken token, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        return await base.CreateAsync(token, saveChanges, cancellationToken);
    }

    public new async ValueTask<AccessToken> DeleteAsync(
        AccessToken token, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        return await base.DeleteAsync(token, saveChanges, cancellationToken);
    }
}