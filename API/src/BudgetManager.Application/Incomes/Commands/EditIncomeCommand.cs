using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Incomes.Commands
{
    public record UpdateIncomeCommand(Guid Id, decimal Amount, string Comment) : IRequest<Unit>;

    public class UpdateExpenseHandler : IRequestHandler<UpdateIncomeCommand, Unit>
    {
        private readonly IIncomeRepository _incomeRepository;
        public UpdateExpenseHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public async ValueTask<Unit> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await _incomeRepository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

            income.Amount = request.Amount;
            income.Comment = request.Comment;

            await _incomeRepository.UpdateAsync(income, cancellationToken);
            return Unit.Value;
        }
    }
}
