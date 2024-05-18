using Collectify.Domain.Entities;

namespace Collectify.Application.Identity.Services;

public interface IAccessTokenGeneratorService
{
    string GetToken(User user);
}