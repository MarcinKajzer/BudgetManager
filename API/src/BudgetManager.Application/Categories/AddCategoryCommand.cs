using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories
{
    public record AddCategoryCommand(string Name) : IRequest<Guid>;

    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        public AddCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
            };

            _repository.Add(category);
            return new ValueTask<Guid>(category.Id); //jak dział value task i czy zwrotka nowego taska to dobre rozwiązanie ? 
        }
    }
}
