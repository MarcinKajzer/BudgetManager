using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.ExpenseSubcategories.Commands
{
    public record AddSubcategoryCommand(Guid CategoryId, string Name) : ICommand;

    public class AddSubcategoryHandler : ICommandHandler<AddSubcategoryCommand>
    {
        private readonly IExpenseCategoryRepository _categoryRepository;
        private readonly IExpenseSubcategoryRepository _subcategoryRepository;
        private readonly IIdStorage _idStorage;
        
        public AddSubcategoryHandler(IExpenseCategoryRepository categoryRepository, IExpenseSubcategoryRepository repository, IIdStorage idStorage)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = repository;
            _idStorage = idStorage;
        }

        public async ValueTask<Unit> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var subcategory = new ExpenseSubcategory
            {
                Name = request.Name,
                Category = category
            };

            await _subcategoryRepository.CreateAsync(subcategory, cancellationToken);
            _idStorage.SetId(subcategory.Id);

            return Unit.Value; 
        }
    }
}