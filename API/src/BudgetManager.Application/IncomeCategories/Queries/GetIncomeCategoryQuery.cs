using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Queries
{
    public record GetIncomeCategoryQuery(Guid id) : IRequest<GetIncomeCategoryResult>;

    public class GetIncomeCategoryResult
    {
        public GetIncomeCategoryResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GetIncomeCategoryHandler : IRequestHandler<GetIncomeCategoryQuery, GetIncomeCategoryResult>
    {
        private readonly IIncomeCategoryRepository _repository;
        public GetIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<GetIncomeCategoryResult> Handle(GetIncomeCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.id) ?? throw new NotFoundException();
            return new ValueTask<GetIncomeCategoryResult>(new GetIncomeCategoryResult(category.Id, category.Name));
        }
    }
}
