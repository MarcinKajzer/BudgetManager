using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mapster;
using Mediator;

namespace BudgetManager.Application.IncomesTable.Queries
{
    public record GetIncomeTableQuery : IRequest<IEnumerable<GetIncomeTableResult>>;

    public class GetIncomeTableResult : IRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IncomeDto> Incomes { get; set; }

        public class IncomeDto
        {
            public Guid Id { get; set; }
            public decimal Amount { get; set; }
            public string Comment { get; set; }
            public DateTime Date { get; set; }
            public Guid CategoryId { get; set; }
        }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IncomeCategory, GetIncomeTableResult>();
        }
    }

    public class GetExpenseTableHandler : IRequestHandler<GetIncomeTableQuery, IEnumerable<GetIncomeTableResult>>
    {
        private readonly IIncomeTableRepository _repository;
        public GetExpenseTableHandler(IIncomeTableRepository repository) => _repository = repository;
        
        public ValueTask<IEnumerable<GetIncomeTableResult>> Handle(GetIncomeTableQuery request, CancellationToken cancellationToken)
        {
            var table = _repository.Get();
            return new ValueTask<IEnumerable<GetIncomeTableResult>>(table.Adapt<GetIncomeTableResult[]>());
        }
    }
}
