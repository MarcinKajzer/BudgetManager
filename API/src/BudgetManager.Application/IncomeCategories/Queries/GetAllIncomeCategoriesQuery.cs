using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mapster;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Queries
{
    public record GetAllIncomeCategoriesQuery : IRequest<IEnumerable<GetAllIncomeCategoriesResult>>;

    public class GetAllIncomeCategoriesResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IncomeCategory, GetAllIncomeCategoriesResult>();
        }
    }

    public class GetAllExpenseCategoriesRequest : IRequestHandler<GetAllIncomeCategoriesQuery, IEnumerable<GetAllIncomeCategoriesResult>>
    {
        private readonly IIncomeCategoryRepository _repository;
        public GetAllExpenseCategoriesRequest(IIncomeCategoryRepository repository) => _repository = repository;

        public ValueTask<IEnumerable<GetAllIncomeCategoriesResult>> Handle(GetAllIncomeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _repository.GetAll();
            return new ValueTask<IEnumerable<GetAllIncomeCategoriesResult>>(categories.Adapt<GetAllIncomeCategoriesResult[]>());
        }
    }
}