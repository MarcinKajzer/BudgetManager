using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mapster;
using Mediator;

namespace BudgetManager.Application.Categories.Queries
{
    public record GetExpenseCategoryQuery(Guid id) : IRequest<GetExpenseCategoryResult>;

    public class GetExpenseCategoryResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public IEnumerable<ExpenseSubcategoryDto> Subcategories { get; set; }

        public class ExpenseSubcategoryDto
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ExpenseCategory, GetExpenseCategoryResult>();
        }
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
            return new ValueTask<GetExpenseCategoryResult>(category.Adapt<GetExpenseCategoryResult>());
        }
    }
}
