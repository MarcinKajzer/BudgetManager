using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record AddSubcategoryCommand(Guid CategoryId, string Name) : IRequest<Guid>;

    public class AddSubcategoryHandler : IRequestHandler<AddSubcategoryCommand, Guid>
    {
        private readonly IExpenseCategoryRepository _categoryRepository;
        private readonly IExpenseSubcategoryRepository _subcategoryRepository;
        public AddSubcategoryHandler(IExpenseCategoryRepository categoryRepository, IExpenseSubcategoryRepository repository)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = repository;
        }
        public async ValueTask<Guid> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var subcategory = new ExpenseSubcategory
            {
                Name = request.Name,
                Category = category
            };

            await _subcategoryRepository.CreateAsync(subcategory, cancellationToken);

            return subcategory.Id; 
        }
    }
}