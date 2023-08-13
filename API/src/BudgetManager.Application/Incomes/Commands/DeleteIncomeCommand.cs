using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Incomes.Commands
{
    public record DeleteIncomeCommand(Guid Id) : IRequest<Unit>;

    public class DeleteExpenseHandler : IRequestHandler<DeleteIncomeCommand, Unit>
    {
        private readonly IIncomeRepository _repository;
        public DeleteExpenseHandler(IIncomeRepository repository)
        {
            _repository = repository;
        }
        public async ValueTask<Unit> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await _repository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

            await _repository.DeleteAsync(income, cancellationToken);
            return Unit.Value;
        }
    }
}
