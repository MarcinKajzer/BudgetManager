using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mapster;
using Mediator;

namespace BudgetManager.Application.ExpenseTables.Queries
{
    public record GetExpenseTableQuery : IRequest<IEnumerable<GetExpenseTableResult>>;

    public class GetExpenseTableResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExpenseSubcategoryDto> Subcategories { get; set; }

        public class ExpenseSubcategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CategoryId { get; set; }
            public ICollection<ExpenseDto> Expenses { get; set; }
        } 

        public class ExpenseDto
        {
            public Guid Id { get; set; }
            public decimal Amount { get; set; }
            public string Comment { get; set; }
            public DateTime Date { get; set; }
            public Guid SubcategoryId { get; set; }
        }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ExpenseCategory, GetExpenseTableResult>();
        }
    }

    public class GetExpenseTableHandler : IRequestHandler<GetExpenseTableQuery, IEnumerable<GetExpenseTableResult>>
    {
        private readonly IExpenseTableRepository _repository;
        public GetExpenseTableHandler(IExpenseTableRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<IEnumerable<GetExpenseTableResult>> Handle(GetExpenseTableQuery request, CancellationToken cancellationToken)
        {
            var table = _repository.Get();
            return new ValueTask<IEnumerable<GetExpenseTableResult>>(table.Adapt<GetExpenseTableResult[]>());
        }
    }
}
