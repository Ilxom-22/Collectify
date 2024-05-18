using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Collectify.Application.Identity.Models.Settings;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Constants;
using Collectify.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Collectify.Infrastructure.Identity.Services;

public class AccessTokenGeneratorService(IOptions<JwtSettings> jwtSettings)
    : IAccessTokenGeneratorService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    
    public string GetToken(User user)
    {
        var token = GenerateToken(user);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateToken(User user)
    {
        var claims = GetClaims(user);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credentials
        );
        
        return token;
    }

    private List<Claim> GetClaims(User user)
        => 
        [
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimConstants.UserId, user.Id.ToString()),
            new Claim(ClaimConstants.UserName, user.UserName)
        ];
}