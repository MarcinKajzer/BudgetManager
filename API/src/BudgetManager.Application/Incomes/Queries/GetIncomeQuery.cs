using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mapster;
using Mediator;

namespace BudgetManager.Application.Incomes.Queries;

public record GetIncomeQuery(Guid Id) : IRequest<GetIncomeResult>;

public class GetIncomeResult : IRegister
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime Date { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Income, GetIncomeResult>();
    }
}

public class GetExpenseHandler : IRequestHandler<GetIncomeQuery, GetIncomeResult>
{
    private readonly IIncomeRepository _repository;

    public GetExpenseHandler(IIncomeRepository repository)
    {
        _repository = repository;
    }
    public async ValueTask<GetIncomeResult> Handle(GetIncomeQuery request, CancellationToken cancellationToken)
    {
        var expense = await _repository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();
        return expense.Adapt<GetIncomeResult>();
    }
}

