using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record AddSubcategoryCommand(Guid CategoryId, string Name) : IRequest<Guid>;

    public class AddSubcategoryHandler : IRequestHandler<AddSubcategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        public AddSubcategoryHandler(ICategoryRepository categoryRepository, ISubcategoryRepository repository)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = repository;
        }
        public ValueTask<Guid> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var subcategory = new Subcategory
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