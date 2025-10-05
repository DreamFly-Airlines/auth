using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Services;

public class MicrosoftIdentityModelJwtProviderService(JwtOptions jwtOptions) : IJwtProviderService
{
    public JwtToken Generate(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            SecurityAlgorithms.HmacSha256);
        var expiresIn = DateTime.UtcNow.AddSeconds(jwtOptions.ExpiresIn.TotalSeconds);
        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            expires: expiresIn,
            claims: [new(ClaimTypes.NameIdentifier, user.Id)]);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        if (accessToken is null)
            throw new InvalidOperationException($"{nameof(accessToken)} is null");
        return JwtToken.FromString(accessToken, expiresIn);
    }
}