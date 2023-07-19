using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomesTable.Queries
{
    public record GetIncomeTableQuery : IRequest<IEnumerable<GetIncomeTableResult>>;

    public class GetIncomeTableResult
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
    }

    public class GetExpenseTableHandler : IRequestHandler<GetIncomeTableQuery, IEnumerable<GetIncomeTableResult>>
    {
        private readonly IIncomeTableRepository _repository;
        public GetExpenseTableHandler(IIncomeTableRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<IEnumerable<GetIncomeTableResult>> Handle(GetIncomeTableQuery request, CancellationToken cancellationToken)
        {
            //TO DO: zaimplementować maperly
            var categories = _repository.Get().Select(c =>
                new GetIncomeTableResult
                {
                    Id = c.Id,
                    Name = c.Name,
                    Incomes = c.Incomes.Select(i => new GetIncomeTableResult.IncomeDto
                    {
                        Amount = i.Amount,
                        Comment = i.Comment,
                        Date = i.Date,
                        Id = i.Id,
                        CategoryId = i.CategoryId
                    }).ToList()
                });

            return new ValueTask<IEnumerable<GetIncomeTableResult>>(categories);
        }
    }
}
