using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mapster;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Queries
{
    public record GetIncomeCategoryQuery(Guid id) : IRequest<GetIncomeCategoryResult>;

    public class GetIncomeCategoryResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IncomeCategory, GetIncomeCategoryResult>();
        }
    }

    public class GetIncomeCategoryHandler : IRequestHandler<GetIncomeCategoryQuery, GetIncomeCategoryResult>
    {
        private readonly IIncomeCategoryRepository _repository;
        public GetIncomeCategoryHandler(IIncomeCategoryRepository repository) => _repository = repository;

        public ValueTask<GetIncomeCategoryResult> Handle(GetIncomeCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.id) ?? throw new NotFoundException();
            return new ValueTask<GetIncomeCategoryResult>(category.Adapt<GetIncomeCategoryResult>());
        }
    }
}
