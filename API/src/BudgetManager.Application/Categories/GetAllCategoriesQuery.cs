using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<GetAllCategoriesResult>>;

    public class GetAllCategoriesResult
    {
        public GetAllCategoriesResult(Guid id, string name, IEnumerable<Subcategory> subcategories)
        {
            Id = id;
            Name = name;
            Subcategories = subcategories;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }

    public class GetAllCategoriesRequest : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetAllCategoriesResult>>
    {
        private readonly ICategoryRepository _repository;
        public GetAllCategoriesRequest(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<IEnumerable<GetAllCategoriesResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _repository.GetAll().Select(c => new GetAllCategoriesResult(c.Id, c.Name, c.Subcategories));
            return new ValueTask<IEnumerable<GetAllCategoriesResult>>(categories);
        }
    }
}

