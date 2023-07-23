using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mediator;

namespace BudgetManager.Application.Incomes.Commands
{
    public record AddIncomeCommand(Guid CategoryId, DateTime Date, decimal Amount, string Comment) : IRequest<Guid>;

    public class AddIncomeHandler : IRequestHandler<AddIncomeCommand, Guid>
    {
        private readonly IIncomeCategoryRepository _categoryRepository;
        private readonly IIncomeRepository _incomeRepository;
        public AddIncomeHandler(IIncomeCategoryRepository categoryRepository, IIncomeRepository incomeRepository)
        {
            _categoryRepository = categoryRepository;
            _incomeRepository = incomeRepository;
        }
        public ValueTask<Guid> Handle(AddIncomeCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var income = new Income
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Comment = request.Comment,
                Date = request.Date,
                CategoryId = category.Id
            };

            _incomeRepository.Add(income);

            return new ValueTask<Guid>(income.Id);
        }
    }
}
