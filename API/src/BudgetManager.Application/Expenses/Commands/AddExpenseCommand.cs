using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record AddExpenseCommand(Guid SubcategoryId, DateTime Date, decimal Amount, string Comment) : ICommand;

    public class AddExpenseHandler : ICommandHandler<AddExpenseCommand>
    {
        private readonly IExpenseSubcategoryRepository _subcategoryRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IIdStorage _idStorage;
        
        public AddExpenseHandler(IExpenseSubcategoryRepository subcategoryRepository, IExpenseRepository expenseRepository, IIdStorage idStorage)
        {
            _subcategoryRepository = subcategoryRepository;
            _expenseRepository = expenseRepository;
            _idStorage = idStorage;
        }
        public async ValueTask<Unit> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
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
            _idStorage.SetId((expense.Id));
            
            return Unit.Value;
        }
    }
}
