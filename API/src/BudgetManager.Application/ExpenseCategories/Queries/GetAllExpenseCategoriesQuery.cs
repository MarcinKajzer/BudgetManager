using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories.Queries
{
    public record GetAllExpenseCategoriesQuery : IRequest<IEnumerable<GetAllExpenseCategoriesResult>>;

    public class GetAllExpenseCategoriesResult
    {
        public GetAllExpenseCategoriesResult(Guid id, string name, IEnumerable<ExpenseSubcategory> subcategories)
        {
            Id = id;
            Name = name;
            Subcategories = subcategories;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExpenseSubcategory> Subcategories { get; set; }
    }

    public class GetAllExpenseCategoriesRequest : IRequestHandler<GetAllExpenseCategoriesQuery, IEnumerable<GetAllExpenseCategoriesResult>>
    {
        private readonly IExpenseCategoryRepository _repository;
        public GetAllExpenseCategoriesRequest(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<IEnumerable<GetAllExpenseCategoriesResult>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _repository.GetAll().Select(c => new GetAllExpenseCategoriesResult(c.Id, c.Name, c.Subcategories));
            return new ValueTask<IEnumerable<GetAllExpenseCategoriesResult>>(categories);
        }
    }
}

