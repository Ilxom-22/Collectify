using Collectify.Application.Identity.Services;
using Collectify.Domain.Entities;
using Collectify.Domain.Pagination;
using Collectify.Persistence.Extensions;

namespace Collectify.Infrastructure.Identity.Services;

public class AccountService(IUserService userService) : IAccountService
{
    public IQueryable<User> GetUsers(PaginationOptions paginationOptions)
    {
        var users = userService.Get().ApplyPagination(paginationOptions);

        return users;
    }
}