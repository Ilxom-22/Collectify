using Collectify.Domain.Entities;
using Collectify.Domain.Pagination;

namespace Collectify.Application.Identity.Services;

public interface IAccountService
{
    IQueryable<User> GetUsers(PaginationOptions paginationOptions);
}