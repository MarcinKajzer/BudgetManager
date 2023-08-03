using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Incomes.Commands
{
    public record EditIncomeCommand(Guid Id, decimal Amount, string Comment) : IRequest<Unit>;

    public class EditExpenseHandler : IRequestHandler<EditIncomeCommand, Unit>
    {
        private readonly IIncomeRepository _incomeRepository;
        public EditExpenseHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public async ValueTask<Unit> Handle(EditIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = _incomeRepository.Get(request.Id) ?? throw new NotFoundException();

            income.Amount = request.Amount;
            income.Comment = request.Comment;

            await _incomeRepository.UpdateAsync(income, cancellationToken);
            return Unit.Value;
        }
    }
}
