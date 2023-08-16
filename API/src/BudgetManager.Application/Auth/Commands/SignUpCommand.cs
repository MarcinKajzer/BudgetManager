using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Application.Security;
using BudgetManager.Domain.Users;
using Mediator;

namespace BudgetManager.Application.Auth.Commands;

public record SignUpCommand(string Email, string Password) : ICommand;

public class SingUpHandler : ICommandHandler<SignUpCommand>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;

    public SingUpHandler(IUserRepository repository, IPasswordManager passwordManager)
    {
        _repository = repository;
        _passwordManager = passwordManager;
    }

    public async ValueTask<Unit> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(command.Email, cancellationToken);

        if (user != null)
        {
            throw new UserAlreadyExistsException();
        }

        var newUser = new User()
        {
            Email = command.Email,
            Password = _passwordManager.Hash(command.Password)
        };

        await _repository.CreateAsync(newUser, cancellationToken);
        return Unit.Value;
    }
}

