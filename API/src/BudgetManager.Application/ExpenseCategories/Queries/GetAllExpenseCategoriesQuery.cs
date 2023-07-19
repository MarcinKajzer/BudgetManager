using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Queries
{
    public record GetAllExpenseCategoriesQuery : IRequest<IEnumerable<GetAllExpenseCategoriesResult>>;

    public class GetAllExpenseCategoriesResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExpenseSubcategoryDto> Subcategories { get; set; }

        public class ExpenseSubcategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CategoryId { get; set; }
        }
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
            var categories = _repository.GetAll().Select(c =>
                new GetAllExpenseCategoriesResult
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = c.Subcategories.Select(s => new GetAllExpenseCategoriesResult.ExpenseSubcategoryDto
                    {
                        CategoryId = s.CategoryId,
                        Id = s.Id,
                        Name = s.Name
                    })
                });
            return new ValueTask<IEnumerable<GetAllExpenseCategoriesResult>>(categories);
        }
    }
}

