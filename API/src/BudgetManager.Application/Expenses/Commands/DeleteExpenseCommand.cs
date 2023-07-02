using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record DeleteExpenseCommand(Guid Id) : IRequest<Unit>;

    public class DeleteExpenseHandler : IRequestHandler<DeleteExpenseCommand, Unit>
    {
        private readonly IExpenseRepository _repository;
        public DeleteExpenseHandler(IExpenseRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<Unit> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = _repository.Get(request.Id) ?? throw new NotFoundException();
            _repository.Delete(expense.Id); // możnaby przekazać cały obiekt zamist tylko Id

            return new ValueTask<Unit>(Unit.Value);
        }
    }
}
