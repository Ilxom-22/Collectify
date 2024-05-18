using Collectify.Domain.Entities;

namespace Collectify.Persistence.Repositories.Interfaces;

public interface IAccessTokenRepository
{
    ValueTask<AccessToken?> GetByUserIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);
    
    ValueTask<AccessToken> CreateAsync(
        AccessToken token,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<AccessToken> DeleteAsync(
        AccessToken token,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}