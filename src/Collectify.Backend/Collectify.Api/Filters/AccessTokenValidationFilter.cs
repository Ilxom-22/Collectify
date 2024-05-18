using System.Security.Authentication;
using Collectify.Application.Identity.Brokers;
using Collectify.Application.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Collectify.Api.Filters;

public class AccessTokenValidationFilter(
    IRequestContextProvider contextProvider, 
    IAccessTokenService accessTokenService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var isAuthorized = context.ActionDescriptor.EndpointMetadata
            .Any(endpointMetadata => endpointMetadata is AuthorizeAttribute);

        if (isAuthorized)
        {
            var accessTokenId = contextProvider.GetUserId();

            if (accessTokenId is null)
                throw new AuthenticationException("Access Token Validation filter!");

            _ = await accessTokenService.GetByUserIdAsync(
                (Guid)accessTokenId,
                true);
        }

        await next();
    }
}