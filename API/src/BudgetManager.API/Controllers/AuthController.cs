using BudgetManager.Application.Auth.Commands;
using BudgetManager.Application.Interfaces;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApplicationControllerBase
{
    private readonly ITokenStorage _tokenStorage;

    public AuthController(IMediator mediator, ITokenStorage tokenStorage) : base(mediator) => _tokenStorage = tokenStorage;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(_tokenStorage.GetTokens());
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(_tokenStorage.GetTokens());
    }
}
