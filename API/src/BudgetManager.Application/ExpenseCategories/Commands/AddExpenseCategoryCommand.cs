using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.ExpenseCategories.Commands
{
    public record AddExpenseCategoryCommand(string Name) : ICommand;

    public class AddExpenseCategoryHandler : ICommandHandler<AddExpenseCategoryCommand>
    {
        private readonly IExpenseCategoryRepository _repository;
        private readonly IIdStorage _idStorage;
        public AddExpenseCategoryHandler(IExpenseCategoryRepository repository, IIdStorage idStorage)
        {
            _repository = repository;
            _idStorage = idStorage;
        }

        public async ValueTask<Unit> Handle(AddExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new ExpenseCategory
            {
                Name = request.Name
            };

            await _repository.CreateAsync(category, cancellationToken);
            _idStorage.SetId(category.Id);

            return Unit.Value;
        }
    }
}
