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
        public ValueTask<Unit> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = _repository.Get(request.Id) ?? throw new NotFoundException();
            _repository.Delete(income.Id); // możnaby przekazać cały obiekt zamist tylko Id

            return new ValueTask<Unit>(Unit.Value);
        }
    }
}
