using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mapster;
using Mediator;

namespace BudgetManager.Application.Categories.Queries
{
    public record GetAllExpenseCategoriesQuery : IRequest<IEnumerable<GetAllExpenseCategoriesResult>>;

    public class GetAllExpenseCategoriesResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public IEnumerable<ExpenseSubcategoryDto> Subcategories { get; set; }

        public class ExpenseSubcategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Index { get; set; }
            public Guid CategoryId { get; set; }
        }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ExpenseCategory, GetAllExpenseCategoriesResult>();
        }

        public class GetAllExpenseCategoriesRequest : IRequestHandler<GetAllExpenseCategoriesQuery, IEnumerable<GetAllExpenseCategoriesResult>>
        {
            private readonly IExpenseCategoryRepository _repository;
            public GetAllExpenseCategoriesRequest(IExpenseCategoryRepository repository) => _repository = repository;

            public ValueTask<IEnumerable<GetAllExpenseCategoriesResult>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = _repository.GetAll().Adapt<GetAllExpenseCategoriesResult[]>();
                return new ValueTask<IEnumerable<GetAllExpenseCategoriesResult>>(categories);
            }
        }
    }
}
