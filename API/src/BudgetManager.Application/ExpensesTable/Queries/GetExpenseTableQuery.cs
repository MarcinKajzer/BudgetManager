using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.ExpenseTables.Queries
{
    public record GetExpenseTableQuery : IRequest<IEnumerable<GetExpenseTableResult>>;

    public class GetExpenseTableResult
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
            //TO DO: zaimplementować maperly
            var categories = _repository.Get().Select(c =>
                new GetExpenseTableResult
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = c.Subcategories.Select(s => new GetExpenseTableResult.ExpenseSubcategoryDto
                    {
                        CategoryId = s.CategoryId,
                        Id = s.Id,
                        Name = s.Name,
                        Expenses = s.Expenses.Select(e => new GetExpenseTableResult.ExpenseDto
                        {
                            Amount = e.Amount,
                            Comment = e.Comment,
                            Date = e.Date,
                            Id = e.Id,
                            SubcategoryId = e.SubcategoryId
                        }).ToList()
                    })
                });

            return new ValueTask<IEnumerable<GetExpenseTableResult>>(categories);
        }
    }
}
