using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record DeleteExpenseCommand(Guid Id) : ICommand;

    public class DeleteExpenseHandler : ICommandHandler<DeleteExpenseCommand>
    {
        private readonly IExpenseRepository _repository;
        public DeleteExpenseHandler(IExpenseRepository repository)
        {
            _repository = repository;
        }
        public async ValueTask<Unit> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _repository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

            await _repository.DeleteAsync(expense, cancellationToken);
            return Unit.Value;
        }
    }
}
