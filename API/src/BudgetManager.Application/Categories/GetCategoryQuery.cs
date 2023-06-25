using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories
{
    public record GetCategoryQuery(Guid id) : IRequest<GetCategoryResult>;

    public class GetCategoryResult
    {
        public GetCategoryResult(Guid id, string name, IEnumerable<Subcategory> subcategories)
        {
            Id = id;
            Name = name;
            Subcategories = subcategories;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, GetCategoryResult>
    {
        private readonly ICategoryRepository _repository;
        public GetCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<GetCategoryResult> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.id) ?? throw new Exception(); // custom not found exception;
            return new ValueTask<GetCategoryResult>(new GetCategoryResult(category.Id, category.Name, category.Subcategories));
        }
    }
}
