using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record AddExpenseCommand(Guid SubcategoryId, DateTime Date, decimal Amount, string Comment) : IRequest<Guid>;

    public class AddExpnseHandler : IRequestHandler<AddExpenseCommand, Guid>
    {
        private readonly IExpenseSubcategoryRepository _subcategoryRepository;
        private readonly IExpenseRepository _expenseRepository;
        public AddExpnseHandler(IExpenseSubcategoryRepository subcategoryRepository, IExpenseRepository expenseRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _expenseRepository = expenseRepository;
        }
        public async ValueTask<Guid> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _subcategoryRepository.Get(request.SubcategoryId) ?? throw new NotFoundException();

            var expense = new Expense
            {
                Amount = request.Amount,
                Comment = request.Comment,
                Date = request.Date,
                Subcategory = subcategory
            };

            await _expenseRepository.CreateAsync(expense, cancellationToken);
            return expense.Id;
        }
    }
}
