using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Mapster;
using Mediator;

namespace BudgetManager.Application.Expenses.Queries;

public record GetExpenseQuery(Guid Id) : IRequest<GetExpenseResult>;

public class GetExpenseResult : IRegister
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime Date { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Expense, GetExpenseResult>();
    }
}

public class GetExpenseHandler : IRequestHandler<GetExpenseQuery, GetExpenseResult>
{
    private readonly IExpenseRepository _repository;

    public GetExpenseHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }
    public async ValueTask<GetExpenseResult> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        var expense = await _repository.GetAsync(request.Id) ?? throw new NotFoundException();
        return expense.Adapt<GetExpenseResult>();
    }
}
