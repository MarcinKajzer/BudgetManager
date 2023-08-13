using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record EditExpenseCommand(Guid Id, decimal Amount, string Comment) : IRequest<Unit>;

    public class EditExpenseHandler : IRequestHandler<EditExpenseCommand, Unit>
    {
        private readonly IExpenseRepository _expenseRepository;
        public EditExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async ValueTask<Unit> Handle(EditExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

            expense.Amount = request.Amount;
            expense.Comment = request.Comment;

            await _expenseRepository.UpdateAsync(expense, cancellationToken);
            return Unit.Value;
        }
    }
}
