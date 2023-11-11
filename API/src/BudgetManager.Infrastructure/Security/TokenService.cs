using BudgetManager.Application.Interfaces;
using BudgetManager.Application.Security;
using BudgetManager.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BudgetManager.Infrastructure.Security;
internal class TokenService : ITokenService
{
    private readonly AuthOptions _options;
    private readonly IDateTime _dateTime;

    public TokenService(IOptions<AuthOptions> options, IDateTime dateTime)
    {
        _options = options.Value;
        _dateTime = dateTime;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          _options.Issuer,
          _options.Audience,
          claims: claims,
          expires: _dateTime.Now.AddDays(_options.RefreshTokenValidInDays),
          signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string? GetUserIdFromAccessToken(string token)
    {
        var principle = ValidateAccessToken(token);
        return principle.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    private ClaimsPrincipal ValidateAccessToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token"); //TO DO: Add custom expception

        return principal;
    }
}
