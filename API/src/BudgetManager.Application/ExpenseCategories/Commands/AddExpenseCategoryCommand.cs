using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record AddExpenseCategoryCommand(string Name) : IRequest<Guid>;

    public class AddExpenseCategoryHandler : IRequestHandler<AddExpenseCategoryCommand, Guid>
    {
        private readonly IExpenseCategoryRepository _repository;
        public AddExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Guid> Handle(AddExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new ExpenseCategory
            {
                Name = request.Name
            };

            await _repository.CreateAsync(category, cancellationToken);
            return category.Id;
        }
    }
}
