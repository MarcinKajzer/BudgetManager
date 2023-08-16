using BudgetManager.Application.Auth.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApplicationControllerBase
{
    
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    //[HttpPost]
    //public async Task<IActionResult> SignIn(SignInCommand command, CancellationToken cancellationToken)
    //{
    //    await _mediator.Send(command, cancellationToken);
    //    return NoContent();
    //}

    //public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    //{
    //    await _mediator.Send(command, cancellationToken);
    //    return NoContent();
    //}
}
