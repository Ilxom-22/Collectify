using Collectify.Application.Identity.Models.Dtos;
using Collectify.Domain.Entities;

namespace Collectify.Application.Identity.Services;

public interface IAuthService
{
    ValueTask<User> GetCurrentUser(
        CancellationToken cancellationToken = default);
    
    ValueTask<User> SignUpAsync(
        SignUpDetails details, 
        CancellationToken cancellationToken = default);

    ValueTask<AccessToken> SignInAsync(
        SignInDetails details, 
        CancellationToken cancellationToken = default);

    public ValueTask LogOutAsync(
        AccessToken accessToken,
        CancellationToken cancellationToken = default);
}