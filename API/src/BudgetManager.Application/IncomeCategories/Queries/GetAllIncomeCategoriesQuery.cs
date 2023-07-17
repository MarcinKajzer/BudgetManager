using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Queries
{
    public record GetAllIncomeCategoriesQuery : IRequest<IEnumerable<GetAllIncomeCategoriesResult>>;

    public class GetAllIncomeCategoriesResult
    {
        public GetAllIncomeCategoriesResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GetAllExpenseCategoriesRequest : IRequestHandler<GetAllIncomeCategoriesQuery, IEnumerable<GetAllIncomeCategoriesResult>>
    {
        private readonly IIncomeCategoryRepository _repository;
        public GetAllExpenseCategoriesRequest(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<IEnumerable<GetAllIncomeCategoriesResult>> Handle(GetAllIncomeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _repository.GetAll().Select(c => new GetAllIncomeCategoriesResult(c.Id, c.Name));
            return new ValueTask<IEnumerable<GetAllIncomeCategoriesResult>>(categories);
        }
    }
}