using AutoMapper;
using Collectify.Application.Identity.Models.Dtos;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectify.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AccountsController(IMapper mapper, IAccountService accountService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers([FromQuery] PaginationOptions paginationOptions)
    {
        var users = accountService.GetUsers(paginationOptions);

        return Ok(mapper.Map<IEnumerable<UserDto>>(users));
    }
}