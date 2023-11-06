using BudgetManager.Application.Security;
using BudgetManager.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BudgetManager.Infrastructure.Security;
internal class JwtGenerator : ITokenGenerator
{
    private readonly AuthOptions _options;

    public JwtGenerator(IOptions<AuthOptions> options) => _options = options.Value;

    public string Generate()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          _options.Issuer,
          _options.Audience,
          null,
          expires: DateTime.Now.AddHours(_options.ExpiryHours),
          signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
