using Collectify.Application.Identity.Brokers;
using Collectify.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Collectify.Infrastructure.Identity.Brokers;

public class RequestContextProvider(IHttpContextAccessor httpContextAccessor) : IRequestContextProvider
{
    public Guid? GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)?.Value;

        return userIdClaim is not null ? Guid.Parse(userIdClaim) : null;
    }
}