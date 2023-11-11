using BudgetManager.Application.Interfaces;
using BudgetManager.Application.Security;
using Mediator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BudgetManager.Application.Auth.Commands;
public record RefreshAccessTokenCommand(string refreshToken, string? accessToken) : ICommand;

public class RefreshAccessTokenHandler : ICommandHandler<RefreshAccessTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ITokenStorage _tokenStorage;

    public RefreshAccessTokenHandler(IUserRepository userRepository, ITokenService tokenService, ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _tokenStorage = tokenStorage;
    }

    public async ValueTask<Unit> Handle(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
    {
        if (command.accessToken is null)
        {
            throw new UnauthorizedAccessException();
        }

        var userId = _tokenService.GetUserIdFromAccessToken(command.accessToken);

        if (userId is null)
        {
            throw new UnauthorizedAccessException(); //TO DO: custom exception Invalid Access Token
        }

        var user = await _userRepository.GetAsync(Guid.Parse(userId), cancellationToken);

        if (user is null || user.RefreshToken is null || user.RefreshToken == command.refreshToken)
        {
            throw new UnauthorizedAccessException();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        await _userRepository.UpdateAsync(user, cancellationToken);

        _tokenStorage.SetRefreshToken(refreshToken);
        _tokenStorage.SetAccessToken(_tokenService.GenerateAccessToken(claims));
        
        return Unit.Value;
    }
}

