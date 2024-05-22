using AutoMapper;
using Collectify.Application.Identity.Models.Dtos;
using Collectify.Application.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectify.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController( 
    IMapper mapper,
    IAuthService authService) : ControllerBase
{
    [Authorize]
    [HttpGet("Me")]
    public async ValueTask<IActionResult> GetCurrentUser(CancellationToken cancellationToken = default)
    {
        var currentUser = await authService.GetCurrentUser(cancellationToken);

        return Ok(mapper.Map<UserDto>(currentUser));
    }
    
    [HttpPost("Sign-Up")]
    public async ValueTask<IActionResult> SignUpAsync(
        [FromBody] SignUpDetails details,
        CancellationToken cancellationToken = default)
    {
        var user = await authService.SignUpAsync(details, cancellationToken);
        var result = mapper.Map<UserDto>(user);

        return Ok(result);
    }
    
    [HttpPost("Sign-In")]
    public async ValueTask<IActionResult> SignInAsync(
        [FromBody] SignInDetails details,
        CancellationToken cancellationToken = default)
    {
        var accessToken = await authService.SignInAsync(details, cancellationToken);

        return Ok(accessToken.Token);
    }
    
    [Authorize]
    [HttpPost("Log-Out")]
    public async ValueTask<IActionResult> LogOutAsync(
        CancellationToken cancellationToken = default)
    {
        await authService.LogOutAsync(cancellationToken);

        return NoContent();
    }
}