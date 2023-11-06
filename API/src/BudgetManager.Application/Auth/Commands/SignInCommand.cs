using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Application.Security;
using Mediator;

namespace BudgetManager.Application.Auth.Commands;

public record SignInCommand(string Email, string Password) : ICommand;

public class SignInHandler : ICommandHandler<SignInCommand>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ITokenStorage _tokenStorage;

    public SignInHandler(IUserRepository repository, IPasswordManager passwordManager, ITokenGenerator tokenGenerator, ITokenStorage tokenStorage)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _tokenGenerator = tokenGenerator;
        _tokenStorage = tokenStorage;
    }

    public async ValueTask<Unit> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = (await _repository.GetByEmailAsync(command.Email, cancellationToken)) ?? throw new NotFoundException();
        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidSignInDataException();
        }

        _tokenStorage.SetToken(_tokenGenerator.Generate());

        return Unit.Value;
    } 
}
