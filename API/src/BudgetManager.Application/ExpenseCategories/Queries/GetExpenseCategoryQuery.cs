using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories.Queries
{
    public record GetExpenseCategoryQuery(Guid id) : IRequest<GetExpenseCategoryResult>;

    public class GetExpenseCategoryResult
    {
        public GetExpenseCategoryResult(Guid id, string name, IEnumerable<ExpenseSubcategory> subcategories)
        {
            Id = id;
            Name = name;
            Subcategories = subcategories;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExpenseSubcategory> Subcategories { get; set; }
    }

    public class GetExpenseCategoryHandler : IRequestHandler<GetExpenseCategoryQuery, GetExpenseCategoryResult>
    {
        private readonly IExpenseCategoryRepository _repository;
        public GetExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<GetExpenseCategoryResult> Handle(GetExpenseCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.id) ?? throw new NotFoundException(); 
            return new ValueTask<GetExpenseCategoryResult>(new GetExpenseCategoryResult(category.Id, category.Name, category.Subcategories));
        }
    }
}
