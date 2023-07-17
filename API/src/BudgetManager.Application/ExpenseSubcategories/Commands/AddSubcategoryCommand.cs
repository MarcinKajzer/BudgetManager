using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
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
        public ValueTask<Guid> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var subcategory = new ExpenseSubcategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CategoryId = category.Id
            };

            _subcategoryRepository.Add(subcategory);

            return new ValueTask<Guid>(subcategory.Id); 
        }
    }
}